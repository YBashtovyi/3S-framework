using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Cdn;
using App.Data.Dto.System;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Mvc.Helpers;
using Core.Services.Data;

namespace App.Business.Services.CdnServices
{
    public class ConstructionObjectExPropertyDictionaryService
    {
        #region Constructor
        
        public ConstructionObjectExPropertyDictionaryService(ICommonDataService dataService, IOfficeDocumentService docService)
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

        public async Task<ConstructionObjectExPropertyDictionary> Delete(Guid id, bool softDelete)
        {
            var exPropDict = await GetExPropById(id);

            var exProp = await _dataService.GetDtoAsync<ConstructionObjectExtendedPropertyListDto>(p =>
                p.ConstructionObjectExPropertyId == id || p.ConstructionObjectSubExPropertyId == id);

            if (exProp.Any())
            {
                throw new AppException("Видалення неможливе, оскільки в Системі існують посилання на даний запис!");
            }

            var exPropDictParent = await _dataService.GetDtoAsync<ConstructionObjectExPropertyDictionaryListDto>(p => p.ParentId == exPropDict.Id);
            if (exPropDictParent.Any())
            {
                throw new AppException("Видалення неможливе, оскільки в Системі існують посилання на даний запис!");
            }

            _dataService.Remove<ConstructionObjectExPropertyDictionary>(exPropDict.Id, softDelete);
            await _dataService.SaveChangesAsync();

            return new ConstructionObjectExPropertyDictionary();
        }

        public async Task<(byte[] fileData, string contentType, string fileName)> DownloadListAsExcelAsync(DownloadListModel options)
        {
            var (pageSize, pageNumber, orderBy, otherParameters) = HttpQueryStringHelper.GetQueryParametersFromQueryParamList(options?.ParamList);
            var projects = await _dataService.GetDtoAsync<ConstructionObjectExPropertyDictionaryListDto>(options.OrderBy, null, otherParameters, (pageNumber - 1) * pageSize, pageSize, 0, null, null);

            var fileData = _docService.GenerateStream(projects, opt =>
            {
                opt.ConfigureFields(options.Columns);
                opt.DefaultDateFormatter = (currDate) => currDate.AddMinutes(options.TimeZoneOffsetMinutes).ToString("g");
            });
            return (fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List.xlsx");
        }

        #endregion

        #region MethodsPrivate

        private async Task<ConstructionObjectExPropertyDictionaryDetailsDto> GetExPropById(Guid id)
        {
            var exPropDict = await _dataService.SingleOrDefaultAsync<ConstructionObjectExPropertyDictionaryDetailsDto>(p => p.Id == id);
            if (exPropDict == null)
            {
                throw new AppException("Додаткова характеристика не знайдена");
            }

            return exPropDict;
        }

        #endregion
    }
}
