using System;
using System.ComponentModel.DataAnnotations;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents information about incoming medical referral in E-Health
    /// </summary>
    [Display(Name = "Погашені медичні направлення")]
    public abstract class BaseEhealthIncomingMedicalReferralDto : BaseEhealthMedicalReferralDto
    {
        /// <summary>
        /// Date when when medical referral would be completed in eHealth
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime? CompleteDateInEhealth { get; set; }
    }
}
