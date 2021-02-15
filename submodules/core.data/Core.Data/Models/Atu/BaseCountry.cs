using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Display(Name = "Довідник країн")]
    [Table("AtuCountry")]
    public abstract class BaseCountry : CoreEntity, ICaption
    {
        [Required, Display(Name = "Назва країни"), MaxLength(100)]
        public string Name { get; set; }

        [Required, Display(Name = "Офіційна назва країни"), MaxLength(200)]
        public string FullName { get; set; }

        [Required, Display(Name = "Літерний код (Альфа-2)"), MaxLength(10)]
        public string Code { get; set; }

        [Display(Name = "Заголовок"), MaxLength(200)]
        public string Caption { get; set; }

        [Display(Name = "Примітки"), MaxLength(200)]
        public string Comment { get; set; }
    }
}
