using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Mis.Dto
{
    public abstract class BaseAppointmentDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid OrganizationId { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        [Display(Name = "Дата початку")]
        public virtual DateTime? StartDate { get; set; }
        
        [CaseFilter(CaseFilterOperation.InputRange)]
        [Display(Name = "Дата завершення")]
        public virtual DateTime? EndDate { get; set; }

        [Display(Name = "Стан")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid StateId { get; set; }

        [Display(Name = "Тип взаємодії з пацієнтом")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid? InteractionTypeId { get; set; }

        [Display(Name = "Картка пацієнта")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid PatientCardId { get; set; }

        [Display(Name = "Лікар")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EmployeeId { get; set; }

        [Display(Name = "Коментар")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }
    }
}
