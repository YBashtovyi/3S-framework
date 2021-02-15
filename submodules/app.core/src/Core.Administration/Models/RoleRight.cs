using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Administration.Models
{
    [Table("Adm" + nameof(RoleRight))]
    public class RoleRight: CoreEntity
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Guid RightId { get; set; }
        public Right Right { get; set; }
    }
}
