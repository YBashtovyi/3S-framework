using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthLicenseDto: BaseDto
    {
        [Display(Name = "Тип ліцензії (код)")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string TypeCode { get; set; }
        [Display(Name = "Номер ліцензії")]
        public virtual string LicenseNumber { get; set; }
        [Display(Name = "Ким видана")]
        public virtual string IssuedBy { get; set; } = "Кваліфікацйна комісія";
        [Display(Name = "Дата видачі ліцензії")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual DateTime? IssuedDate { get; set; } = DateTime.Now;
        [Display(Name = "Дата закінчення дії")]
        [DataType(DataType.Date)]
        public virtual DateTime? ExpiryDate { get; set; } = DateTime.Now;
        [Display(Name = "Дата початку дії")]
        [Required(ErrorMessage = "Заповніть поле")]
        [DataType(DataType.Date)]
        public virtual DateTime? ActiveFromDate { get; set; } = DateTime.Now;
        [Display(Name = "Тип ліцензії")]
        public virtual string WhatLicensed { get; set; } = "Реалізація наркотичних засобів";
        [Display(Name = "Номер приказу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string OrderNo { get; set; }
        public virtual Guid? EntityId { get; set; }
    }
}
