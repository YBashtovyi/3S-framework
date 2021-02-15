using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;

namespace App.Data.Models
{
    [Table(nameof(ConstructionObjectExtendedProperty))]
    public class ConstructionObjectExtendedProperty: CoreEntity
    {
        /// <summary>
        /// Id of <see cref="ConstructionObject"/>
        /// </summary>
        [RequiredNonDefault]
        public Guid ConstructionObjectId { get; set; }

        /// <summary>
        /// Id of <see cref="ConstructionObjectExPropertyDictionary"/>
        /// </summary>
        [RequiredNonDefault]
        public Guid DictionaryId { get; set; }

        [Required, MinLength(1), MaxLength(200)]
        public string Value { get; set; }

        [Column(TypeName = "json")]
        public string ValueJson { get; set; }

        public ConstructionObject ConstructionObject { get; set; }

        [ForeignKey("DictionaryId")]
        public ConstructionObjectExPropertyDictionary ConstructionObjectExPropertyDictionary { get; set; }
    }
}
