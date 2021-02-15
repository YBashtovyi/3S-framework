using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Atu
{
    public abstract class BaseRegionListDto: CoreDto, IPagingCounted
    {
        public virtual int TotalRecordCount { get; set; }

        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual string Koatu { get; set; }
        public virtual string AtuRegionType { get; set; }

    }
}
