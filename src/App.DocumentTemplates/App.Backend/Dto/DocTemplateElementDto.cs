using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    public class DocTemplateElementDto: BaseDto, IOwnedEntity
	{
		public Guid? ParentId { get; set; }
        public virtual DocTemplateElementDto Parent { get; set; }
        public Guid TemplateId { get; set; }
        public virtual DocTemplateDto Template { get; set; }
        [NotMapped]
        public virtual ICollection<DocTemplateElementDto> TemplateElements { get; set; }
        public Guid? GlobalElementId { get; set; }
		public long OrderNumber { get; set; }
		public string ElementTypeCode { get; set; }
		public string ControlTypeCode { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
        public string Config { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? ValuesTreeId { get; set; }        
    }
}
