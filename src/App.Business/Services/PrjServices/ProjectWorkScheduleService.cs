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
    public class ProjectWorkScheduleService
    {
        #region Constructor
        
        public ProjectWorkScheduleService(ICommonDataService dataService)
        {
            _dataService = dataService;
        }

        #endregion

        #region Properties

        private readonly ICommonDataService _dataService;

        #endregion

        #region PublicMethods

        public async Task<Guid> Create(ProjectWorkScheduleEditDto dto)
        {
            /*
             * Checking for the existence of a document with a type WorkSchedule
             * - if it exists and the same type is specified, then throw an error
             * - if it exists and the ChangesToWS type is specified, then we will write this document in parentId
             */
            var existsWSToPrj = await _dataService.SingleOrDefaultAsync<ProjectWorkScheduleDetailsDto>(p => p.ProjectId == dto.ProjectId && p.DocType == "WorkSchedule");
            if (existsWSToPrj != null && dto.DocType == "WorkSchedule")
            {
                throw new AppException("Календарний план вже створений. Створення нового заборонено!");
            }
            else if (existsWSToPrj != null && dto.DocType == "ChangesToWS")
            {
                dto.ParentId = existsWSToPrj.Id;
                // TODO: на потом доделать копировани этапов и видов работ. Так произвести перепривязку этапов в видах работ
                //var stages = await CopyProjectWorkScheduleStageList(existsWSToPrj.Id, dto.Id);
                //var subTypes = await CopyProjectWorkScheduleSubTypeList(existsWSToPrj.Id, dto.Id);

                //_dataService.AddDtoRange<ProjectWorkScheduleStage>(stages, false);
                //_dataService.AddDtoRange<ProjectWorkScheduleSubType>(subTypes, false);
            }
            else if (existsWSToPrj == null && dto.DocType == "ChangesToWS")
            {
                throw new AppException("Зміни до календарного плану не можуть бути створені без календарного плану!");
            }

            dto.Id = Guid.NewGuid();
            _dataService.AddDto<ProjectWorkSchedule>(dto, false);
            await _dataService.SaveChangesAsync();

            return dto.Id;
        }

        public async Task<ProjectWorkScheduleEditDto> Edit(Guid id, ProjectWorkScheduleEditDto dto)
        {
            var pws = await GetProjectWorkScheduleById(id);

            if (pws.DocType != dto.DocType)
            {
                throw new AppException("Тип документа змінений. Дія збереження неможлива");
            }

            _dataService.AddDto<ProjectWorkSchedule>(dto, true);
            await _dataService.SaveChangesAsync();

            return dto;
        }

        public async Task Delete(Guid id)
        {
            var pws = await GetProjectWorkScheduleById(id);

            if (pws.DocType == "WorkSchedule")
            {
                var pwsChangesToWS = await _dataService.GetDtoAsync<ProjectWorkScheduleDetailsDto>(p => p.ParentId == pws.Id && p.DocType == "ChangesToWS");
                if (pwsChangesToWS.Any())
                {
                    throw new AppException("Видалення не можливе. Існують посилання на календарний план");
                }
            }

            _dataService.Remove<ProjectWorkSchedule>(id);
            await _dataService.SaveChangesAsync();
        }

        #endregion

        #region PrivateMethods

        private async Task<ProjectWorkScheduleDetailsDto> GetProjectWorkScheduleById(Guid id)
        {
            var ws = await _dataService.SingleOrDefaultAsync<ProjectWorkScheduleDetailsDto>(p => p.Id == id);
            if (ws == null)
            {
                throw new AppException("Календарний план не знайдений");
            }

            return ws;
        }

        private async Task<(List<ProjectWorkScheduleStageEditDto>, Dictionary<Guid, Guid>)> CopyProjectWorkScheduleStageList(Guid idFromSchedule, Guid idToSchedule)
        {
            var stages = (await _dataService.GetDtoAsync<ProjectWorkScheduleStageEditDto>(p => p.PrjWorkScheduleId == idFromSchedule)).ToList();
            if (!stages.Any())
            {
                return (null, null);
            }

            stages.ForEach(p =>
            {
                p.Id = Guid.NewGuid();
                p.PrjWorkScheduleId = idToSchedule;
            });

            return (stages, new Dictionary<Guid, Guid>());
        }

        private async Task<List<ProjectWorkScheduleSubTypeEditDto>> CopyProjectWorkScheduleSubTypeList(Guid idFromSchedule, Guid idToSchedule, Dictionary<Guid, Guid> oldAndNewStageId)
        {
            var subTypes = (await _dataService.GetDtoAsync<ProjectWorkScheduleSubTypeEditDto>(p => p.PrjWorkScheduleId == idFromSchedule)).ToList();
            if (!subTypes.Any())
            {
                return null;
            }

            subTypes.ForEach(p =>
            {
                p.Id = Guid.NewGuid();
                p.PrjWorkScheduleId = idToSchedule;
                p.PrjWorkScheduleStageId = oldAndNewStageId[p.PrjWorkScheduleStageId];
            });

            return subTypes;
        }

        #endregion
    }
}
