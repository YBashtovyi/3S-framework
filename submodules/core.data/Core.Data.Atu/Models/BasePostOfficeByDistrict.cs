using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Atu.Models
{
    [Display(Name = "Довідник поштових відділень районів міста")]
    [Table("AtuPostOfficeByDistrict")]
    public abstract class BasePostOfficeByDistrict : BaseEntity
    {
        [Display(Name = "Поштове відділення")]
        public virtual Guid PostOfficeId { get; set; }

        [Display(Name = "Район міста")]
        public virtual Guid CityDistrictId { get; set; }
    }
}
