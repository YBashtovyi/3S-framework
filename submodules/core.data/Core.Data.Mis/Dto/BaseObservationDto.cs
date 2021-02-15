using System;
using Core.Base.Data;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseObservationDto: BaseDto
    {
        public virtual Guid MedicalExaminationId { get; set; }

        public virtual Guid ObservationMeasurementId { get; set; }
        public virtual string Value { get; set; }

        public virtual bool IsFirstTimeDetected { get; set; }

        public virtual Guid? ObservationSourceTypeId { get; set; }

        public virtual DateTime RecordDateTime { get; set; }

        public virtual DateTime? EffectiveDateTimeStart { get; set; }

        public virtual DateTime? EffectiveDateTimeEnd { get; set; }

        public virtual string Comment { get; set; }
    }
}
