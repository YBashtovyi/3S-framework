using System;
using Core.Base.Data;

namespace Core.Data.Mis.Dto
{
    public class BaseDiagnosticReportDto: BaseDocumentDto
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Guid MedicalReferralCategoryId { get; set; }
        public virtual string MedicalReferralCategoryCaption { get; set; }
        
        /// <summary>
        /// EhealthDictionary "name": "eHealth/diagnostic_report_categories" 
        /// </summary>
        public virtual Guid DiagnosticReportCategoryId { get; set; }
        public virtual string DiagnosticReportCategoryCaption { get; set; }
        
        /// <summary>
        /// Service that is provided for Patient
        /// </summary>
        public virtual Guid EhealthServiceCatalogServiceId { get; set; }
        public virtual string EhealthServiceCatalogServiceCaption { get; set; }
        
        /// <summary>
        /// Первинне джерело даних("зі слів пацієнта/ запис в документації/зі слів пов'язаної особи")
        /// EhealthDictionary     "name": "eHealth/report_origins" 
        ///  used in Ehealth with  name: "report_origin"
        /// </summary>
        public virtual Guid? DiagnosticReportOriginId { get; set; }
        public virtual string DiagnosticReportOriginCaption { get; set; }
        
        /// <summary>
        /// Первинне джерело даних
        /// </summary>
        public virtual bool IsPrimarySource { get; set; }
        
        public virtual Guid PatientCardId { get; set; }
        public virtual string PatientCardCaption { get; set; }
        
        /// <summary>
        /// Employee who process results
        /// EhealthEmployee "EmployeeTypeCode" = "DOCTOR"/"SPECIALIST"  
        /// </summary>
        public virtual Guid? ResultsInterpreterId { get; set; }
        public virtual string ResultsInterpreterCaption { get; set; }
        
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
        public virtual string RecordedByCaption { get; set; }
        
        /// <summary>
        /// Employee who make diagnosis 
        ///  EhealthEmployee "EmployeeTypeCode" = "DOCTOR"/"SPECIALIST"  
        /// </summary>
        public virtual Guid? PerformerId { get; set; }
        public virtual string PerformerCaption { get; set; }
        
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
        public string DivisionCaption { get; set; }
        
        /// <summary>
        /// Registred In Ehealth
        /// </summary>
        public virtual Guid? EhealthId { get; set; }
        
        /// <summary>
        /// OrganizationId registred in E-Health
        /// </summary>
        public virtual Guid LegalEntityId { get; set; }

    }
}
