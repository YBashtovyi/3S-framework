using System.ComponentModel.DataAnnotations;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Base.Data
{
    public abstract class BaseDictionaryDto : BaseDto
    {
        [Display(Name = "Код")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Code { get; set; }

        [Display(Name = "Назва")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Name { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Caption { get; set; }
    }
}
