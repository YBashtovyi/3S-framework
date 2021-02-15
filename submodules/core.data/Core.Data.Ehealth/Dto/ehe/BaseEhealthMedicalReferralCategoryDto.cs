using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Reference book of medical referral categories
    /// </summary>
    public abstract class BaseEhealthMedicalReferralCategoryDto: BaseDto
    {
        /// <summary>
        /// Medical referral category code in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Medical referral category name in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Medical referral category code in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string EhealthCode { get; set; }

        /// <summary>
        /// Sign that this medical referral category is present in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsUsedInEhealth { get; set; }
    }
}
