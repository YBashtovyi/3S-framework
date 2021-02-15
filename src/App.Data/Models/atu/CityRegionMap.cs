using Core.Data.Models.Atu;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(CityRegionMap))]
    public class CityRegionMap : BaseCityRegionMap // Why foreign key added in inherited classes ?
    {
        public City City { get; set; }
        public Region Region { get; set; }
    }
}
