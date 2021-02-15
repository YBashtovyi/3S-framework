using System;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    public class DocTemplatePresetValueDto: CoreDto, IOwnedEntity
    {
        public Guid PresetId { get; set; }
        public string Value { get; set; }
        public Guid TemplateElementId { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
