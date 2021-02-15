using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Common;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(Person))]
    [Table("Cmn" + nameof(Person))]
    public class Person: BasePerson
    {
    }
}
