using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthContractDto: BaseDto
    {
        [Display(Name = "Підписант")]
        public virtual Guid? ContractorOwnerId { get; set; }
        [Display(Name = "Діє на підставі")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string ContractorBase { get; set; }
        [Display(Name = "Період контракту з")]
        [DataType(DataType.Date)]
        public virtual DateTime? StartDate { get; set; }
        [Display(Name = "Період контракту по")]
        [DataType(DataType.Date)]
        public virtual DateTime? EndDate { get; set; }
        [Display(Name = "Вид договору")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string IdFormCode { get; set; }
        [Display(Name = "Номер")]
        public virtual string ContractNumber { get; set; }
        public virtual string StatuteMd5 { get; set; }
        public virtual string AdditionalDocumentMd5 { get; set; }
        [Display(Name = "Погодження")]
        public virtual string ConsentText { get; set; }
        [Display(Name = "Кількість осіб, що обслуговувалась закладом")]
        public virtual int ContractorRmspAmount { get; set; }
        [Display(Name = "Версія запиту №")]
        public virtual Guid? PreviousRequestId { get; set; }
        public virtual Guid? EhealthId { get; set; }
        public virtual bool ExternalContractorFlag { get; set; }
    }
}
