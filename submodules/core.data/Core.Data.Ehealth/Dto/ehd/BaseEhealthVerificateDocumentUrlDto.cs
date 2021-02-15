using System;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthVerificateDocumentUrlDto: BaseDto
    {
        public virtual string Url { get; set; }
        public virtual string Type { get; set; }
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EntityId { get; set; }
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual bool IsUploaded { get; set; }
    }
}
