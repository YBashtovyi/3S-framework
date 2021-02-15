using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Org.Models
{
    [Display(Name = "Організація")]
    [Table("OrgOrganization")]
    public abstract class BaseOrganization : BaseEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public Type BaseType => typeof(BaseOrgUnit);
        #endregion

        [Display(Name = "Повна назва")]
        public virtual string FullName { get; set; }
        [MaxLength(400)]
        public virtual string Code { get; set; }
        public virtual Guid? ParentId { get; set; }
        [MaxLength(100)]
        public virtual string Description { get; set; }
    }
}
