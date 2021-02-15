using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Services.Data;

namespace Core.Business.Services
{
    public class SaveFileInDirectoryService: ISaveFileService
    {
        public Task SaveAsync<TFileStore>(TFileStore fileMetadata) where TFileStore : BaseFileStore
        {
            throw new NotImplementedException();
        }
    }
}
