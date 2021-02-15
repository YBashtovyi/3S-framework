using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents external organization in E-Health
    /// </summary>
    public abstract class BaseEhealthExternalOrganizationDto: BaseDto
    {
        /// <summary>
        /// Organiation name in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Organiation short name in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string ShortName { get; set; }

        /// <summary>
        /// Organization type in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid TypeId { get; set; }

        /// <summary>
        /// Organization edrpou code
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Edrpou { get; set; }

        /// <summary>
        /// Organization email
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Email { get; set; }

        /// <summary>
        /// Sign, that the organization is NHS-certified
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool NhsVerified { get; set; }

        /// <summary>
        /// Organization id in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EhealthId { get; set; }
    }
}
