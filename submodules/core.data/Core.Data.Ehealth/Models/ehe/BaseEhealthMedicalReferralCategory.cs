using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Reference book of medical referral categories
    /// </summary>
    [Display(Name = "Класифікатор категорій направлень")]
    [Table("EheMedicalReferralCategory")]
    public abstract class BaseEhealthMedicalReferralCategory: BaseEntity
    {
        /// <summary>
        /// Medical referral category code in application
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Medical referral category name in application
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Medical referral category code in E-Health
        /// </summary>
        public virtual string EhealthCode { get; set; }

        /// <summary>
        /// Sign that this medical referral category is present in E-Health
        /// </summary>
        public virtual bool IsUsedInEhealth { get; set; }
    }
}
