using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.ApplicationServices;
using App.Data.Models;
using Core.Common.Enums;
using Core.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using App.Data.Dto.System;

namespace App.Business.Services.ApiControllerServices
{
    public class FileStoreControllerService
    {
        #region FieldsAndProperties
        public ICommonDataService DataService { get; }
        private readonly IFileStoreService _fileStoreService;
        private readonly IObjectMapper _mapper;
        private readonly ILogger<FileStoreControllerService> _logger;
        private readonly DefaultValuesService _defaultValuesService;
        #endregion FieldsAndProperties

        #region Constructors
        public FileStoreControllerService(ICommonDataService dataService,
            IFileStoreService fileStoreService,
            IObjectMapper mapper,
            ILogger<FileStoreControllerService> logger, DefaultValuesService defaultValuesService)
        {
            DataService = dataService;
            _fileStoreService = fileStoreService;
            _mapper = mapper;
            _logger = logger;
            _defaultValuesService = defaultValuesService;
        }
        #endregion Constructors

        public async Task<IEnumerable<FileStoreDto>> SaveFileAsync(IFormFile formFile, Guid entityId, string entityName, Guid? documentTypeId, Guid? fileGroupId, string description, string typeOfAttachedFile)
        {
            var ownerId = await _defaultValuesService.GetCurrentEmployeeOrganizationAsync();
            var result = await _fileStoreService.SaveFileAsync<FileStore>(formFile, new FileStoreDestinationType[] { FileStoreDestinationType.Local },
                entityId, entityName, documentTypeId, fileGroupId, description, typeOfAttachedFile, ownerId);

            return ConvertCollectionToFileStoreDto(result);
        }

        public async Task<IEnumerable<FileStoreDto>> SaveFilesAsync(FileEmbeddedDto fileDto)
        {
            if (string.IsNullOrWhiteSpace(fileDto.FileName))
            {
                fileDto.FileName = "(without name)";
            }

            //HACK: Change typeOfAttachedFile to normal value, temporary fix. 
            string typeOfAttachedFile = string.Empty;
            var ownerId = await _defaultValuesService.GetCurrentEmployeeOrganizationAsync();

            var result = await _fileStoreService.SaveFileAsync<FileStore>(fileDto.FileData, fileDto.FileName,
                    new FileStoreDestinationType[] { FileStoreDestinationType.Local },
                    fileDto.EntityId, fileDto.EntityName, fileDto.DocumentTypeId, fileDto.FileGroupId, fileDto.Description, typeOfAttachedFile, ownerId);

            return ConvertCollectionToFileStoreDto(result);
        }

        public async Task<(byte[] data, string fileName, string contentType)> GetFileAsync(Guid id)
        {
            var fileData = await _fileStoreService.GetFileAsync<FileStore>(id);
            return fileData;
        }

        private FileStoreDto[] ConvertCollectionToFileStoreDto(IEnumerable<FileStore> entities)
        {
            var collectionType = entities.GetType();
            int count;
            if (collectionType == typeof(FileStore[]))
            {
                count = ((FileStore[])entities).Length;
            }
            else if (collectionType == typeof(List<FileStore>))
            {
                count = ((List<FileStore>)entities).Count;
            }
            else
            {
                count = entities.Count();
            }

            var result = new FileStoreDto[count];
            var i = 0;
            foreach (var entity in entities)
            {
                result[i] = _mapper.Map<FileStoreDto>(entity);
                i++;
            }

            return result;
        }
    }
}
