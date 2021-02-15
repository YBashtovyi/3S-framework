using System.ComponentModel.DataAnnotations;

namespace Core.Data.Ehealth
{
    public enum PrescriptionRequestTransferState
    {
        [Display(Name = "Не передавалось")]
        NotTransferred,
        [Display(Name = "Очікує передачі")]
        WaitingForTransfer,
        [Display(Name = "Передано")]
        Transferred,
        [Display(Name = "Очікує скасування")]
        WaitingForCancel,
        [Display(Name = "Скасовано передачу")]
        TransferCanceled
    }
}
