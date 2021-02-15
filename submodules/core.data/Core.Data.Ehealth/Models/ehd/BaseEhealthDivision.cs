using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Ehealth department
    /// </summary>
    [Display(Name = "Підрозділ eHealth")]
    [Table("EhdDivision")]
    public abstract class BaseEhealthDivision: BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string TypeCode { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual string Email { get; set; }
        public virtual decimal LocationLatitude { get; set; }
        public virtual decimal LocationLongitude { get; set; }
        public virtual Guid? EhealthId { get; set; }
    }
}
