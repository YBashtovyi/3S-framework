using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Org
{
    public abstract class BaseDepartmentDetailsDto: CoreDto
    {
        public virtual string Name { get; set; }

        public virtual string FullName { get; set; }

        public virtual string Code { get; set; }

        public virtual string DepartmentType { get; set; }

        public virtual string Description { get; set; }

        public virtual Guid? ParentId { get; set; }
    }

    public abstract class BaseDepartmentEditDto : CoreDto
    {
        public virtual string Name { get; set; }

        public virtual string FullName { get; set; }

        public virtual string Code { get; set; }

        public virtual string DepartmentType { get; set; }

        public virtual string Description { get; set; }

        public virtual Guid? ParentId { get; set; }
    }

    public abstract class BaseDepartmentListDto : CoreDto, IPagingCounted
    {
        public virtual int TotalRecordCount { get; set; }

        public DateTime CreatedOn { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string DepartmentType { get; set; }
    }
}
