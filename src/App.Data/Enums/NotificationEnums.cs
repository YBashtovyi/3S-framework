using System.ComponentModel.DataAnnotations;

namespace App.Data.Enums
{
    public enum NotificationState
    {
        [Display(Name = "Не передавалось")]
        NotTransfered = 0,

        [Display(Name = "Успішно")]
        Successful = 1,

        [Display(Name = "Не вдалося")]
        Failed = 2,

        [Display(Name = "Є помилки")]
        Errored = 3
    }

    public enum NotificationType
    {
        [Display(Name = "Системні")]
        System = 0,

        [Display(Name = "Звичайні")]
        Usual = 1
    }
}
