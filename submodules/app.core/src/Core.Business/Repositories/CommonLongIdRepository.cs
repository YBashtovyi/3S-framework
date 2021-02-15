using Core.Base.Data;
using Core.Common;
using Core.Common.Extensions;
using Core.Data;
using Core.Models;
using Core.Services.Data;
using Core.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Business.Repositories
{
    public class CommonLongIdRepository : ICommonGenericRepository<long>, IApplicationModels
    {
        protected CoreDbContext Context { get; }
        protected IQueryTextService QueryTextService { get; }

        public CommonLongIdRepository(CoreDbContext context,
                IQueryTextService queryTextService)
        {
            Context = context;
            QueryTextService = queryTextService;
        }


        public void Add<TEntity>(TEntity entity, BaseUserInfo fromUser) where TEntity : IGenericEntity<long>
        {
            Context.Add(entity);
        }

        public async Task AddAsync<TEntity>(TEntity entity, BaseUserInfo fromUser) where TEntity : IGenericEntity<long>
        {
            await Context.AddAsync(entity);
        }

        public IEnumerable<TDto> GetDto<TDto>(Expression<Func<TDto, bool>> predicate, Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var dtoType = typeof(TDto);
            var addCount = typeof(IPagingCounted).IsAssignableFrom(dtoType);
            var sqlText = QueryTextService.GetParameterizedQueryString(dtoType, parameters, addCount, null, extraParameters);

            return GetDtoInternal(sqlText, predicate, orderBy, cacheResultDuration, skip, take, addCount);
        }

        public IEnumerable<TDto> GetDto<TDto>(string orderBy, Expression<Func<TDto, bool>> predicate, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var dtoType = typeof(TDto);
            var addCount = typeof(IPagingCounted).IsAssignableFrom(dtoType);
            var sqlText = QueryTextService.GetParameterizedQueryString(dtoType, parameters, addCount, null, extraParameters);

            return GetDtoInternal(sqlText, predicate, orderBy, cacheResultDuration, skip, take, addCount);
        }

        public async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(Expression<Func<TDto, bool>> predicate, Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var dtoType = typeof(TDto);
            var addCount = typeof(IPagingCounted).IsAssignableFrom(dtoType);
            var sqlText = await QueryTextService.GetParameterizedQueryStringAsync(dtoType,
                                    parameters,
                                    addCount,
                                    null,
                                    extraParameters);

            return GetDtoInternal(sqlText, predicate, orderBy, cacheResultDuration, skip, take, addCount);
        }

        public async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(string orderBy, Expression<Func<TDto, bool>> predicate, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            var dtoType = typeof(TDto);
            var addCount = typeof(IPagingCounted).IsAssignableFrom(dtoType);
            var sqlText = await QueryTextService.GetParameterizedQueryStringAsync(dtoType,
                                    parameters,
                                    addCount,
                                    null,
                                    extraParameters);

            return GetDtoInternal(sqlText, predicate, orderBy, cacheResultDuration, skip, take, addCount);
        }

        public IEnumerable<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int skip, int take, bool doNotTrackChanges, BaseUserInfo fromUser, string[] includeProperties) where TEntity : class, IGenericEntity<long>
        {
            IQueryable<TEntity> result;
            if (predicate != null)
            {
                result = Context.Set<TEntity>().Where(predicate);
            }
            else
            {
                result = Context.Set<TEntity>();
            }

            foreach (var property in includeProperties)
            {
                result = result.Include(property);
            }

            if (doNotTrackChanges)
            {
                result = result.AsNoTracking();
            }

            if (orderBy != null)
            {
                result = orderBy(result);
            }

            if (skip > 0)
            {
                result = result.Skip(skip);
            }

            if (take > 0)
            {
                result = result.Take(take);
            }

            var data = result.ToArray();
            return data;
        }

        public async Task<TEntity> GetEntityAsync<TEntity>(Guid id, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            return await Context.FindAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> GetEntityAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int skip, int take, bool doNotTrackChanges, BaseUserInfo fromUser, string[] includeProperties) where TEntity : class, IGenericEntity<long>
        {
            IQueryable<TEntity> result;
            if (predicate != null)
            {
                result = Context.Set<TEntity>().Where(predicate);
            }
            else
            {
                result = Context.Set<TEntity>();
            }

            foreach (var property in includeProperties)
            {
                result = result.Include(property);
            }

            if (doNotTrackChanges)
            {
                result = result.AsNoTracking();
            }

            if (orderBy != null)
            {
                result = orderBy(result);
            }

            if (skip > 0)
            {
                result = result.Skip(skip);
            }

            if (take > 0)
            {
                result = result.Take(take);
            }

            var data = await result.ToArrayAsync();
            return data;
        }

        public void Remove<TEntity>(TEntity entity, BaseUserInfo fromUser) where TEntity : IGenericEntity<long>
        {
            Context.Remove(entity);
        }

        public void Update<TEntity>(TEntity entity, BaseUserInfo fromUser) where TEntity : IGenericEntity<long>
        {
            Context.Update(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public IEnumerable<Type> GetApplicationModels(bool withPrimaryKeysOnly)
        {
            return Context.GetApplicationModels(withPrimaryKeysOnly);
        }

        #region Methods_Private

        // caching is not implemented, see CommonRepository for example, how caching should work
        private IEnumerable<TDto> GetDtoInternal<TDto>(FormattableString sql,
            Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy,
            int cacheResultDuration,
            int skip = 0,
            int take = 0,
            bool addCount = false) where TDto : class
        {
            var query = GetFilteredDto(sql, predicate);
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // under these conditions there is no way calculate records count otherwise than executing an extra query
            var count = 0;
            if (addCount && predicate != null)
            {
                count = query.Count();
            }

            query = GetSkipTakeDto(query, skip, take);
            var result = query.ToArray();

            if (addCount && predicate != null)
            {
                foreach (var item in (IEnumerable<IPagingCounted>)result)
                {
                    item.TotalRecordCount = count;
                }
            }

            return result;
        }

        // caching is not implemented, see CommonRepository for example, how caching should work
        private IEnumerable<TDto> GetDtoInternal<TDto>(FormattableString sql,
            Expression<Func<TDto, bool>> predicate,
            string orderBy,
            int cacheResultDuration,
            int skip = 0,
            int take = 0,
            bool addCount = false) where TDto : class
        {
            var query = GetFilteredDto(sql, predicate);
            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy<TDto>(orderBy);
            }

            // under these conditions there is no way calculate records count otherwise than executing an extra query
            var count = 0;
            if (addCount && predicate != null)
            {
                count = query.Count();
            }

            query = GetSkipTakeDto(query, skip, take);
            var result = query.ToArray();

            if (addCount && predicate != null)
            {
                foreach (var item in (IEnumerable<IPagingCounted>)result)
                {
                    item.TotalRecordCount = count;
                }
            }

            return result;
        }

        private IQueryable<TDto> GetFilteredDto<TDto>(FormattableString sql, Expression<Func<TDto, bool>> predicate) where TDto : class
        {
            var query = Context.Set<TDto>().FromSqlInterpolated(sql);
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

        #endregion Methods_Private
    }
}
