using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data
{
    [Table("Sys" + nameof(ApplicationSetting))]
    [Display(Name = "Системні налаштування")]
    public class ApplicationSetting: CoreEntity, ICaption
    {
        [Display(Name = "Назва")]
        public string Caption { get; set; }

        [Display(Name = "Налаштування")]
        public string Name { get; set; }

        [Display(Name = "Внутрішній тип")]
        public string Type { get; set; }

        public string TypeName { get; set; }

        [Display(Name = "Увімкнено")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Значення")]
        public string Value { get; set; }
    }
}
