using System;
using System.Collections.Generic;
using System.Text;
using Core.Common.Enums;

namespace Core.Services.Data
{
    public interface IFileStoreDestination
    {
        string GetDestination(FileStoreDestinationType destinationType);
    }
}
