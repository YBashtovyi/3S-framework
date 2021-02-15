using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Display(Name = "Сутність, який повинні наслідувати всі адміністративно-територіальні одиниці")]
    [Table("AtuSubject")]
    public abstract class BaseSubject: CoreEntity, IDerivableEntity
    {
        public string DerivedEntity { get; set; }

        [Required, Display(Name = "Назва одиниці АТУ"), MaxLength(200)]
        public virtual string Name { get; set; }

        [Required, Display(Name = "Внутрішній код одиниці АТУ"), MaxLength(100)]
        public virtual string Code { get; set; }

        public virtual Guid ParentId { get; set; }
    }
}
