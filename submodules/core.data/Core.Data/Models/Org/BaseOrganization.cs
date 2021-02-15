using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Org
{
    /// <summary>
    /// Основна модель журналу організацій, містить в собі детальний опис організації,
    /// перелік її департаментів та підрозділів, дані по формі власності, типу, контактну інформацію, тощо.
    /// </summary>
    [Display(Name = "Організація")]
    [Table("Organization")]
    public abstract class BaseOrganization : CoreEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public virtual Type BaseType => typeof(BaseOrgUnit);
        #endregion

        [StringLength(200, MinimumLength = 1), Display(Name = "Назва організації")]
        public virtual string Name { get; set; }

        [StringLength(200, MinimumLength = 1), Display(Name = "Повна назва організації")]
        public virtual string FullName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public virtual string Code { get; set; }

        public virtual Guid? ParentId { get; set; }

        [StringLength(500, MinimumLength = 1), Display(Name = "Будь яка додаткова інформація")]
        public virtual string Description { get; set; }

        [StringLength(50, MinimumLength = 1), Display(Name = "Перелік організаційно правових форм")]
        public virtual string OrgType { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public virtual string Edrpou { get; set; }
    }
}
