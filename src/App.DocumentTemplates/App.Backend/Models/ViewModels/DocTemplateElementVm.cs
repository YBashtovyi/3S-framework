using System;
using System.Collections.Generic;
using System.Linq;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels
{
	public class DocTemplateElementVm: BaseDto
	{
        public Guid? ParentId { get; set; }
        public Guid TemplateId { get; set; }
		public Guid? GlobalElementId { get; set; }
		public long OrderNumber { get; set; }
		public string ElementTypeCode { get; set; }
		public string ControlTypeCode { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public RecordState RecordState { get; set; }
        public string Config { get; set; }
        public Guid? ValuesTreeId { get; set; }
        public IEnumerable<DocTemplateElementVm> TemplateElements { get; set; } = Enumerable.Empty<DocTemplateElementVm>();
    }
}
