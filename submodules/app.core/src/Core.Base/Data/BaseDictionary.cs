using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Base.Data
{
    public abstract class BaseDictionary: CoreEntity
    {
        [Display(Name = "Код"), MaxLength(50)]
        public virtual string Code { get; set; }
        [Display(Name = "Назва"), MaxLength(100)]
        public virtual string Name{ get; set; }
        [Display(Name = "Коротке пояснення значення довідника"), MaxLength(100)]
        public virtual string Caption { get; set; }
    }
}
