using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;

namespace Core.Data.Models.Common
{
    [Table("PersonExtendedProperty")]
    public class BasePersonExtendedProperty: CoreEntity
    {
        [RequiredNonDefault]
        public virtual Guid PersonId { get; set; }

        [MaxLength(50), Display(Name = "Перелік властивостей, які характиризують додаткові дані персони (Passport, DrivingLicense, etc.)")]
        public virtual string PersonExtendedProperty { get; set; }

        [MaxLength(200)]
        public virtual string Value { get; set; }

        [Column(TypeName = "json")]
        public virtual string ValueJson { get; set; }
    }
}
