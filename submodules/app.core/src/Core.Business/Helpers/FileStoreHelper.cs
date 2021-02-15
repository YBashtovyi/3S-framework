using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Data.Helpers
{
    public static class FileStoreHelper
    {
        //public static string CreateMD5(string input) {
        //    using (var md5 = MD5.Create()) {
        //        var inputBytes = Encoding.ASCII.GetBytes(input);
        //        var hashBytes = md5.ComputeHash(inputBytes);
        //        var sb = new StringBuilder();
        //        for (var i = 0; i < hashBytes.Length; i++) {
        //            sb.Append(hashBytes[i].ToString("X2"));
        //        }
        //        return sb.ToString();
        //    }
        //}

        //public static FileStoreDTO SaveFile(IConfiguration config, IFormFile formFile, FileStoreDTO fileStoreDTO)
        //{
        //    var folderForSave = config.GetSection("FileStorePath").Value + DateTime.Now.ToString("ddMMyyyy") + "/";
        //    var filePath = Path.GetFullPath(folderForSave);

        //    if (!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }

        //    if (formFile.Length > 0) {
        //        var fileExt = Path.GetExtension(formFile.FileName).ToLower();
        //        var newName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".zip";
        //        var tempfolder = Path.GetTempPath();
        //        var fullOrigPath = tempfolder + formFile.FileName;
        //        var fullZipPath = filePath + newName;

        //        using (var stream = new FileStream(fullOrigPath, FileMode.Create)) {
        //            formFile.CopyTo(stream);
        //        }

        //        var result = CreateZip(fullZipPath, fullOrigPath);

        //        DeleteFileIfExist(fullOrigPath);

        //        return new FileStoreDTO() {
        //            EntityId = fileStoreDTO.EntityId,
        //            EntityName = fileStoreDTO.EntityName,
        //            FileType = GetFileType(fileExt),
        //            FileName = newName,
        //            FilePath = fullZipPath,
        //            OrigFileName = formFile.FileName,
        //            ContentType = GetTypeByContentType(formFile.ContentType),
        //            FileSize = formFile.Length,
        //            DocumentType = fileStoreDTO.DocumentType,
        //            Description = fileStoreDTO.Description
        //        };
        //    }

        //    return null;
        //}

        //public static bool LoadFile(FileStoreDTO fileStoreDTO, out MemoryStream stream, out string contentType)
        //{
        //    stream = null;
        //    contentType = GetContentType(Path.GetExtension(fileStoreDTO.OrigFileName).ToLower());

        //    if (fileStoreDTO == null)
        //    {
        //        return false;
        //    }

        //    var tempDir = Path.GetTempPath();

        //    if (UnZipFile(fileStoreDTO.FilePath, tempDir, fileStoreDTO.OrigFileName)) {
        //        var unZipFilePath = Path.Combine(tempDir, fileStoreDTO.OrigFileName);
        //        stream = new MemoryStream();
        //        using (var st = new FileStream(unZipFilePath, FileMode.Open)) {
        //            st.CopyTo(stream);
        //        }
        //        stream.Position = 0;
        //        DeleteFileIfExist(unZipFilePath);
        //        return true;
        //    }

        //    return false;
        //}

        //private static bool CreateZip(string zipFileName, string fileToZip) {
        //    var zipFile = new FileInfo(zipFileName);
        //    var fs = zipFile.Create();
        //    using (var zip = new ZipArchive(fs, ZipArchiveMode.Create)) {
        //        zip.CreateEntryFromFile(fileToZip, Path.GetFileName(fileToZip), CompressionLevel.Optimal);
        //    }
        //    return true;
        //}

        //private static bool UnZipFile(string zipFileName, string dirToUnzipTo, string origFileName) {
        //    using (ZipArchive archive = ZipFile.OpenRead(zipFileName)) {
        //        //Loops through each file in the zip file
        //        foreach (ZipArchiveEntry file in archive.Entries) {
        //            //Identifies the destination file name and path
        //            var fileUnzipFullName = Path.Combine(dirToUnzipTo, origFileName);

        //            //Extracts the files to the output folder in a safer manner
        //            if (!File.Exists(fileUnzipFullName)) {
        //                file.ExtractToFile(fileUnzipFullName);
        //            }
        //        }
        //    }
        //    return true;
        //}

        //public static void DeleteFileIfExist(string path) {
        //    if (File.Exists(path)) {
        //        File.Delete(path);
        //    }
        //}

        //private static string GetContentType(string path) {
        //    var types = GetMimeTypes();
        //    var ext = Path.GetExtension(path).ToLowerInvariant();
        //    return types[ext];
        //}

        //private static string GetTypeByContentType(string mime) {
        //    var types = GetMimeTypes();
        //    return types.FirstOrDefault(x => x.Value == mime).Key; ;
        //}

        //private static string GetFileTypeByContentType(string path) {
        //    var types = GetMimeTypes();
        //    var ext = Path.GetExtension(path).ToLowerInvariant();
        //    return types[ext];
        //}

        //private static FileType GetFileType(string ext) {
        //    var dict = new Dictionary<string, FileType>
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
        //    return dict[ext];
        //}

        //private static Dictionary<string, string> GetMimeTypes() {
        //    return new Dictionary<string, string>
        //    {
        //        {".txt", "text/plain"},
        //        {".pdf", "application/pdf"},
        //        {".doc", "application/vnd.ms-word"},
        //        {".docx", "application/vnd.ms-word"},
        //        {".xls", "application/vnd.ms-excel"},
        //        {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
        //        {".png", "image/png"},
        //        {".jpg", "image/jpeg"},
        //        {".jpeg", "image/jpeg"},
        //        {".gif", "image/gif"},
        //        {".csv", "text/csv"}
        //    };
        //}
    }
}
