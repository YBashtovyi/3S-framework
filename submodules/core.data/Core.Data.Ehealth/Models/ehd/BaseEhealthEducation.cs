using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Education. Of employees, for example
    /// </summary>
    [Display(Name = "Освіта")]
    [Table("EhdEducation")]
    public abstract class BaseEhealthEducation: BaseEntity
    {
        public virtual string CountryCode { get; set; }
        public virtual string City { get; set; }
        public virtual string InstitutionName { get; set; }
        public virtual DateTime? IssuedDate { get; set; }
        public virtual string DiplomaNumber { get; set; }
        public virtual string DegreeCode { get; set; }
        public virtual string SpecialityCode { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
