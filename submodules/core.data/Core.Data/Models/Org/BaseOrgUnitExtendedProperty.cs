using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;

namespace Core.Data.Models.Org
{
    [Display(Name = "Додаткові властивості організаційної одиниці")]
    [Table("OrgUnitExtendedProperty")]
    public class BaseOrgUnitExtendedProperty: CoreEntity
    {
        [RequiredNonDefault]
        public virtual Guid OrgUnitId { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public virtual string OrgExtendedProperty { get; set; }

        [MaxLength(200), Display(Name = "Тестове поле, використоється для зберігання простих значень")]
        public virtual string Value { get; set; }

        [Column(TypeName = "json"), Display(Name = "Дані у форматі json, використовується для зберігання даних у більш складних форматах")]
        public virtual string ValueJson { get; set; }
    }
}
