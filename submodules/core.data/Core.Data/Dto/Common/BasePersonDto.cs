using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Common
{
    public abstract class BasePersonDto : BaseDto
    {
        [Display(Name = "ПІБ"), CaseFilter(CaseFilterOperation.Contains)]
        public override string Caption { get; set; }

        [Display(Name = "Ім'я"), CaseFilter(CaseFilterOperation.Contains)]
        public virtual string FirstName { get; set; }

        [Display(Name = "По батькові"), CaseFilter(CaseFilterOperation.Contains)]
        public virtual string MiddleName { get; set; }

        [Display(Name = "Прізвище"), CaseFilter(CaseFilterOperation.Contains)]
        public virtual string LastName { get; set; }

        [Display(Name = "Стать"), CaseFilter]
        public virtual string Gender { get; set; }

        [Display(Name = "Дата народження"), CaseFilter(CaseFilterOperation.ValueRange)]
        public virtual DateTime? Birthday { get; set; }

        [Display(Name = "Індивідуальний податковий номер (ІПН)"), CaseFilter(CaseFilterOperation.Contains)]
        public virtual string TaxNumber { get; set; }

        [Display(Name = "Чи відсутній ІПН"), CaseFilter]
        public virtual bool NoTaxNumber { get; set; }

        [Display(Name = "Документ, що посвідчує особу"), CaseFilter]
        public virtual string IdentityDocument { get; set; }
    }
}
