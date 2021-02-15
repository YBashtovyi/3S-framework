using System;
using System.ComponentModel;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels
{
    public class DocDataVm: BaseDto
    {
        [DisplayName("TempElId")]
        public Guid TemplateElementId { get; set; }
        public string Value { get; set; }
        public Guid? ServiceDocId { get; set; }   // document ID from external system
        public Guid? ServiceOwnerId { get; set; } // Doc template Owner (Owner's Id in Doc Template services)
        public Guid? OriginDbRecordId { get; set; } // Original DataBase Id (Unique DataBase Id)
        public Guid? OriginDbId { get; set; } // Row	Id in Original DataBase

        #region DocTemplateElement fields
        public Guid? ValuesTreeId { get; set; }
        public virtual DocTemplateElementValueTreeVm ValuesTree { get; set; }
        public Guid? ParentId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid? GlobalElementId { get; set; }
        public long  OrderNumber { get; set; }
        public string Order { get; set; }
        public string ElementTypeCode { get; set; }
        public string ControlTypeCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public RecordState RecordState { get; set; }
        public string Config { get; set; }
        #endregion
    }
    
}
