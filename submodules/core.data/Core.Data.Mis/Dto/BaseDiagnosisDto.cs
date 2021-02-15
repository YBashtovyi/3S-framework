using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseDiagnosisDto: BaseDto
    {
    }

    public abstract class BaseDiagnosisListDto: BaseDiagnosisDto
    {
    }

    public abstract class BaseDiagnosisDetailDto: BaseDiagnosisDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? IcpcId { get; set; }
    }
}
