using System.ComponentModel.DataAnnotations;

namespace Core.Data.Ehealth
{
    public enum PrescriptionTransferState
    {
        [Display(Name = "Не потребує передачі")]
        NotTransferred,
        [Display(Name = "Передано")]
        Transferred,
        [Display(Name = "Очікує скасування")]
        WaitingForCancel,
        [Display(Name = "Скасовано передачу")]
        TransferCanceled
    }
}
