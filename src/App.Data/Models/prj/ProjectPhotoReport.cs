using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("PrjPhotoReport")]
    public class ProjectPhotoReport: CoreEntity, IDerivedEntity
    {
        #region IDerivedEntity
        public Type BaseType => typeof(Document);
        #endregion

        [Required]
        public Guid ProjectId { get; set; }

        [Required, Display(Name = "Замовник")]
        public Guid CustomerId { get; set; }

        public Guid GeneralContractorId { get; set; }

        [Required, MaxLength(50)]
        public string DocType { get; set; }

        public DateTime RegDate { get; set; }

        [Required, MaxLength(100)]
        public string RegNumber { get; set; }

        [Required, MaxLength(50)]
        public string DocState { get; set; }

        [Required, MaxLength(50), Display(Name = "Тип фіксації даних")]
        public string FixationType { get; set; }

        [Required, MaxLength(50), Display(Name = "Стан фіксації")]
        public string FixationState { get; set; }

        [Display(Name = "Відповідальна особа")]
        public Guid? ResponsibleEmployeeId { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Column(TypeName = "json")]
        public string AtuCoordinates { get; set; }

        public Employee ResponsibleEmployee { get; set; }

        public Project Project { get; set; }

        public Organization Customer { get; set; }

        public Organization GeneralContractor { get; set; }
    }
}
