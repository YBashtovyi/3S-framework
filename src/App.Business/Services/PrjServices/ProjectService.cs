using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Business.Services.ApplicationServices;
using App.Data.Dto.Common.NotMapped;
using App.Data.Dto.Prj;
using App.Data.Dto.System;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Mvc.Helpers;
using Core.Services.Data;
using Newtonsoft.Json;

namespace App.Business.Services.PrjServices
{
    public class ProjectService
    {
        #region Constructor

        public ProjectService(ICommonDataService dataService, DefaultValuesService DefaultValuesService, IOfficeDocumentService docService)
        {
            this._dataService = dataService;
            _defaultValuesService = DefaultValuesService;
            _docService = docService;
        }

        #endregion

        #region Propertiers

        private readonly ICommonDataService _dataService;

        private readonly DefaultValuesService _defaultValuesService;

        private readonly IOfficeDocumentService _docService;

        #endregion

        #region MethodsPublic

        public async Task<ProjectEditDto> Create(ProjectEditDto dto)
        {
            dto.Id = Guid.NewGuid();
            dto.OwnerId = await _defaultValuesService.GetCurrentEmployeeOrganizationAsync();

            // TODO: Добавить проверку что ContructionObject активный
            var prjObj = new ProjectConstructionObject
            {
                ConstructionObjectId = dto.ConstructionObjectId,
                ProjectId = dto.Id
            };

            _dataService.Add(prjObj, false);
            _dataService.AddDto<Project>(dto, false);
            await _dataService.SaveChangesAsync();

            return dto;
        }

        public async Task Edit(Guid id, ProjectEditDto dto)
        {
            if (id != dto.Id)
            {
                throw new AppException("Конфлікт даних. Id <> dto.Id");
            }

            /*
             * Проверка связи проекта и объекта. Если связь есть, но id объекта изменен, то мы удаляем старую связь и создаем новую.
             * Если связи не существует, то создаем новую
             */
            // TODO: связей может быть много, но пока что должна быть 1 связь
            var isCreatePrjConstObject = false;
            var prjObject = (await _dataService.GetEntityAsync<ProjectConstructionObject>(p => p.ProjectId == id, true)).SingleOrDefault();
            if (prjObject != null)
            {
                if (prjObject.ConstructionObjectId != dto.ConstructionObjectId)
                {
                    _dataService.Remove<ProjectConstructionObject>(prjObject.Id, false);
                    isCreatePrjConstObject = true;
                }
            }
            else
            {
                isCreatePrjConstObject = true;
            }

            if (isCreatePrjConstObject)
            {
                prjObject = new ProjectConstructionObject
                {
                    ConstructionObjectId = dto.ConstructionObjectId,
                    ProjectId = dto.Id
                };
                _dataService.Add(prjObject, false);
            }

            _dataService.AddDto<Project>(dto, true);

            await _dataService.SaveChangesAsync();
        }

        public async Task<Project> Delete(Guid id, bool softDelete)
        {
            var project = await GetProjectById(id);

            var participant = await _dataService.GetDtoAsync<ProjectParticipantListDto>(p => p.ProjectId == id);
            if (participant.Any())
            {
                throw new AppException("Проєкт не може бути видалений! У проєкта наявні учасники");
            }

            var prjObject = (await _dataService.GetEntityAsync<ProjectConstructionObject>(p => p.ProjectId == id, true)).SingleOrDefault();
            if (prjObject != null)
            {
                _dataService.Remove<ProjectConstructionObject>(prjObject.Id, true);
            }

            _dataService.Remove<Project>(project.Id, true);
            await _dataService.SaveChangesAsync();

            return new Project();
        }

        public async Task<(byte[] fileData, string contentType, string fileName)> DownloadListAsExcelAsync(DownloadListModel options)
        {
            var (pageSize, pageNumber, orderBy, otherParameters) = HttpQueryStringHelper.GetQueryParametersFromQueryParamList(options?.ParamList);
            var projects = await _dataService.GetDtoAsync<ProjectListExcelDto>(options.OrderBy, null, otherParameters, (pageNumber - 1) * pageSize, pageSize, 0, null, null);

            var fileData = _docService.GenerateStream(projects, opt =>
            {
                opt.ConfigureFields(options.Columns);
                opt.DefaultDateFormatter = (currDate) => currDate.AddMinutes(options.TimeZoneOffsetMinutes).ToString("g");
            });
            return (fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List.xlsx");
        }

        #region AtuCoordinate

        public async Task<MapCoordinate> AddCoordinate(Guid prjId, MapCoordinate coordinate)
        {
            var project = await GetProjectById(prjId);

            if (coordinate.Lines == null && coordinate.Polygons == null && coordinate.Markers == null)
            {
                project.AtuCoordinates = string.Empty;
            }
            else
            {
                project.AtuCoordinates = JsonConvert.SerializeObject(coordinate);
            }

            _dataService.AddDto<Project>(project, true);
            await _dataService.SaveChangesAsync();

            return coordinate;
        }

        public async Task<MapCoordinate> GetCoordinates(Guid prjId)
        {
            var project = await GetProjectById(prjId);

            if (string.IsNullOrEmpty(project.AtuCoordinates))
            {
                return new MapCoordinate();
            }

            var coordinates = JsonConvert.DeserializeObject<MapCoordinate>(project.AtuCoordinates);

            return coordinates;
        }

        #endregion

        #endregion

        #region MethodsPrivate

        private async Task<ProjectDetailsDto> GetProjectById(Guid id)
        {
            var constObject = await _dataService.SingleOrDefaultAsync<ProjectDetailsDto>(p => p.Id == id);
            if (constObject == null)
            {
                throw new AppException("Проєкт не знайдений");
            }

            return constObject;
        }

        #endregion
    }
}
