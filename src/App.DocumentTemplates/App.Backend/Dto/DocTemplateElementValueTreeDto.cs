using System;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    public class DocTemplateElementValueTreeDto: CoreDto, IOwnedEntity
    {
        public Guid? OwnerId { get; set; }
        public string Caption { get; set; }
        public string Code { get; set; }
    }
}
