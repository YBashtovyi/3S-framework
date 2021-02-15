using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Data.Models.Org;

namespace Core.Administration.Models
{
    [Table("Adm" + nameof(User))]
    public class User: CoreEntity
    {
        public Guid EmployeeId { get; set; }
        public BaseEmployee Employee { get; set; }

        [MaxLength(100)]
        public string Login { get; set; }

        [Column(TypeName = "json")]
        public string Roles { get; set; }

        [Column(TypeName = "json")]
        public string Rls { get; set; }

        public List<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
    }
}
