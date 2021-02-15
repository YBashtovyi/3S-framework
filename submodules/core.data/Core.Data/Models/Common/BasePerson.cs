using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Common
{
    [Display(Name = "Персона")]
    [Table("Person")]
    public abstract class BasePerson : CoreEntity, ICaption
    {
        [MaxLength(300), Display(Name = "ПІБ")]
        public virtual string Caption { get; set; }

        [Required, MaxLength(100), Display(Name = "Ім'я")]
        public virtual string FirstName { get; set; }

        [Required, MaxLength(100), Display(Name = "По батькові")]
        public virtual string MiddleName { get; set; }

        [Required, MaxLength(100), Display(Name = "Прізвище")]
        public virtual string LastName { get; set; }

        [MaxLength(50), Display(Name = "Стать")]
        public virtual string Gender { get; set; }

        [Display(Name = "Дата народження")]
        public virtual DateTime? Birthday { get; set; }

        [MaxLength(20), Display(Name = "Індивідуальний податковий номер (ІПН)")]
        public virtual string TaxNumber { get; set; }

        [Display(Name = "ІПН відсутній (по замовчанню false)")]
        public virtual bool NoTaxNumber { get; set; }

        [MaxLength(50), Display(Name = "Документ, що посвідчує особу")]
        public virtual string IdentityDocument { get; set; }
    }
}
