using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Table("AtuStreet")]
    public class BaseStreet: CoreEntity
    {
        [Required, Display(Name = "Назва вулиці"), MaxLength(200)]
        public virtual string Name { get; set; }

        public virtual Guid CityId { get; set; }

        [Required, Display(Name = "Тип вулиці, перелік(проспекти, бульвари, площі і т.д)"), MaxLength(50)]
        public virtual string AtuStreetType { get; set; }
    }
}
