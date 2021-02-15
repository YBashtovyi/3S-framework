using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Adm
{
    [Table("AdmUser")]
    public class BaseUser: CoreEntity
    {
        public virtual Guid EmployeeId { get; set; }

        /// <summary>
        /// Id of the Identity
        /// </summary>
        public virtual Guid AccountId { get; set; }

        [MaxLength(100)]
        public virtual string Login { get; set; }
    }
}
