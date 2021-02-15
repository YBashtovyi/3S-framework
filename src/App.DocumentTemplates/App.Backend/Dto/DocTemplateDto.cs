using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    [NotMapped]
    public class DocTemplateDto: BaseDto, IOwnedEntity
    {
		public Guid? ParentId { get; set; }
		public long? OrderNumber { get; set; }
		public string ClassShortCode { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
        [NotMapped]
        public ICollection<DocTemplateElementDto> TemplateElements { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
