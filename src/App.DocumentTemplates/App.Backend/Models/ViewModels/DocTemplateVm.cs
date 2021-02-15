using System;
using System.Collections.Generic;
using System.Linq;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels
{
	public class DocTemplateVm: BaseDto
	{
		public Guid? ParentId { get; set; }
        public virtual IEnumerable<DocTemplateVm> Templates { get; set; } = Enumerable.Empty<DocTemplateVm>();
        public long? OrderNumber { get; set; }
		public string ClassShortCode { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public RecordState RecordState { get; set; }
        public IEnumerable<DocTemplateElementVm> TemplateElements { get; set; } = Enumerable.Empty<DocTemplateElementVm>();
    }
}
