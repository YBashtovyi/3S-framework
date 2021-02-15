using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthDispensesDetailDto : BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid PrescriptionId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string ManufacturerName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string ManufacturerCountry { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? EhealthId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string MedicationName { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual double MedicationQuantity { get; set; }
    }
}
