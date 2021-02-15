using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name= "Контракт між клінікою та НСЗУ")]
    [Table("EhdContract")]
    public abstract class BaseEhealthContract: BaseEntity
    {
        public virtual Guid? ContractorOwnerId { get; set; }
        public virtual string ContractorBase { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string IdFormCode { get; set; }
        public virtual string ContractNumber { get; set; }
        public virtual string StatuteMd5 { get; set; }
        public virtual string AdditionalDocumentMd5 { get; set; }
        public virtual string ConsentText { get; set; }
        public virtual int ContractorRmspAmount { get; set; }
        public virtual Guid? PreviousRequestId { get; set; }
        public virtual Guid? EhealthId { get; set; }
        public virtual bool ExternalContractorFlag { get; set; }
    }
}
