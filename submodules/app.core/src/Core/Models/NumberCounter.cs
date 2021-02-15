using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Enums;

namespace Core.Models
{
    [Display(Name = "Лічильник вхідник/вихідних номерів документів")]
    public class NumberCounter: CoreEntity, ICaption
    {
        public string Caption { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Сутність")]
        public string EntityName { get; set; }

        [Display(Name = "Ідентифікатор сутності")]
        public Guid? EntityId { get; set; }

        [Display(Name = "Тип нумерації")]
        public RegNumberCounterType CounterType { get; set; }

        [Display(Name = "Шаблон номеру")]
        public NumberCounterPattern Pattern { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Номер")]
        public string Value { get; set; }
    }
}
