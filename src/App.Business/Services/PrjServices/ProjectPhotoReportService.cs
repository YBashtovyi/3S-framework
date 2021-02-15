using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Common.NotMapped;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Services.Data;
using Newtonsoft.Json;

namespace App.Business.Services.PrjServices
{
    public class ProjectPhotoReportService
    {
        public ProjectPhotoReportService(ICommonDataService dataService)
        {
            _dataService = dataService;
        }

        private readonly ICommonDataService _dataService;

        #region publicMethods

        public async Task<Guid> Create(ProjectPhotoReportEditDto dto)
        {
            var project = await _dataService.SingleOrDefaultAsync<ProjectDetailsDto>(p => p.Id == dto.ProjectId);
            if (project == null)
            {
                throw new AppException("Проєкт не знайдений");
            }

            // TODO: create constants for ProjectRole
            var participants = await _dataService.GetDtoAsync<ProjectParticipantListDto>(p =>
                p.ProjectId == project.Id &&
                (p.ProjectRole == "Customer" || p.ProjectRole == "GeneralContractor"));

            var participantCustomer = participants.SingleOrDefault(p => p.ProjectRole == "Customer");
            var participantGeneralContractor = participants.SingleOrDefault(p => p.ProjectRole == "GeneralContractor");

            if (participantCustomer == null)
            {
                throw new AppException("Проєкт не має замовника!");
            }

            if (participantGeneralContractor == null)
            {
                throw new AppException("Проєкт не має генпідрядника");
            }

            dto.CustomerId = participantCustomer.ParticipantId;
            dto.GeneralContractorId = participantGeneralContractor.ParticipantId;
            dto.Id = Guid.NewGuid();

            _dataService.AddDto<ProjectPhotoReport>(dto, false);
            await _dataService.SaveChangesAsync();

            return dto.Id;
        }

        #region AtuCoordinate

        public async Task<MapCoordinate> AddCoordinate(Guid id, MapCoordinate coordinate)
        {
            var photoReport = await GetProjectPhotoReportById(id);

            if (coordinate.Lines == null && coordinate.Polygons == null && coordinate.Markers == null)
            {
                photoReport.AtuCoordinates = string.Empty;
            }
            else
            {
                photoReport.AtuCoordinates = JsonConvert.SerializeObject(coordinate);
            }

            _dataService.AddDto<ProjectPhotoReport>(photoReport, true);
            await _dataService.SaveChangesAsync();

            return coordinate;
        }

        public async Task<MapCoordinate> GetCoordinates(Guid prjId)
        {
            var photoReport = await GetProjectPhotoReportById(prjId);

            if (string.IsNullOrEmpty(photoReport.AtuCoordinates))
            {
                return new MapCoordinate();
            }

            var coordinates = JsonConvert.DeserializeObject<MapCoordinate>(photoReport.AtuCoordinates);

            return coordinates;
        }

        #endregion

        #endregion

        #region privateMethods

        private async Task<ProjectPhotoReportDetailsDto> GetProjectPhotoReportById(Guid id)
        {
            var constObject = await _dataService.SingleOrDefaultAsync<ProjectPhotoReportDetailsDto>(p => p.Id == id);
            if (constObject == null)
            {
                throw new AppException("Фотозвіт не знайдений");
            }

            return constObject;
        }

        #endregion
    }
}
