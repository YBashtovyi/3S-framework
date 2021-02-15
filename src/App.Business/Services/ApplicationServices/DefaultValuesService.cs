using System;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Common;
using App.Data.Dto.Org;
using App.Data.Models;
using Core.Services;
using Core.Services.Data;
using Core.Services.DistributedCacheService;
using Microsoft.Extensions.Logging;

namespace App.Business.Services.ApplicationServices
{
    /// <summary>
    /// Contains frequently used methods
    /// </summary>
    public class DefaultValuesService
    {
        #region fields: private
        private readonly ICommonDataService _dataService;
        private readonly IUserInfoService _userService;
        private readonly ILogger<DefaultValuesService> _logger;
        private readonly IDistributedCacheService _cacheService;
        #endregion

        #region constructor
        public DefaultValuesService(ICommonDataService dataService,
            IUserInfoService userInfoService,
            IDistributedCacheService cacheService,
            ILogger<DefaultValuesService> logger)
        {
            _dataService = dataService;
            _userService = userInfoService;
            _logger = logger;
            _cacheService = cacheService;
        }
        #endregion

        #region methods: public
        /// <summary>
        /// Get organization id of current user
        /// </summary>
        /// <returns></returns>
        public async Task<Guid> GetCurrentEmployeeOrganizationAsync()
        {
            var currentUser = await _userService.GetCurrentUserInfoAsync();
            if (currentUser.Id == Guid.Empty)
            {
                return Guid.Empty;
            }
            else
            {
                // В списке может быть больше 1 записи. Человек может быть в разных организациях. Нужно обговорить
                var employee = (await _dataService.GetDtoAsync<OrgEmployeeSimpleDto>(x => x.UserId == currentUser.Id, null, null, 0, 0, 0, _userService.SystemUser, null)).FirstOrDefault();

                if (employee == null)
                {
                    _logger.LogError("Cannot find an employee by id {EmployeeId} to get default organization", currentUser.Id);
                    return Guid.Empty;
                }
                else
                {
                    if (employee.OrgUnitId == Guid.Empty)
                    {
                        _logger.LogError("The organization was not defined for current user with id {EmployeeId}", currentUser.Id);
                    }
                    return employee.OrgUnitId;
                }
            }
        }

        /// <summary>
        /// Get department id of current user
        /// </summary>
        /// <returns></returns>
        //public async Task<Guid?> GetCurrentEmployeeDepartmentAsync()
        //{
        //    var currentUser = await _userService.GetCurrentUserInfoAsync();
        //    if (currentUser?.UserId == null)
        //    {
        //        return Guid.Empty;
        //    }
        //    else
        //    {
        //        var employee = (await _dataService.GetDtoAsync<OrgEmployeeSimpleDto>(x => x.Id == currentUser.UserId, null, null, 0, 0, 0, _userService.SystemUser, null)).FirstOrDefault();

        //        if (employee == null)
        //        {
        //            _logger.LogError("Cannot find an employee by id {EmployeeId} to get default organization", currentUser.UserId);
        //            return Guid.Empty;
        //        }
        //        else
        //        {
        //            if (employee.DepartmentId == null)
        //            {
        //                _logger.LogError("The department was not defined for current user with id {EmployeeId}", currentUser.UserId);
        //            }
        //            return employee.DepartmentId;
        //        }
        //    }
        //}

        /// <summary>
        /// Gets enum record from cache by enum type and value type
        /// If value is not cached, gets value from the database and caches the value for subsequent queries
        /// </summary>
        /// <param name="group">Field in <see cref="EnumRecord"/> entity</param>
        /// <param name="valueType">Field in <see cref="EnumRecord"/> entity</param>
        /// <returns>First enum record that matches parameters or null</returns>
        public async Task<EnumRecordDto> GetEnumRecordAsync(string group, int valueType = 0)
        {
            var record = (await _dataService.GetDtoAsync<EnumRecordDto>(x => x.ItemNumber == valueType && x.Group == group,
                    null, null, 0, 0, 3600, _userService.SystemUser, null))
                .FirstOrDefault();

            return record;
        }

        #endregion
    }
}
