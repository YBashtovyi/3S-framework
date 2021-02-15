using System;
using System.ComponentModel.DataAnnotations;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Base.Data
{
    public abstract class CoreDto: IGenericEntity<Guid>
    {
        [Display(Name = "Ідентифікатор")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid Id { get; set; }
    }
}
