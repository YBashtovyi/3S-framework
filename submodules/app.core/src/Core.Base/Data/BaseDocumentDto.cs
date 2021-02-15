using System;
using System.ComponentModel.DataAnnotations;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Base.Data
{
    public abstract class BaseDocumentDto: BaseDto
    {
        [Display(Name = "Номер")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string RegNumber { get; set; }

        [Display(Name = "Дата")]
        [DocumentDate]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime RegDate { get; set; }

        [Display(Name = "Опис")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }
    }
}
