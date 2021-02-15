using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Реєстр програм реімбурсації")]
    [Table("EhpReimbursementPrograms")]
    public abstract class BaseReimbursementProgram: BaseEntity
    {
        public virtual string Code { get; set; }

        public virtual bool IsEhealthProgram { get; set; }

        public virtual Guid? EhealthMedicalServiceProgramId { get; set; }

        public virtual DateTime StartDate { get; set; }
        
        public virtual DateTime? EndDate { get; set; }

        public virtual Guid ActivityStatusId { get; set; }

        public virtual string Description { get; set; }

        public virtual Guid MedicineSearchTypeId { get; set; }

        public virtual int PrescriptionMaxItemsCount { get; set; }

        public virtual int MaxCourseDuration { get; set; }

        public virtual bool IsPharmacyDesirable { get; set; }

        public virtual bool ProhibitSavingSameInn { get; set; }
    }
}
