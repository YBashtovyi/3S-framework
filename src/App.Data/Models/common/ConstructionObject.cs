using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Models
{
    [Table(nameof(ConstructionObject))]
    [MainEntity(nameof(ConstructionObject))]
    public class ConstructionObject: CoreEntity
    {
        [Required, MaxLength(20), Display(Name = "Код об'єкту")]
        public string Code { get; set; }

        [Required, MaxLength(50), Display(Name = "Статус об'єкту")]
        public string ObjectStatus { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(50), Display(Name = "Тип об'єкту")]
        public string TypeOfConstructionObject { get; set; }

        [Required, MaxLength(50), Display(Name = "Клас наслідків")]
        public string ClassOfConsequence { get; set; }

        [Column(TypeName = "json")]
        public string AtuCoordinates { get; set; }

        [MaxLength(250), Display(Name = "Коментар")]
        public string Description { get; set; }
    }
}
