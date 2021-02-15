using App.Data.Models;
using Core.Data.Dto.Atu;
using Core.Security;

namespace App.Data.Dto.Atu
{
    [MainEntity(nameof(Region))]
    public class RegionListDto: BaseRegionListDto
    {
    }
}
