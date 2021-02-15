using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Table("AtuAddress")]
    public class BaseAddress: CoreEntity
    {
        public virtual Guid StreetId { get; set; }

        [Required, Display(Name = "Поштовий індекс"), MaxLength(50)]
        public virtual string ZipCode { get; set; }

        [Required, Display(Name = "Номер будинку"), MaxLength(50)]
        public virtual string Building { get; set; }

        [Required, Display(Name = "Під'їд"), MaxLength(50)]
        public virtual string Entrance { get; set; }

        [Required, Display(Name = "Квартира"), MaxLength(50)]
        public virtual string Apartment { get; set; }

        public virtual string Comment { get; set; }
    }
}
