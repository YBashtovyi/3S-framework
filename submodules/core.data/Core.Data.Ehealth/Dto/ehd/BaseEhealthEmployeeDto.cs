using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public class BaseEhealthEmployeeDto: BaseDto
    {
        [Display(Name = "Підрозділ")]
        public virtual Guid? DivisionId { get; set; }
        [Display(Name = "Медзаклад")]
        public virtual Guid? LegalEntityId { get; set; }
        [Display(Name = "Посада")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string PositionCode { get; set; }
        [Display(Name = "Дата прийому на роботу")]
        [DataType(DataType.Date)]
        public virtual DateTime? StartDate { get; set; }
        [Display(Name = "Дата звільнення")]
        [DataType(DataType.Date)]
        public virtual DateTime? EndDate { get; set; }
        [Display(Name = "Тип користувача")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string EmployeeTypeCode { get; set; }
        public virtual Guid? EhealthRequestId { get; set; }
        public virtual Guid? EhealthId { get; set; }
    }
}
