using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Ehealth license
    /// Of an organization, for example
    /// </summary>
    [Display(Name = "Ліцензія", Description = "Ліцензія (наприклад організації)")]
    [Table("EhdLicense")]
    public abstract class BaseEhealthLicense: BaseEntity
    {
        public virtual string TypeCode { get; set; }
        public virtual string LicenseNumber { get; set; }
        public virtual string IssuedBy { get; set; }
        public virtual DateTime? IssuedDate { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual DateTime? ActiveFromDate { get; set; }
        public virtual string WhatLicensed { get; set; }
        public virtual string OrderNo { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
