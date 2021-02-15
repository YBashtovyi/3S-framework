using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Models
{
    [Display(Name = "Попередні налаштування користувача")]
    public class UserPresettings: CoreEntity
    {
        public string User { get; set; }

        public string JournalName { get; set; }

        public string PresettingsJson { get; set; }
    }
}
