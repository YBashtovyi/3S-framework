using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;

namespace Core.Data.Models.Org
{
    /// <summary>
    /// Посада це визначена структурою і штатним розписом первинна структурна одиниця, на яку покладено коло службових повноважень
    /// </summary>
    [Display(Name = "Посади у базовій організації")]
    [Table("OrgUnitPosition")]
    public abstract class BaseOrgUnitPosition : CoreEntity
    {
        [RequiredNonDefault]
        public virtual Guid OrgUnitId { get; set; }

        [RequiredNonDefault]
		public virtual Guid PositionId { get; set; }

        public virtual decimal StaffUnitCount { get; set; }

        [MaxLength(250)]
        public virtual string Description { get; set; }
    }
}
