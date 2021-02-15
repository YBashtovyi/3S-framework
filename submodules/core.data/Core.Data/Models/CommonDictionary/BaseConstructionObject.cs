using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.CommonDictionary
{
    [Display(Name = "Базовий клас який повинні наслідувати всі класи об'єктів будівництва (дорога, міст, будівля)")]
    [Table("ConstructionObject")]
    public class BaseConstructionObject: CoreEntity, IDerivableEntity
    {
        public string DerivedEntity { get; set; }
        
        [MaxLength(100)]
        public virtual string Name { get; set; }
        
        [MaxLength(20)]
        public virtual string Code { get; set; }
    }
}
