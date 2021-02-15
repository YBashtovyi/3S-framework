using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    public class TemplateDocumentDto : BaseDto, ISelfReferenced<TemplateDocumentDto>, IGenericEntity<Guid>, IOwnedEntity
    {
        public Guid? TemplateId { get; set; }
        public virtual DocTemplateDto Template { get; set; }
        public Guid? ParentId { get; set; }
        public virtual TemplateDocumentDto Parent { get; set; }
        public string ClassShortCode { get; set; }
        [NotMapped]
        public virtual IEnumerable<DocDataDto> DocumentData { get; set; }
        public Guid? OwnerId { get; set; }
        public string EntityTypeCode { get; set; }
    }
}
