using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Cdn
{
    [MainEntity(nameof(TypeOfProjectWork))]
    public class TypeOfObjectWorkListDto : CoreDto
    {
        [CaseFilter]
        public string Code { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string FullName { get; set; }

        [CaseFilter]
        public Guid? ParentId { get; set; }

        [NotMapped]
        public string CodeName => $"{Code} {Name}";
    }
}
