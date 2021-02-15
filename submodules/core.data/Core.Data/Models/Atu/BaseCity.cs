using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Table("AtuCity")]
    public class BaseCity: CoreEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public Type BaseType => typeof(BaseSubject);
        #endregion

        [Required, Display(Name = "Назва міста"), MaxLength(200)]
        public virtual string Name { get; set; }

        [Required, Display(Name = "Тип населеного пункту, перелік (місто, смт, селище, село)"), MaxLength(50)]
        public virtual string AtuCityType { get; set; }
    }
}
