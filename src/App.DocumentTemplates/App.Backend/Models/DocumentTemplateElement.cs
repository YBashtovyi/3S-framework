using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    /// <summary>
    /// Elements of DocTemplates
    /// </summary>
    [Table("DtmTemplateElement")]
    public class DocumentTemplateElement: BaseEntity,IOwnedEntity
    {
        public Guid? ParentId { get; set; }
        public virtual DocumentTemplateElement Parent { get; set; }
        public Guid TemplateId { get; set; }
        public virtual DocumentTemplate Template { get; set; }
        public virtual ICollection<DocumentTemplateElement> TemplateElements { get; set; }
        public Guid? GlobalElementId { get; set; }
        public long OrderNumber { get; set; }
        public string ElementTypeCode { get; set; }
        public string Code { get; set; }
        public string ControlTypeCode { get; set; }
        public string  Config { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? ValuesTreeId { get; set; }
        public virtual DocumentTemplateElementValueTree ValuesTree { get; set; }
    }
}



