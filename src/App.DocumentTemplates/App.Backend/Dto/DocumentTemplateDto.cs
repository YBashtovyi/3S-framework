using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.DocumentTemplates.Dto
{
    public abstract class BaseDocumentTemplateDto: BaseDto
    {
        // an entity name the templates belongs to
        public virtual string EntityName { get; set; }
        // templates can be grouped into folders
        public virtual Guid? ParentId { get; set; }
        
        // position in the list
        // TODO: change to int
        public long? OrderNumber { get; set; }
        // TODO: find out, what is it. probably should be deleted
        public string ClassShortCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        // owner that has rights on this template (organization id, for example)
        public Guid? OwnerId { get; set; }
        // here is a prepared document template will be stored (docx, for example)
        public string TemplatePath { get; set; }
    }

    [NotMapped]
    public class DocumentTemplateDetailDto: BaseDocumentTemplateDto
    {
        // elements, that the template uses
        [NotMapped]
        public virtual ICollection<DocumentTemplateDetailDto> Templates { get; set; }
        //public virtual ICollection<DocumentTemplateElement> TemplateElements { get; set; }
        // TODO: what is it?
        //public virtual ICollection<DocumentTemplatePreset> Preset { get; set; }
        public virtual string ParentCaption { get; set; }
        public string OwnerCaption { get; set; }
    }

    [NotMapped]
    public class DocumentTemplateListDto: BaseDocumentTemplateDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }
        public virtual string ParentCaption { get; set; }
        public string OwnerCaption { get; set; }
    }
}
