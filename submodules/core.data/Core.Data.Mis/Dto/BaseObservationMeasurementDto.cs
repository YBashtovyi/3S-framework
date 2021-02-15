using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseObservationMeasurementDto: BaseDto
    {
        /// <summary>
        /// LOINC Code  from LOINC Dictionary
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string LoincCode { get; set; }

        public virtual string LoincCaption { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        [Required]
        public virtual string Name { get; set; }

        public virtual string FullName { get; set; }

        /// <summary>
        /// Observation Category Type
        /// Категорії показників
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ObservationCategoryId { get; set; }

        /// <summary>
        /// Value Type of ObservationMeasurement
        /// Code = 'MetricValueType'
        /// Типи значень показників
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MetricValueTypeId { get; set; }

        /// <summary>
        /// Unit of measure Id for current ObservationMeasurement
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? UnitsOfMeasureId { get; set; }

        [Display(Name = "Коментар")]
        [MaxLength(2000)]
        public virtual string Comment { get; set; }
    }
}
