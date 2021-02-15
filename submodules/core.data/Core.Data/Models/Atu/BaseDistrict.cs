using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Table("AtuDistrict")]
    public class BaseDistrict: CoreEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public Type BaseType => typeof(BaseSubject);
        #endregion

        [Required, Display(Name = "Назва району"), MinLength(200)]
        public virtual string Name { get; set; }

        [Required, Display(Name = "Внутрішній код одиниці АТУ"), MaxLength(100)]
        public virtual string Code { get; set; }

        public virtual Guid ParentId { get; set; }

        [Required, Display(Name = " Тип району, перелік (міський район, район області)"), MaxLength(50)]
        public virtual string AtuDistrictType { get; set; }
    }
}
