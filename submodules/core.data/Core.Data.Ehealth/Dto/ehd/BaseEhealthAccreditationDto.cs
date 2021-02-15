using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthAccreditationDto: BaseDto
    {
        [Display(Name = "Категорія закладу")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string CategoryCode { get; set; }
        [Display(Name = "Дата початку дії")]
        [DataType(DataType.Date)]
        public virtual DateTime? IssuedDate { get; set; } = DateTime.Now;
        [Display(Name = "Дата закінчення дії")]
        [DataType(DataType.Date)]
        public virtual DateTime? ExpiryDate { get; set; } = DateTime.Now;
        [Display(Name = "Номер наказу МОЗ")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string OrderNo { get; set; }
        [Display(Name = "Дата наказу МОЗ")]
        [Required(ErrorMessage = "Заповніть поле")]
        [DataType(DataType.Date)]
        public virtual DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
