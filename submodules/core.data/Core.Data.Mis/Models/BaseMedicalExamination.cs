using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisMedicalExamination")]
    public abstract class BaseMedicalExamination: BaseDocument
    {
        [MaxLength(2000)]
        public virtual string Comment { get; set; }

        /// <summary>
        /// EnumRecord EnumType = "MedicalDocumentType"
        /// </summary>
        public virtual Guid DocumentTypeId { get; set; }

        public virtual Guid EmployeeId { get; set; }

        public virtual Guid PatientCardId { get; set; }

        /// <summary>
        /// Ehealth employee id in application
        /// </summary>
        public virtual Guid EhealthEmployeeId { get; set; }
    }
}
