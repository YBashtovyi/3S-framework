using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Services.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.Business.Services.PrjServices
{
    public class ProjectWorkScheduleStageService
    {
        public ProjectWorkScheduleStageService(ICommonDataService dataService)
        {
            _dataService = dataService;
        }

        private readonly ICommonDataService _dataService;

        #region publicMethods

        public async Task Delete(Guid id, bool softDelete)
        {
            var prjWSStage = await GetProjectWorkScheduleStageById(id);

            var prjWSSubTypes = await _dataService.GetDtoAsync<ProjectWorkScheduleSubTypeListDto>(p =>
                p.PrjWorkScheduleId == prjWSStage.PrjWorkScheduleId && p.PrjWorkScheduleStageId == prjWSStage.Id);
            if (prjWSSubTypes.Any())
            {
                throw new AppException("Видалення неможливе, оскільки в Системі існують види робіт на даний етап!");
            }

            _dataService.Remove<ProjectWorkScheduleStage>(prjWSStage.Id, softDelete);
            await _dataService.SaveChangesAsync();

            
        }

        #endregion

        #region privateMethods

        private async Task<ProjectWorkScheduleStageDetailsDto> GetProjectWorkScheduleStageById(Guid id)
        {
            var prjWSStage = await _dataService.SingleOrDefaultAsync<ProjectWorkScheduleStageDetailsDto>(p => p.Id == id);
            if (prjWSStage == null)
            {
                throw new AppException("Етап не знайдений");
            }

            return prjWSStage;
        }

        #endregion
    }
}
