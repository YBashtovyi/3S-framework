using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Відшкодування ЛЗ за програмою")]
    [Table("EhpMedicineReimbursement")]
    public abstract class BaseEhealthMedicineReimbursement: BaseEntity
    {
        public virtual Guid ReimbursementProgramId { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual Guid ActivityStatusId { get; set; }

        public virtual double WholesalePrice { get; set; }

        public virtual double RetailPrice { get; set; }

        public virtual double CompensationAmount { get; set; }

        public virtual Guid MedicineId { get; set; }

        public virtual int MinimalRealizationAmount { get; set; }
    }
}
