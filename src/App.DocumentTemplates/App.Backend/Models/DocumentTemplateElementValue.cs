using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    /// <summary>
    /// Values of DocTemplate elementes
    /// </summary>
    [Table("DtmElementValue")]
    public class DocumentTemplateElementValue : BaseEntity, IOwnedEntity
    {
        public Guid? ParentId { get; set; }
        public virtual DocumentTemplateElementValue Parent { get; set; }
        public virtual ICollection<DocumentTemplateElementValue> TemplateElementValues { get; set;}
        public Guid? TemplateElementId { get; set; }
        public Guid? ValuesTreeId { get; set; }
        public virtual DocumentTemplateElementValueTree ValuesTree { get; set; }
        public string Name { get; set; }
        public string ContentValue { get; set; }
        public long OrderNumber { get; set; }
        public string ValueTypeCode { get; set; }
        public Guid? OwnerId { get; set; }
    }
}



