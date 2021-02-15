using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Payment details. In a contract, for instance
    /// </summary>
    [Display(Name = "Реквізити оплати")]
    [Table("EhdPaymentDetail")]
    public abstract class BaseEhealthPaymentDetail: BaseEntity
    {
        public virtual string BankName { get; set; }
        public virtual string Mfo { get; set; }
        public virtual string PayerAccount { get; set; }
        /// <summary>
        /// Entity id, where this payment detail is pointed out
        /// </summary>
        public virtual Guid? EntityId { get; set; }
    }
}
