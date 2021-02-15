using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Security;

namespace App.Data.Models
{
    [Display(Name = "Ключова сутність системи, містить в собі дані які характеризують проект по об'єкту будівництва, строки виконання робіт по проекту, цінові характеристики та ін.")]
    [MainEntity(nameof(Project))]
    public class Project: CoreEntity
    {
        [Required, MaxLength(100)]
        public virtual string Name { get; set; }

        [MaxLength(200)]
        public virtual string FullName { get; set; }

        [Required, Display(Name = "Власник")]
        public virtual Guid OwnerId { get; set; }

        [Display(Name = "Область")]
        public virtual Guid RegionId { get; set; }

        [Display(Name = "Район")]
        public virtual Guid? DistrictId { get; set; }

        [Required, MaxLength(20), Display(Name = "Код об'єкту")]
        public virtual string Code { get; set; }

        [RequiredNonDefault, Display(Name = "Тип робіт")]
        public virtual Guid TypeOfProjectWorkId { get; set; }

        [Required, MaxLength(50), Display(Name = "Статус проекту")]
        public virtual string ProjectStatus { get; set; }

        [Required, MaxLength(50), Display(Name = "Тип фінансування")]
        public virtual string TypeOfFinancing { get; set; }

        public virtual decimal? Cost { get; set; }

        [Required, MaxLength(50), Display(Name = "Статус виконання проекту")]
        public virtual string ProjectImplementationState { get; set; }

        public virtual DateTime DateBegin { get; set; }

        public virtual DateTime DateEnd { get; set; }

        [MaxLength(20)]
        public virtual string RepairLength { get; set; }

        [MaxLength(20)]
        public virtual string RepairSquare { get; set; }

        [MaxLength(300)]
        public virtual string Description { get; set; }

        [Column(TypeName = "json")]
        public string AtuCoordinates { get; set; }

        public OrgUnit Owner { get; set; }

        public Region Region { get; set; }

        public District District { get; set; }

        public TypeOfProjectWork TypeOfProjectWork { get; set; }
    }
}
