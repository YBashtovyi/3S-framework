using System;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    public class DocDataDto: CoreDto, IOwnedEntity
    {
        public Guid TemplateElementId { get; set; }
        public string Value { get; set; }

        #region DocTemplateElement fields
        public long? ValuesTreeId { get; set; }
        public virtual DocTemplateElementValueTreeDto ValuesTree { get; set; }
        public Guid? ParentId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid? GlobalElementId { get; set; }
        public long OrderNumber { get; set; }
        public string ElementTypeCode { get; set; }
        public string ControlTypeCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        #endregion
    }
}
