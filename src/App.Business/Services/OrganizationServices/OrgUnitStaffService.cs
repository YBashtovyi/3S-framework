using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Business.Services.AdministrationServices;
using App.Data.Dto.Administration;
using App.Data.Dto.Org;
using App.Data.Models;
using Core.Base.Administration;
using Core.Services.Data;

namespace App.Business.Services.OrganizationServices
{
    public class OrgUnitStaffService
    {
        public OrgUnitStaffService(ICommonDataService dataService, UserService userService)
        {
            DataBase = dataService;
            _userService = userService;
        }

        private readonly ICommonDataService DataBase;
        private readonly UserService _userService;

        #region MethodsPublic

        public async Task<OrgUnitStaffEditDto> CreateOrgUnitStaff(OrgUnitStaffEditDto dto, bool isSaveInDb)
        {
            var orgUnitPosition = await DataBase.FirstOrDefaultAsync<OrgUnitPositionDetailsDto>(p => p.Id == dto.OrgUnitPositionId);
            if (orgUnitPosition == null)
            {
                throw new Exception();
            }

            var orgUnitStaff = await CreateOrgUnitStaff(dto.OrgUnitPositionId, dto.EmployeeId, orgUnitPosition.OrgUnitId, false, dto.StartDate, dto.EndDate);

            if (isSaveInDb)
            {
                await DataBase.SaveChangesAsync();
            }

            return orgUnitStaff;
        }

        public async Task<OrgUnitStaffEditDto> CreateOrgUnitStaff(Guid orgUnitPositionId, Guid orgEmployee, Guid orgUnitId, bool isSaveInDb, DateTime? startDate = null, DateTime? endDate = null)
        {
            // Create orgUnitStaff
            var orgUnitStaff = new OrgUnitStaffEditDto
            {
                Id = Guid.NewGuid(),
                OrgUnitPositionId = orgUnitPositionId,
                EmployeeId = orgEmployee,
                StartDate = startDate ?? DateTime.Now,
                EndDate = endDate ?? DateTime.MaxValue
            };

            DataBase.AddDto<OrgUnitStaff>(orgUnitStaff, false);

            // Create default rls for user (orgUnit)
            var user = (await DataBase.GetDtoAsync<UserDetailsDto>(p => p.EmployeeId == orgEmployee)).SingleOrDefault();
            if (user != null)
            {
                // TODO: check user. If not exists - exception with text error
                await _userService.CreateRls(user.Id, new RowLevelSecurityData
                {
                    OrgUnit = new List<RowLevelSecurityItem>
                    {
                        new RowLevelSecurityItem
                        {
                            Els = "A",
                            Id = orgUnitId
                        }
                    }
                }, false);
            }

            // Save changes
            if (isSaveInDb)
            {
                await DataBase.SaveChangesAsync();
            }

            return orgUnitStaff;
        }

        

        #endregion
    }
}
