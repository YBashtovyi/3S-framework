using System;
using System.Collections.Generic;
using System.Linq;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels { 

    public class DocTemplatePresetVm: BaseDto
	{
		public Guid TemplateId { get; set; }
		public RecordState RecordState { get; set; }
		public long? OrderNumber { get; set; }
        public IEnumerable<DocTemplatePresetValueVm> PresetValues { get; set; } = Enumerable.Empty<DocTemplatePresetValueVm>();
	    public Guid? OwnerId { get; set; }  // Doc template Owner (Owner's Id in Doc Template services)
	    public Guid? OriginDbId { get; set; }  // Original DataBase Id (Unique DataBase Id)
	    public Guid? OriginDbRecordId { get; set; } // Row	Id in Original DataBase
    }
}
