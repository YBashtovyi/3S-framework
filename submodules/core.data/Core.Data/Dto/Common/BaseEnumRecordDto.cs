using System;
using Core.Base.Data;

namespace Core.Data.Dto.Common
{
    public abstract class BaseEnumRecordDto: CoreDto
    {
        public virtual DateTime CreatedOn { get; set; }
        public virtual string Group { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }
}
