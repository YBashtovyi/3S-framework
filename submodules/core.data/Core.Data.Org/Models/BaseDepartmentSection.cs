using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;

namespace Core.Data.Org.Models
{
    /// <summary>
    /// Class represents department section - subdivision of the department.
    /// </summary>
    /// <remarks>
    /// The more similar approach can be used within the same class using hierarchical structure (ParentId field in Department)
    /// </remarks>
    public abstract class BaseDepartmentSection: BaseEntity
    {
        /// <summary>
        /// Section short name
        /// </summary>
        [StringLength(100, MinimumLength = 1)]
        public override string Caption { get; set; }

        /// <summary>
        /// Section department id
        /// </summary>
        [RequiredNonDefault]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Official name of the department section
        /// </summary>
        //[StringLength(100, MinimumLength = 1)]
        public virtual string FullName { get; set; }

        /// <summary>
        /// Code of the the department section
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        public virtual string Code { get; set; }

        /// <summary>
        /// ZIP (postal) code of the department section
        /// </summary>
        [StringLength(12)]
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// Address of the section
        /// </summary>
        [StringLength(400)]
        public virtual string Address { get; set; }

        /// <summary>
        /// Contact phones
        /// </summary>
        [StringLength(100)]
        public virtual string Phone { get; set; }

        /// <summary>
        /// Comment, note or description
        /// </summary>
        [StringLength(4000)]
        public virtual string Description { get; set; }
    }
}
