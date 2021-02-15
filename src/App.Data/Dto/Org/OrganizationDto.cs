using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Data.Dto.Org;
using Core.Security;

namespace App.Data.Dto.Org
{
    [MainEntity(nameof(Organization))]
    [RlsRight(nameof(OrgUnit), nameof(Id))]
    public class OrganizationListDto: BaseOrganizationListDto
    {
        public DateTime CreatedOn { get; set; }

        [CaseFilter]
        public string OrganizationCategory { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string OrganizationCategoryName { get; set; }

        [CaseFilter]
        public string OrgState { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string OrgStateName { get; set; }
    }

    [MainEntity(nameof(Organization))]
    [RlsRight(nameof(OrgUnit), nameof(Id))]
    public class OrganizationDetailsDto: BaseOrganizationDetailsDto
    {
        public string OrganizationCategoryName { get; set; }
        public string OrgStateName { get; set; }

        [NotMapped]
        public IEnumerable<OrgUnitPositionListDto> OrgUnitPositionList { get; set; }
    }

    [MainEntity(nameof(Organization))]
    [RlsRight(nameof(OrgUnit), nameof(Id))]
    public class OrganizationEditDto: BaseOrganizationEditDto
    {
        public string OrganizationCategory { get; set; }
        public string OrgState { get; set; }
    }
}
