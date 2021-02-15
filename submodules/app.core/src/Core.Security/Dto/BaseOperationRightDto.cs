using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Security.Dto
{
    public abstract class BaseOperationRightDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string OperationName { get; set; } = string.Empty;

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual AccessLevel AccessLevel { get; set; } = AccessLevel.No;

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }
    }
}
