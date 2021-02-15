using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Common.Models
{
    [Display(Name = "Документ, що засвідчує особу")]
    public abstract class BaseIdentityDocument: BaseDocument
    {
        [MaxLength(200)]
        [Display(Name = "Ким виданий")]
        public virtual string IssuedAt { get; set; }

        [Display(Name = "Діє до")]
        public virtual DateTime? ExpirationDate { get; set; }
    }
}
