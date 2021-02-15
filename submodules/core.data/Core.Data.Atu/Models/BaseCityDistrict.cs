using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Atu.Models
{
    [Display(Name = "Довідник районів міст")]
    [Table("AtuCityDistrict")]
    public abstract class BaseCityDistrict : BaseEntity
    {
	    public virtual Guid CityId { get; set; }

        [Range(0,99,ErrorMessage="Код повинен бути не більше 2 цифр")]
        public virtual int Code { get; set; }
    }
}
