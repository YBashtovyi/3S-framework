using System.ComponentModel.DataAnnotations;

namespace Core.Common.Enums
{
	public enum Gender
	{
        [Display(Name = "Не вибрано")]
        NotSpecified,
        [Display(Name = "Чоловік")]
        Male,
        [Display(Name = "Жінка")]
        Female
	}
}
