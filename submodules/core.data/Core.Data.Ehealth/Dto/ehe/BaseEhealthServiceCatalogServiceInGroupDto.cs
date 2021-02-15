using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Represents relation between service and group in service catalog
    /// </summary>
    public abstract class BaseEhealthServiceCatalogServiceInGroupDto : BaseDto
    {
        /// <summary>
        /// Service id in service catalog
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ServiceId { get; set; }

        /// <summary>
        /// Group id in service catalog
        /// </summary>
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid GroupId { get; set; }
    }
}
