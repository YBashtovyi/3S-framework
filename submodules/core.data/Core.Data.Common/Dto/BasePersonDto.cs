using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Common.Dto
{
    public abstract class BasePersonDto : BaseDto
    {
        [Display(Name = "По батькові")]
        public virtual string MiddleName { get; set; }

        [Display(Name = "Прізвище")]
        public virtual string LastName { get; set; }

        [Display(Name = "Ім'я")]
        public virtual string Name { get; set; }

        [Display(Name = "Стать")]
        public virtual Guid? GenderId { get; set; }

        [Display(Name = "Дата народження")]
        public virtual DateTime? Birthday { get; set; }
    }
}
