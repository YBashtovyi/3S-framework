using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Dto
{
    public class DocTemplatePresetDto : BaseDto, IOwnedEntity
    {
        public Guid TemplateId { get; set; }
        public long? OrderNumber { get; set; }
        [NotMapped]
        public ICollection<DocTemplatePresetValueDto> PresetValues { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
