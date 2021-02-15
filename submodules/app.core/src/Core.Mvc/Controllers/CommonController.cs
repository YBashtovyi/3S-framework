using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Mvc.Data;
using Core.Mvc.Extensions;
using Core.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ReflectionIT.Mvc.Paging;

namespace Core.Mvc.Controllers
{
    //public class CommonController<TDto, TEntity>: CommonController<TDto, TDto, TEntity>
    //    where TDto : CoreDto
    //    where TEntity : class, IEntity
    //{
    //    public CommonController(ICommonControllerService<TDto, TEntity> controllerService,
    //        ISearchFilterSettingsService searchFilterSettingsService, ILoggerFactory loggerFactory) : base(controllerService, searchFilterSettingsService, loggerFactory)
    //    {
    //    }
    //}

    public class CommonController<TListDto, TDetailDto, TEntity>: CommonController
        where TListDto : CoreDto, IPagingCounted
        where TDetailDto : CoreDto
        where TEntity : class, IEntity
    {
        private readonly ISearchFilterSettingsService _searchFilterSettingsService;
        protected ICommonControllerService<TListDto, TDetailDto, TEntity> GenericService { get; }

        #region Constructors
        public CommonController(ICommonControllerService<TListDto, TDetailDto, TEntity> controllerService,
            ISearchFilterSettingsService searchFilterSettingsService, ILoggerFactory loggerFactory) : base(controllerService, loggerFactory)
        {
            _searchFilterSettingsService = searchFilterSettingsService;
            GenericService = controllerService;
        }

        #endregion Constructors

        #region Methods
        #region Actions
        public virtual async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        public virtual async Task<IActionResult> List(Dictionary<string, string> paramList,
            ActionListOption<TListDto> options, bool? partial)
        {
            return await List(paramList, options, partial ?? true, GenericService.List);
        }

        public virtual async Task<IActionResult> Details(Guid id)
        {
            return await Details(id, GenericService.Details);
        }

        public virtual async Task<IActionResult> Edit(Guid? id, Dictionary<string, string> paramList)
        {
            return await Edit(id, paramList, GenericService.GetEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(Guid id, TDetailDto model)
        {
            return await Edit<TDetailDto, TEntity>(id, model, id != Guid.Empty, GenericService.PostEdit);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(Guid id, bool softDeleting = true)
        {
            return await Delete<TEntity>(id, softDeleting, GenericService.Delete);
        }
        #endregion Actions

        #region SearchForm
        public async Task<string> GetPresettingsAsync(string journalName)
        {
            return await _searchFilterSettingsService.GetUserPresettingsAsync(journalName);
        }

        public async Task<JsonResult> SetPresettings(string journalName, string presettingsJson)
        {
            try
            {
                await _searchFilterSettingsService.SetUserPresettings(journalName, presettingsJson);
                return await Task.FromResult(Json(new { setSuccess = true }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(Json(new { setSuccess = false, ErrorMessage = "Помилка. " + (e.InnerException ?? e).Message }));
            }
        }

        public virtual string GenerateInputConfig()
        {
            return _searchFilterSettingsService.GenerateInputConfig(typeof(TListDto));
        }
        #endregion SearchForm

        #endregion Methods
    }

    public class CommonController: Controller
    {
        #region FieldsAndProperties
        protected ICommonControllerService CommonService { get; }
        protected ILoggerFactory LoggerFactory { get; }
        private static ILogger<CommonController> Logger;

        protected int PageSize { get; set; } = 10;
        protected string ListSortExpressionDefault = "Caption";
        #endregion

        public CommonController(ICommonControllerService controllerService, ILoggerFactory loggerFactory)
        {
            CommonService = controllerService;
            LoggerFactory = loggerFactory;
            if (Logger == null)
            {
                Logger = LoggerFactory.CreateLogger<CommonController>();
            }
        }

        #region NonActions
        [NonAction]
        public async Task<IActionResult> List<T>(Dictionary<string, string> paramList,
            ActionListOption<T> options, bool partial,
            Func<Dictionary<string, string>, string, int, int, Task<GetListResult<T>>> listFunction) where T : CoreDto, IPagingCounted
        {
            if (options == null)
            {
                options = new ActionListOption<T>();
            }

            // this thing was needed for client-side pagination
            ViewBag.FormParamList = string.Join("&", paramList.Select(x => string.Format("{0}={1}", x.Key, x.Value)));

            paramList = paramList
                .Where(x => !string.IsNullOrEmpty(x.Value) &&
                            x.Key != "__RequestVerificationToken" &&
                            x.Key != "X-Requested-With" &&
                            !x.Key.StartsWith("pg_"))
                .ToDictionary(x => x.Key, x => x.Value);

            var orderBy = options.pg_SortExpression ?? ListSortExpressionDefault;
            var pageNumber = options.pg_Page;
            var pageSize = PageSize;

            GetListResult<T> result;
            try
            {
                result = (listFunction == null)
                ? await CommonService.List<T>(paramList, orderBy, pageNumber, pageSize)
                : await listFunction(paramList, orderBy, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, @"Error occured while executing controller action {0} for type {1}.
Parameters: Paramlist = {2}, orderBy = {3}, pageNumber = {4}, pageSize = {5}, partial = {6}",
                    nameof(List), typeof(T), paramList, orderBy, pageNumber, pageSize, partial);
                return BadRequest();
            }

            FillViewData(result.ViewData);

            var pagingList = PagingList.Create(result.Data,
                pageSize,
                pageNumber,
                orderBy,
                "Id",
                x => (x as IPagingCounted)?.TotalRecordCount,
                options.pg_PartialViewName ?? "List",
                true);

            if (partial)
            {
                return PartialView(options.pg_PartialViewName, pagingList);
            }

            return View(options.pg_PartialViewName, pagingList);
        }

        [NonAction]
        public async Task<IActionResult> Details<T>(Guid id, Func<Guid, Task<GetDetailsResult<T>>> detailsFunction) where T : CoreDto
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            GetDetailsResult<T> result;
            try
            {
                result = (detailsFunction == null)
               ? await CommonService.Details<T>(id)
               : await detailsFunction(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while executing controller action {0} for type {1} with id = {2}",
                    nameof(Details), typeof(T), id);
                return BadRequest();
            }

            FillViewData(result.ViewData);
            if (result.Model == null)
            {
                return NotFound();
            }

            return View(result.Model);
        }

        [NonAction]
        public async Task<IActionResult> Edit<T>(Guid? id, Dictionary<string, string> paramList,
            Func<Guid?, Dictionary<string, string>, Task<GetEditResult<T>>> editFunction) where T : CoreDto
        {
            GetEditResult<T> result;
            try
            {
                result = (editFunction == null)
                ? await CommonService.GetEdit<T>(id, paramList)
                : await editFunction(id, paramList);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, @"Error occured while executing controller action {0} for type {1} with id = {2}. ParamList = {3}",
                    nameof(Edit), typeof(T), id, paramList);
                return BadRequest();
            }

            FillViewData(result.ViewData);
            if (result.Model == null)
            {
                return NotFound();
            }

            return View(result.Model);
        }

        [NonAction]
        public async Task<IActionResult> Edit<TDto, TEntity>(Guid id, TDto model, bool? isUpdating,
            Func<TDto, ModelStateDictionary, bool?, bool, Task<PostEditResult>> editFunction) where TDto : CoreDto where TEntity : class, IEntity
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            PostEditResult result;
            try
            {
                result = (editFunction == null)
                ? await CommonService.PostEdit<TDto, TEntity>(model, ModelState, isUpdating, true)
                : await editFunction(model, ModelState, isUpdating, true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, @"Error occured while executing controller action {0} for type {1} with id = {2}. isUpdating = {3}",
                    nameof(Edit), typeof(TDto), id, isUpdating);
                return BadRequest();
            }

            FillViewData(result.ViewData);
            if (result.Success && TryValidateModel(model))
            {
                return this.RedirectBack(result.RedirectToOnSuccess, result.RedirectToValues);
            }

            return View(model);
        }

        [NonAction]
        public async Task<IActionResult> Delete<T>(Guid id, bool softDeleting,
            Func<Guid, bool, bool, Task<PostDeleteResult>> deleteFunction) where T : class, IEntity
        {
            PostDeleteResult result;
            try
            {
                result = (deleteFunction == null)
                ? await CommonService.Delete<T>(id, softDeleting, true)
                : await deleteFunction(id, softDeleting, true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, @"Error occured while executing controller action {0} for type {1} with id = {2}. softDeleting = {3}",
                    nameof(Delete), typeof(T), id, softDeleting);
                return BadRequest("Item hasn't been deleted because of unexpected error");
            }

            if (!result.Success)
            {
                return NotFound("Item does not exist. Probably it was already deleted");
            }

            FillViewData(result.ViewData);

            if (result.RedirectToOnSuccess == null)
            {
                return Ok("Success");
            }

            return this.RedirectBack(result.RedirectToOnSuccess, result.RedirectToValues);
        }

        [NonAction]
        private void FillViewData(Dictionary<string, object> viewData)
        {
            if (viewData == null)
            {
                return;
            }

            foreach (var kv in viewData)
            {
                ViewData[kv.Key] = kv.Value;
            }
        }

        #endregion NonActions

    }
}
