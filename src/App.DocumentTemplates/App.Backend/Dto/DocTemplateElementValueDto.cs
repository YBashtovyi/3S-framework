using System;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    public class DocTemplateElementValueDto: BaseDto, IOwnedEntity
    {
		public Guid? TemplateElementId { get; set; }
        public Guid? ValuesTreeId { get; set; }
		public Guid? ParentId { get; set; }
		public string ContentValue { get; set; }
		public long OrderNumber { get; set; }
		public string ValueTypeCode { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
