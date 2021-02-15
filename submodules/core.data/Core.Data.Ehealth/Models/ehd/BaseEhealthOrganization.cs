using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Ehealth organization (Legal entity in terms of eHealth)
    /// </summary>
    [Display(Name = "Організація eHealth")]
    [Table("EhdOrganization")]
    public abstract class BaseEhealthOrganization: BaseEntity
    {
        public virtual string Edrpou { get; set; }
        public virtual string TypeCode { get; set; }
        public virtual string Email { get; set; }
        public virtual string Website { get; set; }
        public virtual string ReceiverFundsCode { get; set; }
        public virtual string Beneficiary { get; set; }
        public virtual string SecurityRedirectUri { get; set; }
        public virtual string PublicOfferConsentText { get; set; }
        public virtual bool PublicOfferConsent { get; set; }

        /// <summary>
        /// Id of an organization in eHealth
        /// Is set by eHealth, when organization is registered there
        /// </summary>
        public virtual Guid? EhealthId { get; set; }
    }
}
