using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisObservation")]
    public abstract class BaseObservation: BaseEntity
    {
        /// <summary>
        /// Application MedicalExamination id
        /// </summary>
        public virtual Guid MedicalExaminationId { get; set;  }

        /// <summary>
        /// Application ObservationMeasurement id
        /// Довідник показників
        /// </summary>
        public virtual Guid ObservationMeasurementId { get; set; }

        /// <summary>
        /// Значення вноситься в залежності від типу даних (вибір довідникового значення, внесення з клавіатури), Редагується
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Вперше виявлено*
        /// used in Ehealth with name: "primary_source"
        /// </summary>
        public virtual bool IsFirstTimeDetected { get; set; }

        /// <summary>
        ///  Джерело 
        ///  EnumRecord EnumType = "ObservationSourceType"
        ///  used in Ehealth with name: "report_origin"
        /// </summary>
        public virtual Guid? ObservationSourceTypeId { get; set;}

        /// <summary>
        /// UTC DateTime
        /// used in Ehealth with name: "issued"
        /// </summary>
        public virtual DateTime RecordDateTime { get; set; }

        /// <summary>
        /// if  "PrimarySource" == true -> EffectiveDateTimeStart = current DateTime
        /// else Date manualy setting EffectiveDateTimeStart and EffectiveDateTimeEnd
        /// </summary>
        public DateTime? EffectiveDateTimeStart { get; set; }

        /// <summary>
        /// This value setting only if value in "PrimarySource" == false
        /// </summary>
        public DateTime? EffectiveDateTimeEnd { get; set; }

        [MaxLength(500)]
        public virtual string Comment { get; set; }
    }
}
