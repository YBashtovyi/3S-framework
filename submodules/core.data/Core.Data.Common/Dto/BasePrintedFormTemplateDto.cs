using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Common.Dto
{
    /// <summary>
    /// Represents model of printed form template 
    /// </summary>
    public abstract class BasePrintedFormTemplateDto : BaseDto
    {
        /// <summary>
        /// The name of the main entity used by the template
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string MainEntityName { get; set; }

        /// <summary>
        /// Unique template code
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Template data
        /// </summary>
        public virtual string Template { get; set; }
    }
}
