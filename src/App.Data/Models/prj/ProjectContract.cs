using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("PrjContract")]
    public class ProjectContract: CoreEntity, IDerivedEntity
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

        [Required]
        public decimal Cost { get; set; }

        [Required, MaxLength(50), Display(Name = "Тип закупівлі")]
        public string BiddingType { get; set; }

        [MaxLength(100), Display(Name = "Ідентифікатор тендеру Prozorro")]
        public string TenderCode { get; set; }

        [Display(Name = "Ідентифікатор закупівлі Prozorro")]
        public Guid? BiddingCode { get; set; }

        [Required, MaxLength(2000), Display(Name = "Предмет закупівлі")]
        public string BiddingSubject { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Project Project { get; set; }

        public Organization Customer { get; set; }

        public Organization GeneralContractor { get; set; }
    }
}
