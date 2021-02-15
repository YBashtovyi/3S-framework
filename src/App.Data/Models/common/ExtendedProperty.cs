using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Common;
using Core.Security;

namespace App.Data.Models
{
    [Table("Cmn" + nameof(ExtendedProperty))]
    [MainEntity(nameof(ExtendedProperty))]
    public class ExtendedProperty: BaseExtendedProperty
    {
        public Guid PropertyTypeId { get; set; }
        public EnumRecord PropertyType { get; set; }
    }
}
