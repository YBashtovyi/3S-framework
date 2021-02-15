﻿using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents which program contains the service catalog element
    /// </summary>
    public abstract class BaseEhealthParticipationInMedicalProgramDto: BaseDto
    {
        /// <summary>
        /// Medical service program id in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid MedicalServiceProgramId { get; set; }

        /// <summary>
        /// Service catalog group id in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ServiceCatalogGroupId { get; set; }

        /// <summary>
        /// Service catalog service id in application
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? ServiceCatalogServiceId { get; set; }

        /// <summary>
        /// Indication that the participation in medical program is active or inactive
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Indication that the participation in medical program is allowed in the medical referral
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool RequestAllowed { get; set; }

        /// <summary>
        /// Consumer price of participation in medical program 
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual decimal ConsumerPrice { get; set; }

        /// <summary>
        /// Description of participation in medical program 
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Date of creation in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)] 
        public virtual DateTime InsertedAtInEhealth { get; set; }

        /// <summary>
        /// Date of updated in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.InputRange)] 
        public virtual DateTime? UpdatedAtInEhealth { get; set; }

        /// <summary>
        /// Identifier of participation in medical program in E-Health
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)] 
        public virtual Guid? EhealthId { get; set; }
    }
}