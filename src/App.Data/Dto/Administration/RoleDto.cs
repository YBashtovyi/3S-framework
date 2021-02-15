using System;
using System.Collections.Generic;
using System.Text;
using Core.Administration.Models;
using Core.Base.Data;
using Core.Security;
using DocumentFormat.OpenXml.Spreadsheet;

namespace App.Data.Dto.Administration
{
    [MainEntity(nameof(Role))]
    public class RoleListDto: CoreDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }

        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(Role))]
    public class RoleDetailsDto : CoreDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public string Rls { get; set; }
    }

    [MainEntity(nameof(Role))]
    public class RoleEditDto : CoreDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }
    }

    /// <summary>
    /// Get a list of roles that users contain
    /// </summary>
    [MainEntity(nameof(Role))]
    public class UserRoleListDto
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
