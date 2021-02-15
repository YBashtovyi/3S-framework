using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Mvc.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Mvc.Services
{
    public interface ICommonControllerService
    {
        Task<GetListResult<T>> List<T>(Dictionary<string, string> routeValues, string orderBy = "Caption", int pageNumber = 1, int pageSize = 0) where T : CoreDto;
        Task<GetDetailsResult<T>> Details<T>(Guid id) where T : CoreDto;
        Task<GetEditResult<T>> GetEdit<T>(Guid? id, Dictionary<string, string> routeValues) where T : CoreDto;
        Task<PostEditResult> PostEdit<TDto, TEntity>(TDto model, ModelStateDictionary modelState, bool? isUpdating, bool saveChanges)
            where TDto : CoreDto
            where TEntity : class, IEntity;
        Task<PostDeleteResult> Delete<T>(Guid id, bool softDeleting, bool saveChanges) where T : class, IEntity;
    }

    public interface ICommonControllerService<TListDto, TDetailDto, TEntity>: ICommonControllerService
        where TListDto : CoreDto
        where TDetailDto : CoreDto
        where TEntity : class, IEntity
    {
        Task<GetListResult<TListDto>> List(Dictionary<string, string> routeValues, string orderBy = "Caption", int pageNumber = 1, int pageSize = 0);
        Task<GetDetailsResult<TDetailDto>> Details(Guid id);
        Task<GetEditResult<TDetailDto>> GetEdit(Guid? id, Dictionary<string, string> routeValues);
        Task<PostEditResult> PostEdit(TDetailDto model, ModelStateDictionary modelState, bool? isUpdating, bool saveChanges);
        Task<PostDeleteResult> Delete(Guid id, bool softDeleting, bool saveChanges);
    }

    public interface ICommonControllerService<TDto, TEntity>: ICommonControllerService<TDto, TDto, TEntity>
        where TDto : CoreDto
        where TEntity : class, IEntity
    {
    }
}
