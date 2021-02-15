using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Common.Enums;
using Core.Services.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Business.Services
{
    public class FileStoreDestinationService: IFileStoreDestination
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileStoreDestinationService> _logger;
        public FileStoreDestinationService(IConfiguration configuratin, ILogger<FileStoreDestinationService> logger)
        {
            _configuration = configuratin;
            _logger = logger;
        }

        public string GetDestination(FileStoreDestinationType destinationType)
        {
            var path = string.Empty;
            if (destinationType == FileStoreDestinationType.Local)
            {
                path = _configuration.GetValue<string>("FileStorePath:Local");
                if (!string.IsNullOrWhiteSpace(path))
                {
                    path += DateTime.Now.ToString("ddMMyyyy") + "/";
                    path = Path.GetFullPath(path);
                }
            }
            else if (destinationType == FileStoreDestinationType.Remote)
            {
                path = _configuration.GetValue<string>("FileStorePath:Remote");
            }
            else if (destinationType == FileStoreDestinationType.DataBase)
            {
                // not implemented, return empty string
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                _logger.LogError(@"Error occured when trying to get destination for saving type. Cannot find destination for type ({0})", destinationType);
            }
            return path;
        }
    }
}
