using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents information about incoming medical referral in E-Health
    /// </summary>
    [Display(Name = "Погашені медичні направлення")]
    public abstract class BaseEhealthIncomingMedicalReferral : BaseEhealthMedicalReferral
    {
        /// <summary>
        /// Date when when medical referral would be completed in eHealth
        /// </summary>
        public virtual DateTime? CompleteDateInEhealth { get; set; }
    }
}
