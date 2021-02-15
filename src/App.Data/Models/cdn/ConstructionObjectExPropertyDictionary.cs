using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("ConstructionObjectExPropertyDictionary")]
    public class ConstructionObjectExPropertyDictionary: CoreEntity
    {
        /// <summary>
        /// Code of type of construction object (dictionary)
        /// </summary>
        [Required, MaxLength(32)]
        public string Code { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(300)]
        public string FullName { get; set; }

        /// <summary>
        /// Group DataFormat <see cref="EnumRecord"/>
        /// </summary>
        [Required, MaxLength(50), Display(Name = "Формат даних")]
        public string DataFormat { get; set; }

        [Display(Name = "Підпорядкування")]
        public Guid? ParentId { get; set; }
    }
}
