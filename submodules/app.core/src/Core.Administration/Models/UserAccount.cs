using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Administration.Models
{
    [Table("Adm" + nameof(UserAccount))]
    public class UserAccount: CoreEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        [MaxLength(50), MinLength(1)]
        public string AuthProvider { get; set; }

        [MaxLength(36), MinLength(1)]
        public string AccountId { get; set; }
    }
}
