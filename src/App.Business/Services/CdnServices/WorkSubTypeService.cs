using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Cdn;
using App.Data.Dto.Prj;
using App.Data.Dto.System;
using App.Data.Models.cdn;
using Core.Base.Exceptions;
using Core.Mvc.Helpers;
using Core.Services.Data;

namespace App.Business.Services.CdnServices
{
    public class WorkSubTypeService
    {
        #region Constructor

        public WorkSubTypeService(ICommonDataService dataService, IOfficeDocumentService docService)
        {
            _dataService = dataService;
            _docService = docService;
        }

        #endregion

        #region Propertiers

        private readonly ICommonDataService _dataService;
        private readonly IOfficeDocumentService _docService;

        #endregion

        #region MethodsPublic

        public async Task<WorkSubType> Delete(Guid id, bool softDelete)
        {
            var workSubType = await GetWorkSubTypeById(id);

            var workScheduleSubTypes = await _dataService.GetDtoAsync<ProjectWorkScheduleSubTypeListDto>(p =>
                p.WorkSubTypeId == id);

            if (workScheduleSubTypes.Any())
            {
                throw new AppException("Видалення неможливе, оскільки в Системі існують посилання на даний запис!");
            }

            var exPropDictParent = await _dataService.GetDtoAsync<WorkSubTypeListDto>(p => p.ParentId == workSubType.Id);
            if (exPropDictParent.Any())
            {
                throw new AppException("Видалення неможливе, оскільки в Системі існують посилання на даний запис!");
            }

            _dataService.Remove<WorkSubType>(workSubType.Id, softDelete);
            await _dataService.SaveChangesAsync();

            return new WorkSubType();
        }

        public async Task<(byte[] fileData, string contentType, string fileName)> DownloadListAsExcelAsync(DownloadListModel options)
        {
            var (pageSize, pageNumber, orderBy, otherParameters) = HttpQueryStringHelper.GetQueryParametersFromQueryParamList(options?.ParamList);
            var projects = await _dataService.GetDtoAsync<WorkSubTypeListDto>(options.OrderBy, null, otherParameters, (pageNumber - 1) * pageSize, pageSize, 0, null, null);

            var fileData = _docService.GenerateStream(projects, opt =>
            {
                opt.ConfigureFields(options.Columns);
                opt.DefaultDateFormatter = (currDate) => currDate.AddMinutes(options.TimeZoneOffsetMinutes).ToString("g");
            });
            return (fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List.xlsx");
        }

        #endregion

        #region MethodsPrivate

        private async Task<WorkSubTypeDetailsDto> GetWorkSubTypeById(Guid id)
        {
            var workSubType = await _dataService.SingleOrDefaultAsync<WorkSubTypeDetailsDto>(p => p.Id == id);
            if (workSubType == null)
            {
                throw new AppException("Вид робіт не знайдений!");
            }

            return workSubType;
        }

        #endregion
    }
}
