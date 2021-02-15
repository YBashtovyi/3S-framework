using System;
using System.Collections.Generic;
using System.Text;
using Core.Administration.Models;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Dto.Administration
{
    [MainEntity(nameof(RoleRight))]
    public class RoleRightListDto : CoreDto
    {
        public Guid RoleId { get; set; }

        public Guid RightId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
