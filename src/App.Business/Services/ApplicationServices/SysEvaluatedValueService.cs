using System;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.System;
using App.Data.Models;
using Core.Services;
using Core.Services.Data;

namespace App.Business.Services.ApplicationServices
{
    public class SysEvaluatedValueService
    {
        #region public: properties
        public ICommonDataService DataService { get; }
        #endregion

        #region private: properties
        private readonly IUserInfoService _userInfoService;
        #endregion

        #region constructor
        public SysEvaluatedValueService(ICommonDataService dataService, IUserInfoService userInfoService)
        {
            DataService = dataService;
            _userInfoService = userInfoService;
        }
        #endregion

        #region public : methods

        public async Task<SysEvaluatedValueDto> GetSysEvaluatedValueDtosAsync(string entityName, Guid entityId, string valueName)
        {
            return (await DataService.GetDtoAsync<SysEvaluatedValueDto>(x => x.EntityName.Equals(entityName) && x.EntityId == entityId && x.ValueName.Equals(valueName))).FirstOrDefault();
        }

        public async Task SetSysEvaluatedValueDtoAsync(SysEvaluatedValueDto sysEvaluatedValueDto, bool isUpdating = true)
        {
            DataService.AddDto<SysEvaluatedValue>(sysEvaluatedValueDto, isUpdating, _userInfoService.SystemUser);
            await DataService.SaveChangesAsync();
        }

        public async Task DeleteSysEvaluatedValueDtoAsync(SysEvaluatedValueDto sysEvaluatedValueDto, bool softDeleting = false)
        {
            DataService.Remove<SysEvaluatedValue>(sysEvaluatedValueDto.Id, softDeleting, _userInfoService.SystemUser);
            await DataService.SaveChangesAsync();
        }
        #endregion
    }
}
