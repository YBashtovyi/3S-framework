using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthDeclarationDto: BaseDto
    {
        [Display(Name = "Підрозділ")]
        public virtual Guid? DivisionId { get; set; }
        [Display(Name = "Лікар")]
        public virtual Guid? EmployeeId { get; set; }
        public virtual string Scope { get; set; } = "family_doctor";
        public virtual bool Overlimit { get; set; }
        public virtual Guid? EhealthId { get; set; }
    }
}
