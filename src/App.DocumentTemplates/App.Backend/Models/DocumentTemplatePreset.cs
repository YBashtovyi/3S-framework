using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    /// <summary>
    /// Presets of templates values
    /// </summary>
    [Table("DtmPreset")]
    public class DocumentTemplatePreset: BaseEntity, IOwnedEntity
    {
        public Guid TemplateId { get; set; }
        public virtual DocumentTemplate Template { get; set; }
        public long? OrderNumber { get; set; }
        public Guid? OwnerId { get; set; }
        public virtual ICollection<DocumentTemplatePresetValue> PresetValues { get; set; }
    }
}
