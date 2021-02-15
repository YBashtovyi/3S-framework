using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseConsultationDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid EmployeeId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        [RequiredNonDefault]
        public virtual Guid PatientCardId { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime EndDate { get; set; }
    }

    public abstract class BaseConsultationDetatilDto: BaseConsultationDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string EmployeeCaption { get; set; }
    }
}
