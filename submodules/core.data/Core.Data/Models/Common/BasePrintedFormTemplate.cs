using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Models.Common
{
    /// <summary>
    /// Represents model of printed form template 
    /// </summary>
    [Display(Name = "Довідник шаблонів друкованих форм")]
    public abstract class BasePrintedFormTemplate : CoreEntity, ICaption
    {
        public virtual string Caption { get; set; }

        /// <summary>
        /// The name of the main entity used by the template
        /// </summary>
        [MaxLength(128)]
        public virtual string MainEntityName { get; set; }

        /// <summary>
        /// Unique template code
        /// </summary>
        [MaxLength(64)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Template data
        /// </summary>
        public virtual string Template { get; set; }
    }
}
