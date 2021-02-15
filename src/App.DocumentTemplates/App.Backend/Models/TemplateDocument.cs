using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    //document storage system
    [Table("DtmDocument")]
    public class TemplateDocument : BaseEntity, ISelfReferenced<TemplateDocument>, IEntity, IOwnedEntity
    {
        public Guid? TemplateId { get; set; }
        public DocumentTemplate Template { get; set; }
        public Guid? ParentId { get; set; }
        [InverseProperty("Documents")]
        public TemplateDocument Parent { get; set; }
        public ICollection<TemplateDocument> Documents { get; set; }
        public string ClassShortCode { get; set; }
        public ICollection<DocumentData> DocumentDataList { get; set; }
        public string Name { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid EntityId { get; set; }
        public string EntityTypeCode { get; set; }
    }
}
