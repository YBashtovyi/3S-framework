using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Org
{
    [Display(Name = "Адреси організаційних структур")]
    [Table("OrgUnitAtuAddress")]
    public class BaseOrgUnitAtuAddress: CoreEntity
    {
        public virtual Guid? OrgUnitId { get; set; }

        public virtual Guid? AtuAddressId { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public virtual string AddressType { get; set; }

        [MaxLength(250)]
        public virtual string Comment { get; set; }
    }
}
