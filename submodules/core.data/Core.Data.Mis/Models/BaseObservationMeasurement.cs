using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisObservationMeasurement")]
    public abstract class BaseObservationMeasurement: BaseEntity
    {
        /// <summary>
        /// LOINC Code  from LOINC Dictionary
        /// eHealth/LOINC/observation_codes
        /// </summary>
        public virtual string LoincCode { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual string FullName { get; set; }

        /// <summary>
        /// Observation Category Type
        /// «Категорії показників» 
        /// </summary>
        public virtual Guid ObservationCategoryId { get; set; }

        /// <summary>
        /// Value Type of ObservationMeasurement
        /// «Типи значень показників»
        /// </summary>
        public virtual Guid MetricValueTypeId { get; set; }

        /// <summary>
        /// Unit of measure Id for current ObservationMeasurement
        /// «Одиниці виміру показників» 
        /// </summary>
        public virtual Guid? UnitsOfMeasureId { get; set; }

        public virtual string Comment { get; set; }
    }
}
