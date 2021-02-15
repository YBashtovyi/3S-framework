using System;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels
{
    public class DocTemplatePresetValueVm: CoreDto
    {
        public Guid PresetId { get; set; }
        public DocTemplatePresetVm Preset { get; set; }
        public string Value { get; set; }
        public Guid TemplateElementId { get; set; }
    }
}
