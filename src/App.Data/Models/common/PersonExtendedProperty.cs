using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Data.Models.Common;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(PersonExtendedProperty))]
    [Table("Cmn" + nameof(PersonExtendedProperty))]
    public class PersonExtendedProperty: BasePersonExtendedProperty
    {
    }
}
