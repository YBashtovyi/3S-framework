using System;
using System.Threading.Tasks;
using App.Data.Dto.Atu;
using Core.Services.Data;

namespace App.Business.Services.CountryServices
{
    public class CountryService
    {
        public CountryService(ICommonDataService dataService)
        {
            DataBase = dataService;
        }

        public ICommonDataService DataBase { get; }

        #region MethodsPublic

        public async Task<CountryEditDto> GetEditPageCountry(Guid id)
        {
            return new CountryEditDto();
        }

        public async Task<CountryEditDto> UpdateCountry(Guid id, CountryEditDto dto)
        {
            return new CountryEditDto();
        }

        public async Task<CountryEditDto> CreateCountry(CountryEditDto dto)
        {
            return new CountryEditDto();
        }

        #endregion
    }
}
