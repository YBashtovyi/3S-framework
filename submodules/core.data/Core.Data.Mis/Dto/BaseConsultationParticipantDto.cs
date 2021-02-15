using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseConsultationParticipantDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid ConsultationId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid EmployeeId { get; set; }
    }

    public abstract class BaseConsultationParticipantDetailsDto: BaseConsultationParticipantDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string ConsultationCaption { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string EmployeeCaption { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParticipantType { get; set; } 

    }
}
