using System;
using System.Linq;
using System.Threading.Tasks;
using App.Business.Services.OrganizationServices;
using App.Data.Dto.Org;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Services.Data;

namespace App.Business.Services.PrjServices
{
    public class ProjectParticipantService
    {
        #region Constructor

        public ProjectParticipantService(ICommonDataService dataService, OrgUnitPositionService orgUnitPositionService, OrgUnitStaffService orgUnitStaffService)
        {
            _orgUnitPositionService = orgUnitPositionService;
            _orgUnitStaffService = orgUnitStaffService;
            _dataService = dataService;
        }

        #endregion

        #region Propertiers

        private readonly ICommonDataService _dataService;

        private readonly OrgUnitPositionService _orgUnitPositionService;
        private readonly OrgUnitStaffService _orgUnitStaffService;

        #endregion

        #region MethodsPublic

        public async Task<ProjectParticipantEditDto> Create(ProjectParticipantEditDto dto)
        {
            await ValidationCreateOrUpdate(dto);

            // Save data
            dto.Id = Guid.NewGuid();
            _dataService.AddDto<ProjectParticipant>(dto, false);
            await _dataService.SaveChangesAsync();

            return dto;
        }

        public async Task<ProjectParticipantEditDto> Update(Guid id, ProjectParticipantEditDto dto)
        {
            var prjParticipant = await _dataService.SingleOrDefaultAsync<ProjectParticipantDetailsDto>(p => p.Id == id);
            if (prjParticipant == null)
            {
                throw new AppException("Учасника проєкту не існує");
            }

            await ValidationCreateOrUpdate(dto);

            // Save data
            dto.Id = Guid.NewGuid();
            _dataService.AddDto<ProjectParticipant>(dto, false);
            await _dataService.SaveChangesAsync();

            return dto;
        }

        public async Task Delete(Guid id)
        {
            var participant = await GetProjectContractById(id);

            var contracts = await _dataService.GetDtoAsync<ProjectContractListDto>(p => p.ProjectId == participant.ProjectId);
            if (contracts.Any())
            {
                throw new AppException("Видалення неможливе, оскільки в Системі існують посилання на даний запис!");
            }

            _dataService.Remove<ProjectParticipant>(id, true);
            await _dataService.SaveChangesAsync();
        }

        #endregion

        #region MethodsPrivate

        private async Task ValidationCreateOrUpdate(ProjectParticipantEditDto dto)
        {
            // Check if the position of inspector exists, if not, then create
            var orgUnitPosition = await _dataService.SingleOrDefaultAsync<OrgUnitPositionDetailsDto>(p => p.OrgUnitId == dto.ParticipantId && p.PositionCode == "Inspector");
            var orgUnitPositionId = orgUnitPosition?.Id ?? (await _orgUnitPositionService.CreateOrgUnitPosition(dto.ParticipantId, "Inspector", false)).Id;

            // Check if an employee with the position of inspector exists, if not, then create
            var orgUnitStaff = await _dataService.SingleOrDefaultAsync<OrgUnitStaffDetailsDto>(p => p.EmployeeId == dto.ResponsiblePersonId && p.OrgUnitPositionId == orgUnitPositionId);
            var orgUnitStaffId = orgUnitStaff?.Id ?? (await _orgUnitStaffService.CreateOrgUnitStaff(orgUnitPositionId, dto.ResponsiblePersonId, dto.ParticipantId, false)).Id;
        }

        private async Task<ProjectParticipantDetailsDto> GetProjectContractById(Guid id)
        {
            var contract = await _dataService.SingleOrDefaultAsync<ProjectParticipantDetailsDto>(p => p.Id == id);
            if (contract == null)
            {
                throw new AppException("Договір не знайдений");
            }

            return contract;
        }

        #endregion
    }
}
