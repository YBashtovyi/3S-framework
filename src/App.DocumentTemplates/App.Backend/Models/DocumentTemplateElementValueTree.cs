using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    [Table("DtmElementValueTree")]
    public class DocumentTemplateElementValueTree : BaseEntity, IOwnedEntity
    {
        public Guid? OwnerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<DocumentTemplateElementValue> TemplateElementValues { get; set; }
        public virtual ICollection<DocumentTemplateElement> TemplateElements { get; set; }
    }
}
