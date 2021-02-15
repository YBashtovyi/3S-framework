using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Mvc.Data;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Mvc.Services
{
    public class CommonControllerService: ICommonControllerService
    {
        protected ICommonDataService DataService { get; }

        public CommonControllerService(ICommonDataService dataService)
        {
            DataService = dataService;
        }

        public virtual async Task<GetListResult<T>> List<T>(Dictionary<string, string> routeValues, string orderBy = "Caption", int pageNumber = 1, int pageSize = 0) where T : CoreDto
        {
            var viewData = new Dictionary<string, object>();
            var list = await DataService.GetDtoAsync<T>(orderBy, null, routeValues, (pageNumber - 1) * pageSize, pageSize, 0, null, null);
            return new GetListResult<T> { Data = list, ViewData = viewData };
        }

        public virtual async Task<GetDetailsResult<T>> Details<T>(Guid id) where T : CoreDto
        {
            var viewData = new Dictionary<string, object>();
            T model = null;
            if (id != Guid.Empty)
            {
                model = (await DataService.GetDtoAsync<T>(x => x.Id == id)).SingleOrDefault();
            }

            return new GetDetailsResult<T> { Model = model, ViewData = viewData };
        }

        public virtual async Task<GetEditResult<T>> GetEdit<T>(Guid? id, Dictionary<string, string> routeValues) where T : CoreDto
        {
            var viewData = new Dictionary<string, object>();
            T model;
            if (id == null)
            {
                model = Activator.CreateInstance<T>();
            }
            else
            {
                model = (await DataService.GetDtoAsync<T>(x => x.Id == id.Value)).SingleOrDefault();
            }

            return new GetEditResult<T> { Model = model, ViewData = viewData };
        }

        public virtual async Task<PostEditResult> PostEdit<TDto, TEntity>(TDto model,
                ModelStateDictionary modelState, bool? isUpdating, bool saveChanges) where TDto : CoreDto where TEntity : class, IEntity
        {
            var result = new PostEditResult
            {
                Success = false,
                ViewData = new Dictionary<string, object>(),
                RedirectToOnSuccess = null,
                RedirectToValues = null
            };

            if (modelState.IsValid)
            {
                model.Id = DataService.AddDto<TEntity>(model, isUpdating);
                if (saveChanges)
                {
                    await DataService.SaveChangesAsync();
                }
                result.Success = true;
                result.RedirectToOnSuccess = nameof(Details);
                result.RedirectToValues = new { model.Id };
            }

            return result;
        }

        public virtual async Task<PostDeleteResult> Delete<T>(Guid id, bool softDeleting, bool saveChanges) where T : class, IEntity
        {
            var entity = DataService.Remove<T>(id, softDeleting);
            if (saveChanges)
            {
                await DataService.SaveChangesAsync();
            }

            var success = (entity != null);
            var viewData = new Dictionary<string, object>();
            return new PostDeleteResult { Success = success, ViewData = viewData, RedirectToOnSuccess = null, RedirectToValues = null };
        }
    }

    public class CommonControllerService<TListDto, TDetailDto, TEntity>: CommonControllerService, ICommonControllerService<TListDto, TDetailDto, TEntity>
        where TListDto : CoreDto
        where TDetailDto : CoreDto
        where TEntity : class, IEntity
    {
        public CommonControllerService(ICommonDataService dataService) : base(dataService)
        {
        }

        public virtual async Task<GetListResult<TListDto>> List(Dictionary<string, string> routeValues, string orderBy = "Caption", int pageNumber = 1, int pageSize = 0)
        {
            return await base.List<TListDto>(routeValues, orderBy, pageNumber, pageSize);
        }

        public virtual async Task<GetDetailsResult<TDetailDto>> Details(Guid id)
        {
            return await base.Details<TDetailDto>(id);
        }

        public virtual async Task<GetEditResult<TDetailDto>> GetEdit(Guid? id, Dictionary<string, string> routeValues)
        {
            return await base.GetEdit<TDetailDto>(id, routeValues);
        }

        public virtual async Task<PostEditResult> PostEdit(TDetailDto model,
            ModelStateDictionary modelState, bool? isUpdating, bool saveChanges)
        {
            return await base.PostEdit<TDetailDto, TEntity>(model, modelState, isUpdating, saveChanges);
        }

        public virtual async Task<PostDeleteResult> Delete(Guid id, bool softDeleting, bool saveChanges)
        {
            return await base.Delete<TEntity>(id, softDeleting, saveChanges);
        }
    }

    public class CommonControllerService<TDto, TEntity>: CommonControllerService<TDto, TDto, TEntity>, ICommonControllerService<TDto, TEntity>
        where TDto : CoreDto
        where TEntity : class, IEntity
    {
        public CommonControllerService(ICommonDataService dataService) : base(dataService)
        {
        }
    }
}
