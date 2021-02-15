using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Ehealth speciality dictionary
    /// Used in eHealth employees
    /// </summary>
    [Display(Name = "Спеціальність eHealth")]
    [Table("EhdSpeciality")]
    public abstract class BaseEhealthSpeciality: BaseEntity
    {
        public virtual string SpecialityCode { get; set; }
        public virtual bool SpecialityOfficio { get; set; }
        public virtual string LevelCode { get; set; }
        public virtual string QualificationTypeCode { get; set; }
        public virtual string AttestationName { get; set; }
        public virtual DateTime? AttestationDate { get; set; }
        public virtual DateTime? ValidToDate { get; set; }
        public virtual string CertificateNumber { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
