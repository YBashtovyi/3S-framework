using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Reference book of medical service program in E-Health
    /// </summary>
    public abstract class BaseEhealthMedicalServiceProgramDto : BaseDto
    {
        /// <summary>
        /// Sign, that program is active in eHealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Hidden, shows if record is hidden on the ui
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool Hidden { get; set; }

        /// <summary>
        /// Medical service program id in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EhealthId { get; set; }
    }
}
