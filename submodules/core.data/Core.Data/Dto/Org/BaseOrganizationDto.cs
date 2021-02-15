using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Org
{
    public abstract class BaseOrganizationDetailsDto: CoreDto
    {
        public virtual string Name { get; set; }

        public virtual string FullName { get; set; }

        public virtual string Description { get; set; }

        public virtual string OrgType { get; set; }

        public virtual string Edrpou { get; set; }
    }

    public abstract class BaseOrganizationEditDto: CoreDto
    {
        public virtual string Name { get; set; }

        public virtual string FullName { get; set; }

        public virtual string Description { get; set; }

        public virtual string OrgType { get; set; }

        public virtual string Edrpou { get; set; }
    }

    public abstract class BaseOrganizationListDto: CoreDto, IPagingCounted
    {
        public virtual int TotalRecordCount { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Edrpou { get; set; }
    }
}
