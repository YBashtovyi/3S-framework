using System.ComponentModel.DataAnnotations;

namespace Core.Common.Enums
{
	public enum Maturity
	{
        [Display(Name = "Не вибрано")]
        NotSpecified,
        [Display(Name = "Дитина (до 18 років)")]
        Child,
        [Display(Name = "Дорослий (від 18 років)")]
        Adult
	}
}
