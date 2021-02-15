using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Represents relation between service and group in service catalog
    /// </summary>
    [Display(Name = "Зв'язок послуги з групою в каталогі послуг")]
    public abstract class BaseEhealthServiceCatalogServiceInGroup : BaseEntity
    {
        /// <summary>
        /// Service id in service catalog
        /// </summary>
        public virtual Guid ServiceId { get; set; }

        /// <summary>
        /// Group id in service catalog
        /// </summary>
        public virtual Guid GroupId { get; set; }
    }
}
