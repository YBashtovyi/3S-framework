using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Extensions;

namespace Core.Data.Common.Models
{
    [Display(Name = "Персона")]
    [Table("Person")]
    public abstract class BasePerson : BaseEntity
    {
        [Display(Name = "Ім'я")]
        public virtual string Name { get; set; }

        [Display(Name = "По батькові")]
        public virtual string MiddleName { get; set; }

        [Display(Name = "Прізвище")]
        public virtual string LastName { get; set; }

        [Display(Name = "Дата народження")]
        public virtual DateTime? Birthday { get; set; }

        [Display(Name = "Стать")]
        public virtual Guid? GenderId { get; set; }
    }
}
