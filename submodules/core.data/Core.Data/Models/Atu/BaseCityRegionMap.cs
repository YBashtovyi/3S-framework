using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Atu
{
    [Table("AtuCityRegionMap")] // TODO : make table historical
    public class BaseCityRegionMap: CoreEntity
    {
        public virtual Guid CityId { get; set; }
        public virtual Guid RegionId { get; set; }
    }
}
