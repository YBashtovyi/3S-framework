using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Business.Services
{
    public class FileStoreService: IFileStoreService
    {
        #region FieldsAndProperties
        private readonly ILogger<FileStoreService> _logger;
        private readonly ICommonDataService _dataService;
        private readonly IFileStoreDestination _fileStoreDestinationService;
        private readonly int _fileSizeLimit = 8_388_608; // 2 pow 23
        //private static readonly Dictionary<string, FileType> _fileTypes = new Dictionary<string, FileType>
        //    {
        //        {".txt", FileType.Txt},
        //        {".pdf", FileType.Pdf},
        //        {".doc", FileType.Docx},
        //        {".docx", FileType.Docx},
        //        {".xls", FileType.Xlsx},
        //        {".xlsx", FileType.Xlsx},
        //        {".png", FileType.Img},
        //        {".jpg", FileType.Img},
        //        {".jpeg", FileType.Img},
        //        {".gif", FileType.Img},
        //        {".csv", FileType.Csv}
        //    };
        private static readonly Dictionary<string, string> _mimeTypes = new Dictionary<string, string>
            {
                {".txt", MediaTypeNames.Text.Plain},
                {".rtf", MediaTypeNames.Text.RichText},
                {".pdf", MediaTypeNames.Application.Pdf},
                {".zip", MediaTypeNames.Application.Zip},
                {".jpg", MediaTypeNames.Image.Jpeg},
                {".jpeg", MediaTypeNames.Image.Jpeg},
                {".gif", MediaTypeNames.Image.Gif},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".ppt", "application/vnd.mspowerpoint"},
                {".pptx", "application/vnd.mspowerpoint"},
                {".png", "image/png"},
                {".csv", "text/csv"}
            };
        #endregion FieldsAndProperties

        public FileStoreService(ICommonDataService dataService, IFileStoreDestination fileStoreDestinationService, ILogger<FileStoreService> logger)
        {
            _dataService = dataService;
            _logger = logger;
            _fileStoreDestinationService = fileStoreDestinationService;
        }

        #region Methods_Public

        public async Task<IEnumerable<TStore>> SaveFileAsync<TStore>(IFormFile formFile, IEnumerable<FileStoreDestinationType> destinationTypes, Guid entityId, string entityName, Guid? documentTypeId, Guid? fileGroupId, string description, string typeOfAttachedFile, Guid ownerId)
            where TStore : BaseFileStore, new()
        {
            if (formFile == null)
            {
                return Enumerable.Empty<TStore>();
            }

            var result = await SaveFileAsync<TStore>(async (data, path) => await CreateFileFromFormFile((IFormFile)data, path),
                formFile, formFile.Length, formFile.FileName, destinationTypes, entityId, entityName, documentTypeId, fileGroupId, description, typeOfAttachedFile, ownerId);

            return result;
        }

        public async Task<IEnumerable<TStore>> SaveFileAsync<TStore>(string fileData, string fileName, IEnumerable<FileStoreDestinationType> destinationTypes, Guid entityId, string entityName, Guid? documentTypeId, Guid? fileGroupId, string description, string typeOfAttachedFile, Guid ownerId)
            where TStore : BaseFileStore, new()
        {
            if (fileData == null)
            {
                return Enumerable.Empty<TStore>();
            }

            var result = await SaveFileAsync<TStore>(async (data, path) => await CreateFileFromString((string)data, path),
                fileData, fileData.Length, fileName, destinationTypes, entityId, entityName, documentTypeId, fileGroupId, description,  typeOfAttachedFile,  ownerId);

            return result;
        }

        public async Task<(byte[] data, string fileName, string contentType)> GetFileAsync<TStore>(Guid id)
            where TStore : BaseFileStore
        {
            var files = await _dataService.GetEntityAsync<TStore>(
                x => x.Id == id,
                o => o.OrderBy(el => el.FileStoreDestinationType),
                true);

            if (!files.Any())
            {
                return (new byte[0], string.Empty, string.Empty);
            }

            foreach (var fileMetadata in files)
            {
                if (fileMetadata.FileStoreDestinationType == FileStoreDestinationType.Local)
                {
                    var tempFolder = Path.GetTempPath();
                    var randomDirectory = Path.Combine(tempFolder, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
                    var unZipFilePath = Path.Combine(randomDirectory, fileMetadata.FileName);
                    var fileData = new byte[fileMetadata.FileSize];

                    CreateDirectory(randomDirectory);
                    UnZipFile(fileMetadata.FilePath, randomDirectory, fileMetadata.FileName);
                    using (var stream = new FileStream(unZipFilePath, FileMode.Open, FileAccess.Read))
                    {
                        await stream.ReadAsync(fileData, 0, fileData.Length);
                    }

                    DeleteFileIfExists(unZipFilePath);
                    DeleteFileIfExists(randomDirectory);

                    return (fileData, fileMetadata.FileName, fileMetadata.ContentType);
                }
                else
                {
                    var exception = new NotSupportedException("File destination " + fileMetadata.FileStoreDestinationType.ToString() + " is not supported");
                    _logger.LogError(exception, "File destination {0} is not supported", fileMetadata.FileStoreDestinationType.ToString());
                    throw exception;
                }
            }

            return (new byte[0], string.Empty, string.Empty);
        }

        #endregion Methods_Public

        #region Methods_Private

        private async Task<IEnumerable<TStore>> SaveFileAsync<TStore>(Func<object, string, Task> createFileAction, object fileData, long fileLength,
            string fileName, IEnumerable<FileStoreDestinationType> destinationTypes,
            Guid entityId, string entityName, Guid? documentTypeId, Guid? fileGroupId, string description, string typeOfAttachedFile, Guid ownerId) where TStore : BaseFileStore, new()
        {
            if (fileLength == 0)
            {
                return Enumerable.Empty<TStore>();
            }
            else if (fileLength > _fileSizeLimit)
            {
                var exception = new InsufficientMemoryException("File size exceeds " + _fileSizeLimit.ToString() + " bytes");
                _logger.LogError(exception, "File size exceeds {0} bytes", _fileSizeLimit.ToString());
                throw exception;
            }

            // get destinations for every destination type
            var destinations = GetFileDestinations(destinationTypes);

            // if among the destinations there is local path, use it as temporary folder for zip files
            // else we use temporary directory
            var tempFolder = GetTempFolderPath(destinations);

            // creating folder if does not exist for saving files (temporary or not)
            CreateDirectory(tempFolder);

            // creating folder if does not exist for saving files inside temp directory to avoid collisions
            var randomFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            var randomDirectory = Path.Combine(tempFolder, randomFileName);
            CreateDirectory(randomDirectory);

            // save file on disk for further archiving (if possible, should avoid this operation and archive file directly from stream)
            var tempFilePath = Path.Combine(randomDirectory, fileName);
            await createFileAction(fileData, tempFilePath);

            // getting extensions and name
            var fileExt = Path.GetExtension(fileName).ToLower();
            //var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var fullZipPath = tempFolder + randomFileName + ".zip";

            // zipping file
            CreateZip(tempFilePath, fullZipPath, fileName);

            // creating metadata for files
            var result = new List<TStore>(destinations.Count);
            foreach (var destination in destinations)
            {
                var destinationType = destination.Key;
                var fileMetadata = new TStore()
                {
                    EntityId = entityId,
                    FileStoreDestinationType = destinationType,
                    EntityName = entityName,
                    FileType = fileExt,
                    FileName = fileName,
                    FilePath = fullZipPath,
                    ContentType = GetContentType(fileExt),
                    FileSize = (int)fileLength,
                    Description = description,
                    TypeOfAttachedFile = typeOfAttachedFile,
                    OwnerId = ownerId

                };

                // TODO: saving file into several destinations
                if (destinationType == FileStoreDestinationType.Local)
                {
                    result.Add(fileMetadata);
                }
                else
                {
                    var exception = new NotSupportedException("File destination " + destinationType.ToString() + " is not supported");
                    _logger.LogError(exception, "File destination {0} is not supported", destinationType.ToString());
                    throw exception;
                }
                //var saveFileService = _saveFileServiceFactory.GetService(desitnationType);
                //try
                //{
                //    await saveFileService.SaveAsync(fileMetadata);
                //    result.Add(fileMetadata);
                //}
                //catch (Exception ex)
                //{
                //    _logger.LogError($"Error when trying saving file {fileMetadata.FileName} at path {fileMetadata.FilePath}. Exception: {ex.Message}");
                //}
            }

            if (result.Any())
            {
                await SaveFileStoreMetadata(result);
            }

            // cleaning temporary files
            DeleteDirectoryIfExists(randomDirectory, true);
            // if temp directory was used, then delete created zip file
            if (!destinations.TryGetValue(FileStoreDestinationType.Local, out var _))
            {
                DeleteFileIfExists(fullZipPath);
            }

            return result;
        }    

        /// <summary>
        /// Adds files metadata to the database and saves changes
        /// </summary>
        /// <typeparam name="TStore"></typeparam>
        /// <param name="storeData"></param>
        /// <returns></returns>
        private async Task SaveFileStoreMetadata<TStore>(IEnumerable<TStore> storeData) where TStore : BaseFileStore
        {
            if (storeData != null)
            {
                foreach (var fileMetadata in storeData)
                {
                    _dataService.Add(fileMetadata, fileMetadata.Id != Guid.Empty);
                }
                await _dataService.SaveChangesAsync();
            }
        }

        private Dictionary<FileStoreDestinationType, string> GetFileDestinations(IEnumerable<FileStoreDestinationType> destinationTypes)
        {
            var destinations = new Dictionary<FileStoreDestinationType, string>();
            foreach (var destinationType in destinationTypes)
            {
                var filePath = _fileStoreDestinationService.GetDestination(destinationType);
                if (!string.IsNullOrEmpty(filePath))
                {
                    destinations.Add(destinationType, filePath);
                }
            }
            return destinations;
        }

        private string GetTempFolderPath(Dictionary<FileStoreDestinationType, string> destinations)
        {
            string tempFolder;
            if (destinations.TryGetValue(FileStoreDestinationType.Local, out var localFolder))
            {
                tempFolder = localFolder;
            }
            else
            {
                tempFolder = Path.GetTempPath();
            }
            return tempFolder;
        }

        private void CreateZip(string sourcePath, string destinationPath, string archiveFileName)
        {
            var zipFile = new FileInfo(destinationPath);
            var fs = zipFile.Create();
            using var zip = new ZipArchive(fs, ZipArchiveMode.Create);
            zip.CreateEntryFromFile(sourcePath, archiveFileName, CompressionLevel.Optimal);
        }

        private async Task CreateFileFromFormFile(IFormFile formFile, string path)
        {
            using var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);
        }

        //private async Task CreateFileFromByteArray(byte[] fileData, string path)
        //{
        //    if (fileData.Length > _fileSizeLimit)
        //    {
        //        var exception = new InsufficientMemoryException("File size exceeds " + _fileSizeLimit.ToString() + " bytes");
        //        _logger.LogError(exception, "File size exceeds {0} bytes", _fileSizeLimit.ToString());
        //        throw exception;
        //    }

        //    using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        //    // write data in portions with size = short.MaxValue
        //    int portionSize = short.MaxValue;
        //    var offset = 0;
        //    while (offset < fileData.Length)
        //    {
        //        var bytesLeft = fileData.Length - offset;
        //        if (bytesLeft > portionSize)
        //        {
        //            await stream.WriteAsync(fileData, offset, portionSize);
        //            offset += portionSize;
        //        }
        //        else if (bytesLeft <= 0)
        //        {
        //            // this should not happen, but still...
        //        }
        //        // if bytes left less then portion size, then it is last write operation
        //        else
        //        {
        //            await stream.WriteAsync(fileData, offset, bytesLeft);
        //            offset = fileData.Length;
        //        }
        //    }
        //}

        private async Task CreateFileFromString(string fileData, string path)
        {
            if (fileData.Length > _fileSizeLimit)
            {
                var exception = new InsufficientMemoryException("File size exceeds " + _fileSizeLimit.ToString() + " bytes");
                _logger.LogError(exception, "File size exceeds {0} bytes", _fileSizeLimit.ToString());
                throw exception;
            }

            using var file = new StreamWriter(path, false);
            await file.WriteAsync(fileData);
        }

        private bool UnZipFile(string zipFileName, string dirToUnzipTo, string origFileName)
        {
            using (var archive = ZipFile.OpenRead(zipFileName))
            {
                //Loops through each file in the zip file
                foreach (var file in archive.Entries)
                {
                    //Identifies the destination file name and path
                    var fileUnzipFullName = Path.Combine(dirToUnzipTo, origFileName);

                    //Extracts the files to the output folder in a safer manner
                    if (!File.Exists(fileUnzipFullName))
                    {
                        file.ExtractToFile(fileUnzipFullName);
                    }
                }
            }
            return true;
        }

        private void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void DeleteDirectoryIfExists(string path, bool recursive)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string GetContentType(string fileExtension)
        {
            if (_mimeTypes.TryGetValue(fileExtension, out var result))
            {
                return result;
            }
            return MediaTypeNames.Application.Octet;
        }

        //private FileType GetFileType(string ext)
        //{
        //    if (_fileTypes.TryGetValue(ext, out var result))
        //    {
        //        return result;
        //    }
        //    return FileType.Unknown;
        //}

        #endregion Methods_Private
    }
}
