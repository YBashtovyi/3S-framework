using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Services.Data
{
    public interface IFileStoreService
    {
        Task<IEnumerable<TStore>> SaveFileAsync<TStore>(IFormFile formFile, IEnumerable<FileStoreDestinationType> destinationTypes,
            Guid entityId, string entityName, Guid? documentTypeId, Guid? fileGroupId, string description, string typeOfAttachedFile, Guid ownerId)
            where TStore : BaseFileStore, new();

        Task<IEnumerable<TStore>> SaveFileAsync<TStore>(string fileData, string fileName, IEnumerable<FileStoreDestinationType> destinationTypes,
            Guid entityId, string entityName, Guid? documentTypeId, Guid? fileGroupId, string description, string typeOfAttachedFile, Guid ownerId)
            where TStore : BaseFileStore, new();

        Task<(byte[] data, string fileName, string contentType)> GetFileAsync<TStore>(Guid id)
            where TStore : BaseFileStore;
    }
}
