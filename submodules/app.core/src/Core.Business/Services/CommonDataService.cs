using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Administration;
using Core.Base.Data;
using Core.Business.Services;
using Core.Common.Extensions;
using Core.Common.Helpers;
using Core.Data;
using Core.Data.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Data
{
    public class CommonDataService: ICommonDataService
    {
        #region Fields_And_Properties

        private readonly CoreDbContext _context;
        private readonly IQueryTextService _queryTextService;
        private readonly IQueryableCacheService _queryableCache;
        private readonly IUserInfoService _userService;
        private readonly IPendingChangeService _pendingChangeService;
        private readonly IObjectMapper _mapper;
        private readonly MetricReporter _metricReporter;

        #endregion Fields_And_Properties

        #region Constructors

        // Full parameters constructor
        public CommonDataService(CoreDbContext context,
                IQueryTextService queryTextService,
                IQueryableCacheService qcache,
                IUserInfoService userService,
                IPendingChangeService pendingChangeService,
                IObjectMapper objectMapper,
                MetricReporter MetricReporter)
        {
            _context = context;
            _queryTextService = queryTextService;
            _queryableCache = qcache;
            _userService = userService;
            _pendingChangeService = pendingChangeService;
            _mapper = objectMapper;
            _metricReporter = MetricReporter;
        }

        // constructor without pending change service
        public CommonDataService(CoreDbContext context,
               IQueryTextService queryTextService,
               IQueryableCacheService qcache,
               IUserInfoService userService,
               IObjectMapper objectMapper, MetricReporter MetricReporter)
        {
            _context = context;
            _queryTextService = queryTextService;
            _queryableCache = qcache;
            _userService = userService;
            _pendingChangeService = null;
            _mapper = objectMapper;
            _metricReporter = MetricReporter;
        }

        #endregion

        #region Methods_Public

        #region GetDto
        /// <inheritdoc/>
        public virtual IEnumerable<TDto> GetDto<TDto>(Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy, IDictionary<string, string> parameters,
            int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var currentType = typeof(TDto);

            AssertCanReadTypeData(currentType, fromUser);

            var addCount = typeof(IPagingCounted).IsAssignableFrom(currentType);
            var sqlText = GetSqlText(currentType, parameters, addCount, fromUser, extraParameters);

            var query = GetFilteredDto(sqlText, predicate).AddOrderBy(orderBy);

            var sw = Stopwatch.StartNew();
            // under come conditions there is no way calculate records count otherwise than executing an extra query
            var count = 0;
            if (addCount && predicate != null)
            {
                count = query.Count();
            }

            query = GetSkipTakeDto(query, skip, take);
            var result = ExecuteQueryOrUseCache(query, sqlText.ToString(), cacheResultDuration);

            SetRecordsCount(result, addCount, count);

            sw.Stop();
            _metricReporter.RegisterSqlTime(typeof(TDto).FullName, sw.Elapsed);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy, IDictionary<string, string> parameters,
            int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var currentType = typeof(TDto);

            AssertCanReadTypeData(currentType, fromUser);

            var addCount = typeof(IPagingCounted).IsAssignableFrom(currentType);
            var sqlText = GetSqlText(currentType, parameters, addCount, fromUser, extraParameters);

            var query = GetFilteredDto(sqlText, predicate).AddOrderBy(orderBy);

            var sw = Stopwatch.StartNew();
            // under come conditions there is no way calculate records count otherwise than executing an extra query
            var count = 0;
            if (addCount && predicate != null)
            {
                count = await query.CountAsync();
            }

            query = GetSkipTakeDto(query, skip, take);
            var result = await ExecuteQueryOrUseCacheAsync(query, sqlText.ToString(), cacheResultDuration);
            SetRecordsCount(result, addCount, count);
            sw.Stop();
            _metricReporter.RegisterSqlTime(typeof(TDto).FullName, sw.Elapsed);

            return result;
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TDto> GetDto<TDto>(string orderBy,
            Expression<Func<TDto, bool>> predicate, IDictionary<string, string> parameters,
            int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var currentType = typeof(TDto);

            AssertCanReadTypeData(currentType, fromUser);

            var addCount = typeof(IPagingCounted).IsAssignableFrom(currentType);
            var sqlText = GetSqlText(currentType, parameters, addCount, fromUser, extraParameters);

            var query = GetFilteredDto(sqlText, predicate).AddOrderBy(orderBy);

            // under come conditions there is no way calculate records count otherwise than executing an extra query
            var count = 0;
            if (addCount && predicate != null)
            {
                count = query.Count();
            }

            query = GetSkipTakeDto(query, skip, take);
            var result = ExecuteQueryOrUseCache(query, sqlText.ToString(), cacheResultDuration);

            SetRecordsCount(result, addCount, count);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(string orderBy,
            Expression<Func<TDto, bool>> predicate, IDictionary<string, string> parameters,
            int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var currentType = typeof(TDto);

            AssertCanReadTypeData(currentType, fromUser);

            var addCount = typeof(IPagingCounted).IsAssignableFrom(currentType);
            var sqlText = GetSqlText(currentType, parameters, addCount, fromUser, extraParameters);

            var query = GetFilteredDto(sqlText, predicate).AddOrderBy(orderBy);

            var sw = Stopwatch.StartNew();

            // under come conditions there is no way calculate records count otherwise than executing an extra query
            var count = 0;
            if (addCount && predicate != null)
            {
                count = await query.CountAsync();
            }

            query = GetSkipTakeDto(query, skip, take);
            var result = await ExecuteQueryOrUseCacheAsync(query, sqlText.ToString(), cacheResultDuration);
            SetRecordsCount(result, addCount, count);

            sw.Stop();
            _metricReporter.RegisterSqlTime(typeof(TDto).FullName, sw.Elapsed);

            return result;
        }

        #endregion GetDto

        #region GetEntity
        public IEnumerable<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int skip, int take,
            bool doNotTrackChanges, int cacheResultDuration, BaseUserInfo fromUser, string[] includeProperties)
        where TEntity : class, IGenericEntity<Guid>
        {
            // TODO: implement caching
            AssertCanReadTypeData(typeof(TEntity), fromUser);
            var currentRights = GetCurrentRights(fromUser);
            var query = GetEntityQuery<TEntity>(predicate, orderBy, skip, take, doNotTrackChanges, includeProperties);

            var sw = Stopwatch.StartNew();
            // rights now are checked after fetching from database, because LINQ cannot translate rights check to query
            // this is OK, because GetEntity only should be used for developers purposes (in usual cases GetDto is used)
            var result = query
                .ToList()
                .Where(x => currentRights.RlsAllowsAccessToObject(x))
                .ToArray();

            sw.Stop();
            _metricReporter.RegisterSqlTime(typeof(TEntity).FullName, sw.Elapsed);

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetEntityAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int skip, int take,
            bool doNotTrackChanges, int cacheResultDuration, BaseUserInfo fromUser, string[] includeProperties)
        where TEntity : class, IGenericEntity<Guid>
        {
            // TODO: implement caching
            AssertCanReadTypeData(typeof(TEntity), fromUser);
            var currentRights = GetCurrentRights(fromUser);
            var query = GetEntityQuery(predicate, orderBy, skip, take, doNotTrackChanges, includeProperties);

            var sw = Stopwatch.StartNew();
            // rights now are checked after fetching from database, because LINQ cannot translate rights check to query
            // this is OK, because GetEntity only should be used for developers purposes (in usual cases GetDtoAsync is used)
            var result = (await query
                        .ToListAsync())
                        .Where(x => currentRights.RlsAllowsAccessToObject(x))
                        .ToArray();

            sw.Stop();
            _metricReporter.RegisterSqlTime(typeof(TEntity).FullName, sw.Elapsed);

            return result;
        }

        #endregion GetEntity

        #region AddUpdateRemove

        /// <inheritdoc/>
        public virtual Guid AddDto<TEntity>(IGenericEntity<Guid> dto,
            bool? isUpdating,
            BaseUserInfo fromUser)
            where TEntity : class, IGenericEntity<Guid>
        {
            return AddDtoInternal<TEntity>(dto, isUpdating, fromUser);
        }

        /// <inheritdoc/>
        public Guid Add<TEntity>(TEntity entity,
            bool? isUpdating,
            BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            return AddInternal(entity, isUpdating, fromUser, true);
        }

        /// <inheritdoc/>
        public async Task<TEntity> RemoveAsync<TEntity>(Guid id,
            bool softDeleting,
            BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            return await RemoveInternalAsync<TEntity>(id, softDeleting, fromUser);
        }

        /// <inheritdoc/>
        public TEntity Remove<TEntity>(Guid id,
            bool softDeleting,
            BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            return RemoveInternalById<TEntity>(id, softDeleting, fromUser);
        }

        /// <inheritdoc/>
        public TEntity Remove<TEntity>(TEntity entity,
            bool softDeleting,
            BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            return RemoveInternal(entity, softDeleting, fromUser);
        }

        #endregion AddUpdateRemove

        /// <inheritdoc/>
        public void ClearContext()
        {
            var entries = _context.ChangeTracker.Entries()
                                       .Where(e => e.State != EntityState.Detached)
                                       .ToList();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }

        /// <inheritdoc/>
        public void SaveChanges()
        {
            var sw = Stopwatch.StartNew();
            if (_context is IAuditableContext auditableContext)
            {
                auditableContext.SaveAuditable(_userService.GetCurrentUserInfo());
            }
            else
            {
                _context.SaveChanges();
            }

            sw.Stop();
            _metricReporter.RegisterSqlTime(string.Join(",", _context.ChangeTracker.Entries().Select(p => p.Entity.ToString())), sw.Elapsed);
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync()
        {
            var sw = Stopwatch.StartNew();
            if (_context is IAuditableContext auditableContext)
            {
                await auditableContext.SaveAuditableAsync(await _userService.GetCurrentUserInfoAsync());
            }
            else
            {
                await _context.SaveChangesAsync();
            }
            sw.Stop();
            _metricReporter.RegisterSqlTime(string.Join(",", _context.ChangeTracker.Entries().Select(p => p.Entity.ToString())), sw.Elapsed);
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetApplicationModels(bool withPrimaryKeysOnly)
        {
            return _context.GetApplicationModels(withPrimaryKeysOnly);
        }


        #endregion Methods_Public

        #region Methods_Private

        /// <summary>
        /// Adds entity to context without saving
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isUpdating">If false - this is create operation, if true - update
        /// If null - database query should be done to find entity by id.
        /// Then the operation create or update operation will be done depending on the query result 
        /// </param>
        /// <param name="fromUser">User which rights should be used or null if use current user</param>
        /// <param name="checkRights">Is rights check needed? False for cases when check was done earlier</param>
        /// <returns>Guid of newly created or updated entity</returns>
        private Guid AddInternal<TEntity>(TEntity entity, bool? isUpdating, BaseUserInfo fromUser, bool checkRights) where TEntity : class, IGenericEntity<Guid>
        {
            if (isUpdating == false || entity.Id == Guid.Empty)
            {
                isUpdating = false;
                AddToContext(entity, fromUser, checkRights);
            }
            else if (isUpdating == true)
            {
                UpdateContext(entity, fromUser, checkRights);
                AddPendingChange(entity, DatabaseOperationType.Update);
            }
            else
            {
                var entityToUpdate = _context.Find<TEntity>(entity.Id);
                if (entityToUpdate == null)
                {
                    isUpdating = false;
                    AddToContext(entity, fromUser, checkRights);
                }
                else
                {
                    isUpdating = true;
                    // entity fetched from the database check anyway; for security reasons when someone updates entity
                    // to new allowed values but in database this entity contains restricted data
                    AssertCarWriteTypeData(entityToUpdate, fromUser);
                    var mappedEntity = _mapper.Map(entity, entityToUpdate);
                    UpdateContext(mappedEntity, fromUser, checkRights);
                    AddPendingChange(mappedEntity, DatabaseOperationType.Update);
                }
            }

            if (entity is IDerivedEntity derived && derived.BaseType != null)
            {
                var currentType = typeof(TEntity);
                object baseEntity;

                baseEntity = _mapper.Map(derived, currentType, derived.BaseType);
                
                if (baseEntity != null)
                {
                    if (baseEntity is IDerivableEntity derivable)
                    {
                        derivable.DerivedEntity = currentType.Name;
                    }
                    ReflectionHelper.InvokeGenericMethod(this, derived.BaseType, nameof(AddInternal), new object[] { baseEntity, isUpdating, fromUser, checkRights });
                }
            }

            return entity.Id;
        }

        private void AddToContext<TEntity>(TEntity entity, BaseUserInfo fromUser, bool checkRights) where TEntity : IGenericEntity<Guid>
        {
            if (checkRights)
            {
                AssertCarWriteTypeData(entity, fromUser);
            }

            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Added)
            {
                _context.Add(entity);
            }
            AddPendingChange(entity, DatabaseOperationType.Insert);
        }

        private void UpdateContext<TEntity>(TEntity entity, BaseUserInfo fromUser, bool checkRights) where TEntity : IGenericEntity<Guid>
        {
            if (checkRights)
            {
                AssertCarWriteTypeData(entity, fromUser);
            }

            _context.Update(entity);
        }

        private void RemoveFromContext<TEntity>(TEntity entity, BaseUserInfo fromUser, bool checkRights) where TEntity : IGenericEntity<Guid>
        {
            if (checkRights)
            {
                AssertCarWriteTypeData(entity, fromUser);
            }

            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                _context.Remove(entity);
            }
            AddPendingChange(entity, DatabaseOperationType.Delete);
        }

        private Guid AddDtoInternal<TEntity>(IGenericEntity<Guid> dto, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            AssertCarWriteTypeData(dto, fromUser);
            TEntity entity;
            if (isUpdating == false || dto.Id == Guid.Empty)
            {
                entity = _mapper.Map<TEntity>(dto);
                AddInternal(entity, isUpdating, fromUser, false);
            }
            else
            {
                var entityToUpdate = _context.Find<TEntity>(dto.Id);
                if (entityToUpdate == null)
                {
                    if (isUpdating == true)
                    {
                        return Guid.Empty;
                    }
                    else
                    {
                        entity = _mapper.Map<TEntity>(dto);
                        AddInternal(entity, false, fromUser, false);
                    }
                }
                else
                {
                    entity = _mapper.Map(dto, entityToUpdate);
                    AddInternal(entity, true, fromUser, false);
                }
            }

            return entity == null ? Guid.Empty : entity.Id;
        }

        private TEntity RemoveInternal<TEntity>(TEntity entity, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            if (entity == null)
            {
                return null;
            }

            if (softDeleting)
            {
                if (entity is IRecordState recordStateEntity)
                {
                    recordStateEntity.RecordState = RecordState.Deleted;
                    UpdateContext(entity, fromUser, true);
                    AddPendingChange(entity, DatabaseOperationType.Delete);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot softly delete an entity of type {entity.GetType().Name} " +
                        $"because this entity does not implement {nameof(IRecordState)} interface");
                }
            }
            else
            {
                RemoveFromContext(entity, fromUser, true);
            }

            if (entity is IDerivedEntity derived && derived?.BaseType != null)
            {
                ReflectionHelper.InvokeGenericMethod(this, derived.BaseType, nameof(RemoveInternalById), new object[] { entity.Id, softDeleting, fromUser });
            }

            return entity;
        }

        /// <summary>
        /// Marks entity is softly deleted or sets state to Detached
        /// </summary>
        /// <typeparam name="TEntity">Entiy type</typeparam>
        /// <param name="id">Entity id</param>
        /// <param name="softDeleting">If true, then entity onlyl marked as deleted and won't be deleted from db</param>
        /// <param name="fromUser">User for rights check (usually current user)</param>
        /// <returns>Deleted entity</returns>
        private TEntity RemoveInternalById<TEntity>(Guid id, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var entity = _context.Find<TEntity>(id);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity of type {typeof(TEntity)} not found by id {id}");
            }

            AssertCarWriteTypeData(entity, fromUser);

            return RemoveInternal(entity, softDeleting, fromUser);
        }

        /// <summary>
        /// Marks entity is softly deleted or sets state to Detached
        /// </summary>
        /// <typeparam name="TEntity">Entiy type</typeparam>
        /// <param name="id">Entity id</param>
        /// <param name="softDeleting">If true, then entity onlyl marked as deleted and won't be deleted from db</param>
        /// <param name="fromUser">User for rights check (usually current user)</param>
        /// <returns>Deleted entity</returns>
        private async Task<TEntity> RemoveInternalAsync<TEntity>(Guid id, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var entity = await _context.FindAsync<TEntity>(id);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity of type {typeof(TEntity)} not found by id {id}");
            }

            AssertCarWriteTypeData(entity, fromUser);

            return RemoveInternal(entity, softDeleting, fromUser);
        }

        /// <summary>
        /// Checks, that user can write type data (both entity and rls)
        /// </summary>
        /// <param name="data">data to check</param>
        /// <param name="fromUser"></param>
        /// <exception cref="NoRightsException">If user has no rights to write current type data</exception>
        private void AssertCarWriteTypeData(object data, BaseUserInfo fromUser)
        {
            var rights = GetCurrentRights(fromUser);
            rights.AssertCanWriteTypeData(data.GetType());
            rights.AssertRlsAllowsObject(data);
        }

        private void AssertCanReadTypeData(Type type, BaseUserInfo fromUser)
        {
            var rights = GetCurrentRights(fromUser);
            rights.AssertCanReadTypeData(type);
        }

        private IEnumerable<T> ExecuteQueryOrUseCache<T>(IQueryable<T> data, string sqlText, int cacheResultDuration, int count = 0) where T : class
        {
            IEnumerable<T> result;
            if (cacheResultDuration > 0)
            {
                var cachedResult = _queryableCache.GetData<T>(sqlText, data.Expression);
                if (cachedResult.Any())
                {
                    result = cachedResult;
                }
                else
                {
                    result = _queryableCache.SetData(data, cacheResultDuration, sqlText, count);
                }
            }
            else
            {
                result = data.ToArray();
            }

            return result;
        }

        private async Task<IEnumerable<T>> ExecuteQueryOrUseCacheAsync<T>(IQueryable<T> data, string sqlText, int cacheResultDuration, int count = 0) where T : class
        {
            IEnumerable<T> result;
            if (cacheResultDuration > 0)
            {
                var cachedResult = _queryableCache.GetData<T>(sqlText, data.Expression);
                if (cachedResult.Any())
                {
                    result = cachedResult;
                }
                else
                {
                    result = await _queryableCache.SetDataAsync(data, cacheResultDuration, sqlText, count);
                }
            }
            else
            {
                result = await data.ToArrayAsync();
            }

            return result;
        }

        private IUserApplicationRights GetCurrentRights(BaseUserInfo user)
        {
            return (user ?? _userService.GetCurrentUserInfo()) as IUserApplicationRights;
        }

        private void SetRecordsCount<TDto>(IEnumerable<TDto> collection, bool addCount, int count)
        {
            if (addCount && count != 0)
            {
                foreach (var item in (IEnumerable<IPagingCounted>)collection)
                {
                    item.TotalRecordCount = count;
                }
            }
        }

        private IQueryable<TDto> GetFilteredDto<TDto>(FormattableString sql, Expression<Func<TDto, bool>> predicate) where TDto : class
        {
            var query = _context.Set<TDto>().FromSqlInterpolated(sql);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        private IQueryable<TDto> GetSkipTakeDto<TDto>(IQueryable<TDto> query, int skip, int take) where TDto : class
        {
            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take != 0)
            {
                query = query.Take(take);
            }

            return query;
        }

        private void AddPendingChange<TEntity>(TEntity entity, string operationType) where TEntity : IGenericEntity<Guid>
        {
            if (_pendingChangeService != null)
            {
                var entityType = entity.GetType();
                var pendingChange = _pendingChangeService.Create(entityType.Name, entity.Id, operationType);
                if (pendingChange != null)
                {
                    _context.Add(pendingChange);
                }
            }
        }

        private IQueryable<TEntity> GetEntityQuery<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int skip,
            int take,
            bool doNotTrackChanges,
            string[] includeProperties) where TEntity : class, IGenericEntity<Guid>
        {
            IQueryable<TEntity> query;
            if (predicate != null)
            {
                query = _context.Set<TEntity>().Where(predicate);
            }
            else
            {
                query = _context.Set<TEntity>();
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }

            if (doNotTrackChanges)
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }

            return query;
        }

        private FormattableString GetSqlText(Type dtoType, IDictionary<string, string> parameters, bool addCount,
            BaseUserInfo fromUser, object[] extraParameters)
        {
            var currentRights = GetCurrentRights(fromUser);
            var sqlText = _queryTextService.GetParameterizedQueryString(dtoType,
                                    parameters,
                                    addCount,
                                    currentRights,
                                    extraParameters);

            return sqlText;
        }

        #endregion Methods_Private

    }
}
