using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    /// <summary>
    /// Module of User Doc Templates  (UDT)
    /// Templates of user documents
    /// </summary>
	[Table("DtmTemplate")]
    public class DocumentTemplate: BaseEntity, IOwnedEntity, ISelfReferenced<DocumentTemplate>
    {
        public string EntityName { get; set; }
        // templates can be grouped into folders
        public Guid? ParentId { get; set; }
        [InverseProperty("Templates")]
        public virtual DocumentTemplate Parent { get; set; }
        public virtual ICollection<DocumentTemplate> Templates { get; set; }
        // TODO: delete
        public long? OrderNumber { get; set; }
        // TODO: find out, what is it. probably should be deleted
        public string ClassShortCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        // elements, that the template uses
        public virtual ICollection<DocumentTemplateElement> TemplateElements { get; set; }
        // TODO: what is it?
        public virtual ICollection<DocumentTemplatePreset> Preset { get; set; }
        public Guid? OwnerId { get; set; }
        // here is a prepared document template will be stored (docx, for example)
        //public byte[] Template { get; set; }
        public string TemplatePath { get; set; }
    }
}
