using System;
using System.Collections.Generic;
using System.Linq;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels
{
    public class DocTemplateElementValueTreeVm: BaseDto
    {
        public Guid? OwnerId { get; set; }
        public string Code { get; set; }
        public IEnumerable<DocTemplateElementValueVm> TemplateElementValues { get; set; } = Enumerable.Empty<DocTemplateElementValueVm>();
    }
}
