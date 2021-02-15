using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Реєстр рецептів та запитів на рецепт")]
    [Table("EhpPrescription")]
    public abstract class BaseEhealthPrescription: BaseEntity
    {
        public virtual DateTime PrescriptionDate { get; set; }

        public virtual DateTime CourseStartDate { get; set; }

        public virtual DateTime CourseEndDate { get; set; }

        public virtual Guid? ReimbursementProgramId { get; set; }

        public virtual Guid PrescriptionTypeId { get; set; }

        public virtual string EhealthNumber { get; set; }

        public virtual string VerificationCode { get; set; }

        public virtual string Description { get; set; }

        public virtual Guid? PharmacyId { get; set; }

        public virtual Guid ApointmentId { get; set; }
    }
}
