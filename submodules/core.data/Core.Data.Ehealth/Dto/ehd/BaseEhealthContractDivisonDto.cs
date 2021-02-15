using System;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthContractDivisonDto: BaseDto
    {
        public virtual Guid DivisionId { get; set; }
        public virtual Guid EntityId { get; set; }
    }
}
