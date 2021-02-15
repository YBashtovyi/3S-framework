using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    [Table("DtmTemplatePresetValue")]
    public class DocumentTemplatePresetValue: BaseEntity, IOwnedEntity
    {
        public Guid PresetId { get; set; }
        public virtual DocumentTemplatePreset Preset { get; set; }
        public string Value { get; set; }
        public Guid TemplateElementId { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
