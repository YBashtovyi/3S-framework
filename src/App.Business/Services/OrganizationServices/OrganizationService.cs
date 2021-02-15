using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Org;
using Core.Mvc.Helpers;
using Core.Services.Data;

namespace App.Business.Services.OrganizationServices
{
    public class OrganizationService
    {
        public OrganizationService(ICommonDataService dataService)
        {
            DataBase = dataService;
        }

        public ICommonDataService DataBase { get; }

        #region MethodsPublic

        public async Task<OrganizationEditDto> GetEditPageOrganization(Guid id)
        {
            return new OrganizationEditDto();
        }

        public async Task<OrganizationEditDto> UpdateOrganization(Guid id, OrganizationEditDto dto)
        {
            return new OrganizationEditDto();
        }

        public async Task<OrganizationEditDto> CreateOrganization(OrganizationEditDto dto)
        {
            return new OrganizationEditDto();
        }


        #endregion
    }
}
