using System;
using Core.Base.Data;

namespace Core.Data.Common.Dto
{
    public abstract class BaseEnumRecordDto: BaseDto
    {
        public virtual string Code { get; set; }
        public virtual string EnumType { get; set; }
        public virtual Guid? ParentId { get; set; }
    }
}
