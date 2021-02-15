using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Org.Dto
{
    /// <summary>
    /// Dto for <see cref="BaseDepartmentSectionDto"/>
    /// </summary>
    public abstract class BaseDepartmentSectionDto: BaseDto
    {
        /// <summary>
        /// Section short name
        /// </summary>
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public override string Caption { get; set; }

        /// <summary>
        /// Section department id
        /// </summary>
        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Official name of the department section
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        //[StringLength(100, MinimumLength = 1)]
        //[Required]
        public virtual string FullName { get; set; }

        /// <summary>
        /// Code of the the department section
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public virtual string Code { get; set; }

        /// <summary>
        /// ZIP (postal) code of the department section
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        [StringLength(12)]
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// Address of the section
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        [StringLength(400)]
        public virtual string Address { get; set; }

        /// <summary>
        /// Contact phones
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        [StringLength(100)]
        public virtual string Phone { get; set; }

        /// <summary>
        /// Comment, note or description
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        [StringLength(4000)]
        public virtual string Description { get; set; }
    }
}
