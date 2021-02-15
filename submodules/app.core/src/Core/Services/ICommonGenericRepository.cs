using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Common;
using Core.Data;

namespace Core.Services.Repositories
{
    public interface ICommonGenericRepository<TId> : IApplicationModels where TId:struct
    {
        void Add<TEntity>(TEntity entity,
            BaseUserInfo fromUser) where TEntity: IGenericEntity<TId>;
        IEnumerable<TDto> GetDto<TDto>(Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto: class;
        Task<IEnumerable<TDto>> GetDtoAsync<TDto>(Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto : class;
        IEnumerable<TDto> GetDto<TDto>(string orderBy,
            Expression<Func<TDto, bool>> predicate,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto : class;
        Task<IEnumerable<TDto>> GetDtoAsync<TDto>(string orderBy,
            Expression<Func<TDto, bool>> predicate,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto : class;
        IEnumerable<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int skip,
            int take,
            bool doNotTrackChanges,
            BaseUserInfo fromUser,
            string[] includeProperties) where TEntity : class, IGenericEntity<TId>;
        Task<TEntity> GetEntityAsync<TEntity>(Guid id, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>;
        Task<IEnumerable<TEntity>> GetEntityAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int skip,
            int take,
            bool doNotTrackChanges,
            BaseUserInfo fromUser,
            string[] includeProperties) where TEntity : class, IGenericEntity<TId>;
        void Remove<TEntity>(TEntity entity,
            BaseUserInfo fromUser) where TEntity : IGenericEntity<TId>;
        void Update<TEntity>(TEntity entity,
            BaseUserInfo fromUser) where TEntity : IGenericEntity<TId>;
        void Save();
        Task SaveAsync();
    }
}
