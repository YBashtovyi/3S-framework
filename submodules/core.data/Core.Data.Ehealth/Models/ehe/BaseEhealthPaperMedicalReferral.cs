using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents information paper medical referral
    /// </summary>
    [Display(Name = "Паперове направлення")]
    public abstract class BaseEhealthPaperMedicalReferral : BaseDocument
    {
        /// <summary>
        /// Organization edrpou code from which paperer medical referral was created
        /// </summary>
        [MaxLength(50)]
        public virtual string RequesterLegalEntityEdrpou { get; set; }
        
        /// <summary>
        /// Organization name from which create paper medical referral was created
        /// </summary>
        public string RequesterLegalEntityName { get; set; }
        
        /// <summary>
        /// Employee name who created paper medical referral
        /// </summary>
        public string RequesterEmployeeName { get; set; }
    }
}
