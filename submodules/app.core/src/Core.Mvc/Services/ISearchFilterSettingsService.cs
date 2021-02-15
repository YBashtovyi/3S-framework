using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Core.Mvc.Services
{
    public interface ISearchFilterSettingsService
    {
        string GenerateInputConfig(Type dtoType);
        string GenerateInputConfig(Type dtoType, Dictionary<string, SelectList> dictionaryOfSelectLists);
        Task<string> GetUserPresettingsAsync(string journalName);
        Task SetUserPresettings(string journalName, string presettingsJson);
    }
}
