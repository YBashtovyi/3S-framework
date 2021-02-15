using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Common;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Services.Data;

namespace App.Business.Services.ApiControllerServices
{
    public class EnumRecordService
    {
        #region properties: public
        public ICommonDataService DataService { get; }
        #endregion

        #region constructor
        public EnumRecordService(ICommonDataService dataService)
        {
            DataService = dataService;
        }
        #endregion

        #region constructor

        public async Task<Dictionary<string, string>> Update(Guid Id, EnumRecordDto dto)
        {
            var enumRecord = (await DataService.GetDtoAsync<EnumRecordDto>(x => x.Id == Id)).FirstOrDefault();
            if (enumRecord == null)
            {
                return new Dictionary<string, string>() {
                    { "Id", "Запис не знайдено" }
                };
            }
            else
            {
                if (enumRecord.Group == dto.Group)
                {
                    DataService.AddDto<EnumRecord>(dto, true);
                    await DataService.SaveChangesAsync();
                }
                else
                {
                    return new Dictionary<string, string>() {
                        { "Group", "Група переліку не може бути змінено" }
                    };
                }
            }
            return new Dictionary<string, string>();
        }

        public async Task<EnumRecordDto> Create(EnumRecordDto dto)
        {
            var enumRecord = (await DataService.GetDtoAsync<EnumRecordDto>(p => p.Group.ToLower() == dto.Group.ToLower() && p.Code.ToLower() == dto.Code.ToLower())).FirstOrDefault();
            if (enumRecord != null)
            {
                // TODO : обработать нормально ошибки бека
                throw new AppException("Схожий запис вже існує");
            }

            dto.Id = Guid.NewGuid();
            DataService.AddDto<EnumRecord>(dto, false);
            await DataService.SaveChangesAsync();

            return dto;
        }
        #endregion
    }
}
