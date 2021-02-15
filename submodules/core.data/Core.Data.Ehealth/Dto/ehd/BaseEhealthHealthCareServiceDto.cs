using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthHealthCareServiceDto: BaseDto
    {
        /// <summary>
        ///  Id of LicensedUnit registred in your System 
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid LicensedUnitId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid SpecialityTypeId { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ProvidingConditionId { get; set; }

        [Display(Name = "Коментар")]
        public string Comment { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }

        public virtual Guid? EhealthId { get; set; }

        /// <summary>
        /// Status of E-Health update
        /// Defaults to <c>false</c>;
        /// </summary>
        /// <remarks>
        /// Set false for mark this Entity as require for synchronize with E-Health
        /// </remarks>
        public bool IsSyncedWithEhealth { get; set; }
    }
}
