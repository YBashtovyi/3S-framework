using System;
using System.Threading.Tasks;
using App.Data.Dto.Atu;
using Core.Services.Data;

namespace App.Business.Services.CityServices
{
    public class CityService
    {
        public CityService(ICommonDataService dataService)
        {
            DataBase = dataService;
        }

        public ICommonDataService DataBase { get; }

        #region MethodsPublic

        public async Task<CityEditDto> GetEditPageCity(Guid id)
        {
            return new CityEditDto();
        }

        public async Task<CityEditDto> UpdateCity(Guid id, CityEditDto dto)
        {
            return new CityEditDto();
        }

        public async Task<CityEditDto> CreateCity(CityEditDto dto)
        {
            return new CityEditDto();
        }

        #endregion
    }
}
