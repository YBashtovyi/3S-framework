using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    public abstract class BaseEhealthAddressDto: BaseDto
    {
        [Display(Name = "Тип адреси")]
        [Required(ErrorMessage = "Заповніть поле")]
        public virtual string TypeCode { get; set; }
        public virtual string CountryCode { get; set; } = "UA";
        [Display(Name = "Область")]
        public virtual string Area { get; set; }
        [Display(Name = "Район")]
        public virtual string Region { get; set; }
        [Display(Name = "Населений пункт")]
        public virtual string Settlement { get; set; }
        [Display(Name = "Тип населеного пункт")]
        public virtual string SettlementTypeCode { get; set; }
        public virtual string SettlementId { get; set; }
        [Display(Name = "Тип адреси")]
        public virtual string StreetTypeCode { get; set; }
        [Display(Name = "Вулиця")]
        public virtual string Street { get; set; }
        [Display(Name = "Будинок")]
        public virtual string Building { get; set; }
        [Display(Name = "Квартира")]
        public virtual string Apartment { get; set; }
        [Display(Name = "Поштовий індекс")]
        public virtual string Zip { get; set; }
        public virtual Guid? EntityId { get; set; }
        public virtual string RegionId { get; set; }
        public virtual string AreaId { get; set; }
        public virtual string StreetId { get; set; }
    }
}
