using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Common.Enums;

namespace Core.Services.Data
{
    public interface ISaveFileService
    {
        Task SaveAsync<TFileStore>(TFileStore fileMetadata) where TFileStore : BaseFileStore;
    }
}
