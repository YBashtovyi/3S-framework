using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Org.Models
{
    /// <summary>
    /// Represents the base model for the department
    /// </summary>
    [Display(Name = "Підрозділ")]
    [Table("OrgDepartment")]
    public abstract class BaseDepartment : BaseEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public Type BaseType => typeof(BaseOrgUnit);
        #endregion

        /// <summary>
        /// Full name of the department
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Code of the department
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Id of the org structure which department relates to
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// Description(notes) of the department
        /// </summary>
        [MaxLength(100)]
        public virtual string Description { get; set; }

        /// <summary>
        /// ZIP (postal) code of the department
        /// </summary>
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// Category of the department
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// Location (address) of the department
        /// </summary>
        public virtual string Location { get; set; }
    }
}
