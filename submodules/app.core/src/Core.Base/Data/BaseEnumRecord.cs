using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Base.Data
{
    [Display(Name = "Перерахування")]
    public abstract class BaseEnumRecord : CoreEntity
    {
        [Display(Name = "Група переліку (англ.)"), MaxLength(50)]
        public virtual string Group { get; set; }

        [Display(Name = "Код"), MaxLength(50)]
        public virtual string Code { get; set; }

        [Display(Name = "Значення елемента переліку"), MaxLength(100)]
        public virtual string Name { get; set; }

        [Display(Name = "Будь які додаткові дані у форматі Json")]
        public virtual string Value { get; set; }
    }
}
