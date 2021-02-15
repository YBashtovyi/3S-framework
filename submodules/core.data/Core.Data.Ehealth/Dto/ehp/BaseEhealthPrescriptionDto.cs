using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Enums;
using Core.Common.Attributes;
using System;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthPrescriptionDto: BaseDto
    {        

        [Display(Name = "Дата створення рецепта")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime PrescriptionDate { get; set; }

        [Display(Name = "Дата початку курсу")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime CourseStartDate { get; set; }

        [Display(Name = "Дата закінчення курсу")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual DateTime CourseEndDate { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ReimbursementProgramId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid PrescriptionTypeId { get; set; }

        [Display(Name = "№ рецепта")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string EhealthNumber { get; set; }

        [Display(Name = "Код верифікації")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string VerificationCode { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? PharmacyId { get; set; }

        [RequiredNonDefault]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid ApointmentId { get; set; }

    }
}
