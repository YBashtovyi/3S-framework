using Core.Data.Models.Atu;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(CityDistrictStreetsMap))]
    public class CityDistrictStreetsMap: BaseCityDistrictStreetsMap
    {
        public City City { get; set; }

        public District District { get; set; }

        public Street Street { get; set; }
    }
}
