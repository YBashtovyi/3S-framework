using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public class BaseEhealthAuthenticationMethodDto: BaseDto
    {
        [Display(Name = "Тип автентифікації")]
        public virtual string TypeCode { get; set; }

        [Display(Name = "Номер телефону")]
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
