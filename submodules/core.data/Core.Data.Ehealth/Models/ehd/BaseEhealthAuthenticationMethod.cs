using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Method, used to interact with a patient and validate theirs identity
    /// </summary>
    [Display(Name = "Спосіб взаємодії з персоною", Description = " Через телефон, через документи, що підтверджують особу тощо")]
    [Table("EhdAuthenticationMethod")]
    public abstract class BaseEhealthAuthenticationMethod: BaseEntity
    {
        public virtual string TypeCode { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual Guid? EntityId { get; set; }

        /// <summary>
        /// Required for type = THIRD_PERSON
        /// </summary>
        public virtual Guid? ThirdPersonEhealthId { get; set; }

        /// <summary>
        /// Required it type = THIRD_PERSON, and optional for type = OTP or OFFLINE
        /// </summary>
        public virtual string Alias { get; set; }
    }
}
