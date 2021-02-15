using System;
using App.Data.Models;
using Core.Security;
using Core.Data.Dto.Atu;


namespace App.Data.Dto.Atu
{
    [MainEntity(nameof(City))]
    public class CityListDto : BaseCityListDto
    {
        public DateTime CreatedOn { get; set; }
    }

    [MainEntity(nameof(City))]
    public class CityDetailsDto: BaseCityDetailsDto
    {
    
    }

    [MainEntity(nameof(City))]
    public class CityEditDto: BaseCityEditDto
    {
    }
}
