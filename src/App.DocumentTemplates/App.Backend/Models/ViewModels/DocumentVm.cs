using System;
using System.Collections.Generic;
using System.Linq;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.ViewModels
{
    public class DocumentVm: BaseDto, ISelfReferenced<DocumentVm>
    {
        public Guid? TemplateId { get; set; }
        public virtual DocTemplateVm Template { get; set; }
        public Guid? ParentId { get; set; }
        public virtual DocumentVm Parent { get; set; }
        public virtual IEnumerable<DocumentVm> Documents { get; set; } = Enumerable.Empty<DocumentVm>();
        public string ClassShortCode { get; set; }
        public virtual IEnumerable<DocDataVm> DocumentDataList { get; set; } = Enumerable.Empty<DocDataVm>();
        public string EntityTypeCode { get; set; }
        public string AppointmentTypeCode { get; set; }
    }
}
