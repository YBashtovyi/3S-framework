using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Org;
using Core.Security;
using Core.Security.Models;

namespace App.Data.Models
{
    [Table("Org" + nameof(Employee))]
    [MainEntity(nameof(Employee))]
    [RlsRight(nameof(Employee), nameof(Id))]
    public class Employee: BaseEmployee
    {
        public Person Person { get; set; }
        [ForeignKey("UserId")]
        public List<UserProfile> Profiles { get; set; }
        [ForeignKey("UserId")]
        public List<UserDefaultValue> DefaultValues { get; set; }
    }
}
