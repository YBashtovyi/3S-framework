using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Table("AtuRegion")]
    public class BaseRegion: CoreEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public Type BaseType => typeof(BaseSubject);
        #endregion

        [Required, Display(Name = "Назва області"), MaxLength(200)]
        public virtual string Name { get; set; }

        [Required, Display(Name = "Внутрішній код одиниці АТУ"), MaxLength(100)]
        public virtual string Code { get; set; }

        public virtual Guid ParentId { get; set; }

        [Required, Display(Name = "Класифікатор об'єктів адміністративно-територіального устрою України"), MaxLength(50)]
        public virtual string KOATU { get; set; }

        [Required, Display(Name = "Тип територіального утворення, перелік (область, регіон, край, штат)"), MaxLength(50)]
        public virtual string AtuRegionType { get; set; }
    }
}
