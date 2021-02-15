using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Ehealth address.
    /// Used in many eHealth entities
    /// </summary>
    [Display(Name = "Адреса eHealth", Description = "Адреса (клініки, підрозділу, пацієнта декларації)")]
    [Table("EhdAddress")]
    public abstract class BaseEhealthAddress: BaseEntity
    {
        public virtual string TypeCode { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Area { get; set; }
        public virtual string Region { get; set; }
        public virtual string Settlement { get; set; }
        public virtual string SettlementTypeCode { get; set; }
        public virtual string SettlementId { get; set; }
        public virtual string StreetTypeCode { get; set; }
        public virtual string Street { get; set; }
        public virtual string Building { get; set; }
        public virtual string Apartment { get; set; }
        public virtual string Zip { get; set; }
        public virtual Guid? EntityId { get; set; }
        public virtual string RegionId { get; set; }
        public virtual string AreaId { get; set; }
        public virtual string StreetId { get; set; }
    }
}
