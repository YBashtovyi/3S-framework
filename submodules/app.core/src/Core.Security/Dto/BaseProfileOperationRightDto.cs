using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Security.Dto
{
    public abstract class BaseProfileOperationRightDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ProfileId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid OperationRightId { get; set; }
    }
}
