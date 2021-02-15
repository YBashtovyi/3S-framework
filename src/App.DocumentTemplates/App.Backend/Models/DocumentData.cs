using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    [Table("DtmDocumentData")]
    public class DocumentData : BaseEntity, IOwnedEntity
    {
        public Guid TemplateElementId { get; set; }
        public DocumentTemplateElement TemplateElement { get; set; }
        public string Value { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? DocumentId { get; set; }
        public TemplateDocument Document { get; set; }
        public long OrderNumber { get; set; }

        #region DocTemplateElement fields
        public Guid? ValuesTreeId {get; set;}
        public DocumentTemplateElementValueTree ValuesTree { get; set; }
        public Guid? ParentId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid? GlobalElementId { get; set; }
        public string ElementTypeCode { get; set; }
        public string ControlTypeCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
