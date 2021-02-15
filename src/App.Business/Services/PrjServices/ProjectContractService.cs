using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Services.Data;

namespace App.Business.Services.PrjServices
{
    public class ProjectContractService
    {
        public ProjectContractService(ICommonDataService dataService)
        {
            _dataService = dataService;
        }

        private readonly ICommonDataService _dataService;

        #region publicMethods

        public async Task<Guid> Create(ProjectContractEditDto dto)
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

            _dataService.AddDto<ProjectContract>(dto, false);
            await _dataService.SaveChangesAsync();

            return dto.Id;
        }

        public async Task Delete(Guid id)
        {
            var contract = await GetProjectContractById(id);

            var addAgreements = await _dataService.GetDtoAsync<ProjectAdditionalAgreementListDto>(p => p.ParentId == id);
            if (addAgreements.Any())
            {
                throw new AppException("Існують додаткові угоди пов'язані з цим документом. Видалення неможливе!");
            }

            _dataService.Remove<ProjectContract>(id, true);
            await _dataService.SaveChangesAsync();
        }

        #endregion

        #region privateMethods

        private async Task<ProjectContractDetailsDto> GetProjectContractById(Guid id)
        {
            var contract = await _dataService.SingleOrDefaultAsync<ProjectContractDetailsDto>(p => p.Id == id);
            if (contract == null)
            {
                throw new AppException("Договір не знайдений");
            }

            return contract;
        }

        #endregion
    }
}
