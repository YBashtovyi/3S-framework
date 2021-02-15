using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents external division in E-Health
    /// </summary>
    public abstract class BaseEhealthExternalDivisionDto : BaseDto
    {
        /// <summary>
        /// Division name in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Division type in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid TypeId { get; set; }

        /// <summary>
        /// Division id in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EhealthId { get; set; }
    }
}
