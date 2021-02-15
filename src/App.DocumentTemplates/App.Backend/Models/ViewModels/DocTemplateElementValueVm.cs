using System;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels
{
	public class DocTemplateElementValueVm: BaseDto
	{
		public Guid? TemplateElementId { get; set; }
        public Guid? ValuesTreeId { get; set; }
        public Guid? ParentId { get; set; }
		public string ContentValue { get; set; }
		public RecordState RecordState { get; set; }
		public long OrderNumber { get; set; }
		public string ValueTypeCode { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? OriginalDbId { get; set; }  // Original DataBase Id (Unique DataBase Id)
		public Guid? OriginalRowId { get; set; } // Row	Id in Original DataBase
	    public int ChildsCount { get; set; }
		
	}
}
