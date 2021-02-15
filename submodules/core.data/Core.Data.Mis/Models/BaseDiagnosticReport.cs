using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisDiagnosticReport")]
    public class BaseDiagnosticReport: BaseDocument
    {
        /// <summary>
        /// EhealthDictionary "name": "eHealth/diagnostic_report_categories" 
        /// </summary>
        public virtual Guid? DiagnosticReportCategoryId { get; set; }

        /// <summary>
        /// Service that is provided for Patient
        /// </summary>
        public virtual Guid EhealthServiceCatalogServiceId { get; set; }

        /// <summary>
        /// Первинне джерело даних("зі слів пацієнта/ запис в документації/зі слів пов'язаної особи")
        /// EhealthDictionary     "name": "eHealth/report_origins" 
        ///  used in Ehealth with  name: "report_origin"
        /// </summary>
        public Guid? DiagnosticReportOriginId { get; set; }

        /// <summary>
        /// Первинне джерело даних
        /// </summary>
        public virtual bool IsPrimarySource { get; set; }

        public virtual Guid PatientCardId { get; set; }

        /// <summary>
        /// Employee who process results
        /// EhealthEmployee "EmployeeTypeCode" = "DOCTOR"/"SPECIALIST"  
        /// </summary>
        public virtual Guid? ResultsInterpreterId { get; set; }

        /// <summary>
        /// DateTime when was digital signed
        /// Час накладення ЕЦП
        /// </summary>
        public virtual DateTime? IssuedAt { get; set; }

        /// <summary>
        ///  Employee who transfer to Ehealth
        ///  EhealthEmployee "EmployeeTypeCode" = "DOCTOR"/"SPECIALIST"  
        /// </summary>
        public virtual Guid? RecordedById { get; set; }

        /// <summary>
        /// Employee who make diagnosis 
        ///  EhealthEmployee "EmployeeTypeCode" = "DOCTOR"/"SPECIALIST"  
        /// </summary>
        public Guid? PerformerId { get; set; }

        /// <summary>
        /// Appointment Start Datetime
        /// Used for compose effective_period
        /// </summary>
        public DateTime? EffectiveDateTimeStart { get; set; }

        /// <summary>
        ///  Appointment End Datetime
        ///  Used for compose effective_period
        /// </summary>
        public DateTime? EffectiveDateTimeEnd { get; set; }

        /// <summary>
        /// Посилання на підрозділ організації зареєстрованої в E-Health
        /// Можливо: "Підрозділ первинної мед.допомоги"/ "Підрозділ вторинної мед.допомоги"
        /// </summary>
        public virtual Guid DivisionId { get; set; }

        /// <summary>
        /// OrganizationId registred in E-Health
        /// </summary>
        public virtual Guid LegalEntityId { get; set; }

        /// <summary>
        /// EnumRecord EnumType = "MedicalDocumentType"
        /// </summary>
        public virtual Guid DocumentTypeId { get; set; }

        /// <summary>
        /// Registred In Ehealth
        /// </summary>
        public virtual Guid? EhealthId { get; set; }
    }
}
