using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Table("EhdQualification")]
    public abstract class BaseEhealthQualification: BaseEntity
    {
        public virtual string TypeCode { get; set; }
        public virtual string InstitutionName { get; set; }
        public virtual string SpecialityCode { get; set; }
        public virtual DateTime? IssuedDate { get; set; }
        public virtual string CertificateNumber { get; set; }
        public virtual DateTime? ValidTo { get; set; }
        public virtual string AdditionalInfo { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
