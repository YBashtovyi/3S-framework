using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseSymptomDto: BaseDto
    {
    }

    public abstract class BaseSymptomListDto: BaseSymptomDto
    {
    }

    public abstract class BaseSymptomDetailDto: BaseSymptomDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? IcpcId { get; set; }
    }
}
