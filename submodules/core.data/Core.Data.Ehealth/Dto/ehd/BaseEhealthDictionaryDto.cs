using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents ehealth dictionary in the application
    /// </summary>
    public abstract class BaseEhealthDictionaryDto: BaseDto
    {
        /// <summary>
        /// Ehealth dictionary name
        /// </summary>
        /// <remarks>
        /// In eHealth this dictionary actually stores many different dictionaries
        /// Name property differs one dictionary from another.
        /// </remarks>
        [Display(Name = "Назва довідника")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Ehealth can deactivate some values if they are not needed anymore
        /// </summary>
        [Display(Name = "Активний")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; } = false;

        /// <summary>
        /// Code in eHealth. Is unique for every dictionary name
        /// </summary>
        [Display(Name = "Код")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Value of dictionary. In general this value should be displayed as caption
        /// </summary>
        [Display(Name = "Значення")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Value { get; set; }
    }
}
