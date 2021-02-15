using App.Data.Models;
using Core.Data.Dto.Atu;
using Core.Security;

namespace App.Data.Dto.Atu
{
    [MainEntity(nameof(Country))]
    public class CountryListDto : BaseCountryListDto
    {
    }

    [MainEntity(nameof(City))]
    public class CountryDetailsDto : BaseCountryDetailsDto
    {

    }

    [MainEntity(nameof(City))]
    public class CountryEditDto : BaseCountryEditDto
    {
    }
}
