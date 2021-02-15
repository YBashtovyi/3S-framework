using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Common;
using Core.Security;

namespace App.Data.Models
{
    [Table("Cmn" + nameof(EntityExtendedPropertyValue))]
    [MainEntity(nameof(EntityExtendedPropertyValue))]
    public class EntityExtendedPropertyValue: BaseEntityExtendedPropertyValue
    {
        public ExtendedProperty Property { get; set; }
    }
}
