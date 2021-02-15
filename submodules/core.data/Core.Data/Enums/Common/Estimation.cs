﻿using System.ComponentModel.DataAnnotations;

namespace Core.Data.Common
{
    public enum Estimation
    {
        [Display(Name = "Дуже погано")]
        VeryBad = 1,
        [Display(Name = "Погано")]
        Bad = 2,
        [Display(Name = "Посередньо")]
        Average = 3,
        [Display(Name = "Добре")]
        Good = 4,
        [Display(Name = "Дуже добре")]
        Excellent = 5
    }
}
