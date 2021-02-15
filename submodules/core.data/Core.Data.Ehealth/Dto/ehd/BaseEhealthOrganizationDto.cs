using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthOrganizationDto: BaseDto
    {
        [Display(Name = "Код ЄДРПОУ")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Edrpou { get; set; }
        [Display(Name = "Тип оранізації")]
        public virtual string TypeCode { get; set; }
        [Display(Name = "E-mail клініки")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string Email { get; set; }
        [Display(Name = "Сайт клініки")]
        public virtual string Website { get; set; }
        [Display(Name = "Код одержувача/розпорядника бюджетних коштів для Казначейства.")]
        public virtual string ReceiverFundsCode { get; set; }
        [Display(Name = "Інформація про власника ЗОЗ")]
        public virtual string Beneficiary { get; set; }       
        public virtual string SecurityRedirectUri { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string PublicOfferConsentText { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual bool PublicOfferConsent { get; set; }
        public virtual Guid? EhealthId { get; set; }
    }
}
