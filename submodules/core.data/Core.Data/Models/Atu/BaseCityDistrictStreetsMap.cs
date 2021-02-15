using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Table("AtuCityDistrictStreetsMap")]
    public class BaseCityDistrictStreetsMap
    {
        public virtual Guid CityId { get; set; }

        public virtual Guid DistrictId { get; set; }

        public virtual Guid StreetId { get; set; }
        // TODO : make table historical
    }
}
