using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisEncounterServices")]
    [Display(Name = "Взаємодії: послуги")]
    public class BaseEncounterEhealthCatalogService: BaseEntity
    {
        /// <summary>
        /// Provided Encounter
        /// </summary>
        public Guid EncounterId { get; set; }

        /// <summary>
        /// Service that is provided for Encounter
        /// </summary>
        public Guid EhealthServiceCatalogServiceId { get; set; }
    }
}
