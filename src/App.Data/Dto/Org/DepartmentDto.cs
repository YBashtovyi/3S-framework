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
    [MainEntity(nameof(Department))]
    [RlsRight(nameof(OrgUnit), nameof(Id))]
    public class DepartmentListDto : BaseDepartmentListDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string DepartmentTypeName { get; set; }

        [CaseFilter]
        public string DepartmentState { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string DepartmentStateName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentName { get; set; }
    }

    [MainEntity(nameof(Department))]
    [RlsRight(nameof(OrgUnit), nameof(Id))]
    public class DepartmentDetailsDto: BaseDepartmentDetailsDto
    {
        public string DepartmentTypeName { get; set; }

        [CaseFilter]
        public string DepartmentState { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string DepartmentStateName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentName { get; set; }
    }

    [MainEntity(nameof(Department))]
    [RlsRight(nameof(OrgUnit), nameof(Id))]
    public class DepartmentEditDto : BaseDepartmentEditDto
    {
        [CaseFilter]
        public string DepartmentState { get; set; }
    }
}
