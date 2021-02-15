using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Org.Models
{
    [Display(Name = "Базова організація")]
    [Table("OrgUnit")]
    public abstract class BaseOrgUnit : BaseEntity, IDerivableEntity
    {
        /// <summary>
        /// Concrete document type. Usually, nameof(SomeEntityClass)
        /// </summary>
        public string DerivedEntity { get; set; }

        [MaxLength(100)]
        public virtual string Name { get; set; }
        [MaxLength(20)]
        public virtual string Code { get; set; }
        public virtual Guid? ParentId { get; set; }
    }
}
