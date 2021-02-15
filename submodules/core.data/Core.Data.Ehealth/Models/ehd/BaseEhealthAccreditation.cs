using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Accreditation info (of a clinic for example)
    /// </summary>
    [Display(Name = "Інформація про акредитацію")]
    [Table("EhdAccreditation")]
    public abstract class BaseEhealthAccreditation: BaseEntity
    {
        public virtual string CategoryCode { get; set; }
        public virtual DateTime? IssuedDate { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual string OrderNo { get; set; }
        public virtual DateTime OrderDate { get; set; }
    }
}
