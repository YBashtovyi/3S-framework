using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthPharmacyDto: BaseDto
    {
        [Display(Name = "Назва аптеки")]
        [Required(ErrorMessage = "Заповніть поле")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }

        [Display(Name = "ЄДРПОУ")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Edrpou { get; set; }

        [Display(Name = "Примітка")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }

        [Display(Name = "Статус")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; } = true;
    }
}
