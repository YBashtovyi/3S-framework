using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Common;
using Core.Data;

namespace Core.Services.Data
{
    /// <summary>
    /// Service for CRUD operations with database via DbContext
    /// </summary>
    /// <typeparam name="TId">Primary key column type. Usually Guid or long</typeparam>
    public interface ICommonGenericDataService<TId> : IApplicationModels where TId: struct
    {
        /// <summary>
        /// Gets the results from the database as Dto collection
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="predicate">Conditions applied to collection</param>
        /// <param name="orderBy">Sorting function that will be applied to collection</param>
        /// <param name="parameters">Specially formatted parametes that will be converted to 'where' conditions</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="cacheResultDuration">How long results should be stored in the cache.
        /// Pay attention that result can be not cached at all even if duration is specified.</param>
        /// <param name="extraParameters">These parameters will be placed to {} placeholders in a text query, if any</param>
        /// <returns></returns>
        IEnumerable<TDto> GetDto<TDto>(Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto : class;

        /// <summary>
        /// Gets the results from the database as Dto collection
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="predicate">Conditions applied to collection</param>
        /// <param name="orderBy">Sorting function that will be applied to collection</param>
        /// <param name="parameters">Specially formatted parameters that will be converted to 'where' conditions</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="cacheResultDuration">How long results should be stored in the cache.
        /// Pay attention that result can be not cached at all even if duration is specified.</param>
        /// <param name="extraParameters">These parameters will be placed to {} placeholders in a text query, if any</param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetDtoAsync<TDto>(Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto : class;

        /// <summary>
        /// Gets the results from the database as Dto collection
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="orderBy">Sorting columns, separated by comma</param>
        /// <param name="predicate">Conditions applied to collection</param>
        /// <param name="parameters">Specially formatted parameters that will be converted to 'where' conditions</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="cacheResultDuration">How long results should be stored in the cache.
        /// Pay attention that result can be not cached at all even if duration is specified.</param>
        /// <param name="extraParameters">These parameters will be placed to {} placeholders in a text query, if any</param>
        /// <returns></returns>
        IEnumerable<TDto> GetDto<TDto>(string orderBy,
            Expression<Func<TDto, bool>> predicate,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto : class;

        /// <summary>
        /// Gets the results from the database as Dto collection
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="orderBy">Sorting columns, separated by comma</param>
        /// <param name="predicate">Conditions applied to collection</param>
        /// <param name="parameters">Specially formatted parameters that will be converted to 'where' conditions</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="cacheResultDuration">How long results should be stored in the cache.
        /// Pay attention that result can be not cached at all even if duration is specified.</param>
        /// <param name="extraParameters">These parameters will be placed to {} placeholders in a text query, if any</param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetDtoAsync<TDto>(string orderBy,
            Expression<Func<TDto, bool>> predicate,
            IDictionary<string, string> parameters,
            int skip,
            int take,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            object[] extraParameters) where TDto : class;

        /// <summary>
        /// Creates an IQueryable<TEntity> and executes the query, returning collection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate">Filter data conditions. Using: x => x.Id == Guid.Empty</param>
        /// <param name="orderBy">Sorting expression. Using: x => x.OrderBy(el => el.Caption)</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="doNotTrackChanges">Tells change tracker do not track any changes for these entities.
        /// In such case Save() and SaveAsync() methods do not affect fetched entities</param>
        /// <param name="includeProperties">Navigation properties to include</param>
        /// <returns>A collection fetched from the database</returns>
        IEnumerable<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int skip,
            int take,
            bool doNotTrackChanges,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            string[] includeProperties) where TEntity : class, IGenericEntity<TId>;

        /// <summary>
        /// Creates an IQueryable<TEntity> and executes the query, returning collection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate">Filter data conditions. Using: x => x.Id == Guid.Empty</param>
        /// <param name="orderBy">Sorting expression. Using: x => x.OrderBy(el => el.Caption)</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="doNotTrackChanges">Tells change tracker do not track any changes for these entities.
        /// In such case Save() and SaveAsync() methods do not affect fetched entities</param>
        /// <param name="includeProperties">Navigation properties to include</param>
        /// <returns>A collection fetched from the database</returns>
        Task<IEnumerable<TEntity>> GetEntityAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int skip,
            int take,
            bool doNotTrackChanges,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            string[] includeProperties) where TEntity : class, IGenericEntity<TId>;

        /// <summary>
        /// Adds entity without saving to the database. Call Save or SaveAsync to save changes to the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isUpdating">True - if it is updating of existing entity, false - if entity is new, by default null - if not sure</param>
        /// <returns>Id of entity that will be added to the database</returns>
        TId Add<TEntity>(TEntity entity, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<TId>;

        /// <summary>
        /// Adds dto as TEntity without saving. Call Save or SaveAsync to save the entity to the database
        /// </summary>
        /// <typeparam name="TEntity">Type to which dto should be mapped</typeparam>
        /// <param name="dto"></param>
        /// <returns>Id of entity that will be added to the database</returns>
        /// <summary>
        /// Adds dto as TEntity without saving. Call Save or SaveAsync to save the entity to the database
        /// </summary>
        /// <typeparam name="TEntity">Type to which dto should be mapped</typeparam>
        /// <param name="dto"></param>
        /// <param name="isUpdating">True - if it is updating of existing entity, false - if entity is new, by default null - if not sure</param>
        /// <returns>Id of entity that will be added to the database</returns>
        TId AddDto<TEntity>(IGenericEntity<TId> dto, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<TId>;

        /// <summary>
        /// Marks entity as deleted. Call Save or SaveAsync to submit changes
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="softDeleting">Set to true if an entity should not be deleted from the database</param>
        /// <returns>Entity that will be removed from the database after saving changes</returns>
        /// 
        Task<TEntity> RemoveAsync<TEntity>(TId id, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<TId>;

        /// <summary>
        /// Finds in context TEntity with id
        /// Marks entity as deleted. Call Save or SaveAsync to submit changes
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id">Id of TEntity</param>
        /// <param name="softDeleting">Set to true if an entity should not be deleted from the database</param>
        /// <returns>Entity that will be removed from the database after saving changes</returns>
        TEntity Remove<TEntity>(TId id, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<TId>;

        /// <summary>
        /// Marks entity as deleted. Call Save or SaveAsync to submit changes
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="softDeleting">Set to true if an entity should not be deleted from the database</param>
        /// <returns>Entity that will be removed from the database after saving changes</returns>
        TEntity Remove<TEntity>(TEntity entity, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<TId>;

        /// <summary>
        /// Saves all made changes to the database
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all made changes to the database
        /// </summary>
        Task SaveChangesAsync();
    }
}
