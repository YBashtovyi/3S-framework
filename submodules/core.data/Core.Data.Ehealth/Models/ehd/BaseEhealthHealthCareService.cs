using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models.ehd
{
    [Table("EhdHealthCareService")]
    public abstract class BaseEhealthHealthCareService: BaseEntity
    {
        /// <summary>
        ///   LicensedUnitId is Id of EhealthLicensedUnit Entity 
        /// </summary>
        public virtual Guid LicensedUnitId { get; set; }
        /// <summary>
        /// Ehealth dictionary with Name  "SPECIALITY_TYPE"
        /// </summary>
        public virtual Guid SpecialityTypeId { get; set; }

        /// <summary>
        /// Ehealth dictionary with Name "PROVIDING_CONDITION"
        /// </summary>
        public virtual Guid ProvidingConditionId { get; set; }
        public virtual string Comment { get; set; }
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

        // Not implemented yet.Reserved for future use
        // Category
        // Type
        // CoverageArea
    }
}
