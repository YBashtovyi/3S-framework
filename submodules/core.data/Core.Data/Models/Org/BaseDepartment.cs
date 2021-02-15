using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;


namespace Core.Data.Models.Org
{
    /// <summary>
    /// Represents the base model for the department
    /// </summary>
    [Display(Name = "Підрозділ")]
    [Table("OrgDepartment")]
    public abstract class BaseDepartment : CoreEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public virtual Type BaseType => typeof(BaseOrgUnit);
        #endregion

        /// <summary>
        /// Name of the department
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Full name of the department
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        public virtual string FullName { get; set; }

        /// <summary>
        /// Code of the department
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Description(notes) of the department
        /// </summary>
        [MaxLength(500)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Id of the org structure which department relates to
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// Type of the department <see cref="BaseEnumRecord"/>
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        public string DepartmentType { get; set; }

    }
}
