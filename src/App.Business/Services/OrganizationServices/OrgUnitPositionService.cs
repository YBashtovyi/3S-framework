using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Cdn;
using App.Data.Dto.Org;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Services.Data;

namespace App.Business.Services.OrganizationServices
{
    public class OrgUnitPositionService
    {
        public OrgUnitPositionService(ICommonDataService dataService)
        {
            DataBase = dataService;
        }

        public ICommonDataService DataBase { get; }

        #region MethodsPublic

        public async Task<OrgUnitPositionEditDto> CreateOrgUnitPosition(Guid orgUnitId, string positionCode, bool isSaveInDb)
        {
            var position = await DataBase.SingleOrDefaultAsync<PositionDto>(p => p.Code == positionCode);
            if (position == null)
            {
                throw new AppException($"Посада з кодом {positionCode} не існує у довіднику посад");
            }

            var orgUnitPosition = new OrgUnitPositionEditDto()
            {
                Id = Guid.NewGuid(),
                OrgUnitId = orgUnitId,
                PositionId = position.Id,
                StaffUnitCount = 0
            };

            DataBase.AddDto<OrgUnitPosition>(orgUnitPosition, false);

            if (isSaveInDb)
            {
                await DataBase.SaveChangesAsync();
            }

            return orgUnitPosition;
        }

        #endregion
    }
}
