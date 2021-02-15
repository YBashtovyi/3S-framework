using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Data;

namespace Core.Services.Data
{
    /// <summary>
    /// Extensions for ICommonDataService
    /// </summary>
    public static class ICommonDataServiceExtensions
    {

        #region GetDtoAsync
        /// <summary>
        /// Gets dto collection using predicate
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Dto collection that matches predicate</returns>
        public static async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(this ICommonDataService dataService, Expression<Func<TDto, bool>> predicate) where TDto : class
        {
            return await dataService.GetDtoAsync(predicate, null, null, 0, 0, 0, null, new object[0]);
        }

        /// <summary>
        /// Gets dto collection using predicate and concrete user
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used</param>
        /// <returns>Dto collection that matches predicate</returns>
        public static async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            BaseUserInfo fromUser) where TDto : class
        {
            return await dataService.GetDtoAsync(predicate, null, null, 0, 0, 0, fromUser, new object[0]);
        }

        /// <summary>
        /// Gets dto collection using predicate and concrete user
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="parameters">Parameters that will be converted to 'where' conditions.
        /// Dictionary key - dto field name, dictionary value - conditions for this field</param>
        /// <returns>Dto collection that matches predicate</returns>
        public static async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate, 
            IDictionary<string, string> parameters) where TDto : class
        {
            return await dataService.GetDtoAsync(predicate, null, parameters, 0, 0, 0, null, new object[0]);
        }

        /// <summary>
        /// Gets dto collection using predicate and concrete user
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="parameters">Parameters that will be converted to 'where' conditions.
        /// Dictionary key - dto field name, dictionary value - conditions for this field</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used</param>
        /// <returns>Dto collection that matches predicate</returns>
        public static async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            IDictionary<string, string> parameters,
            BaseUserInfo fromUser) where TDto : class
        {
            return await dataService.GetDtoAsync(predicate, null, parameters, 0, 0, 0, fromUser, new object[0]);
        }

        /// <summary>
        /// Gets dto collection using predicate and concrete user
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">Sorting function that will be applied to resulting collection</param>
        /// <returns>Ordered dto collection that matches predicate</returns>
        public static async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy) where TDto : class
        {
            return await dataService.GetDtoAsync(predicate, orderBy, null, 0, 0, 0, null, new object[0]);
        }

        /// <summary>
        /// Gets dto collection using predicate and concrete user
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">Sorting function that will be applied to resulting collection</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used<</param>
        /// <returns>Ordered dto collection that matches predicate</returns>
        public static async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy,
            BaseUserInfo fromUser) where TDto : class
        {
            return await dataService.GetDtoAsync(predicate, orderBy, null, 0, 0, 0, fromUser, new object[0]);
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">Sorting function that will be applied to resulting collection</param>
        /// <returns>A task that represents the asynchronous operation. The task result is null
        ///  if source is empty; otherwise, the first element in source.
        /// </returns>
        public static async Task<TDto> FirstOrDefaultAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, orderBy, null, 0, 1, 0, null, new object[0]))
                .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns>A task that represents the asynchronous operation. The task result is null
        ///  if source is empty; otherwise, the first element in source.
        /// </returns>
        public static async Task<TDto> FirstOrDefaultAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 1, 0, null, new object[0]))
                .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">Sorting function that will be applied to resulting collection</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used</param>
        /// <returns>A task that represents the asynchronous operation. The task result is null
        ///  if source is empty; otherwise, the first element in source.
        /// </returns>
        public static async Task<TDto> FirstOrDefaultAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy,
            BaseUserInfo fromUser) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, orderBy, null, 0, 1, 0, fromUser, new object[0]))
                .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used</param>
        /// <returns>A task that represents the asynchronous operation. The task result is null
        ///  if source is empty; otherwise, the first element in source.
        /// </returns>
        public static async Task<TDto> FirstOrDefaultAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            BaseUserInfo fromUser) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 1, 0, fromUser, new object[0]))
                .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the only element of a sequence that satisfies a specified
        ///   condition or a default value if no such element exists; this method throws an
        ///   exception if more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns>A task that represents the asynchronous operation. The task result is null
        ///  if source is empty; otherwise, the first element in source.
        /// </returns>
        public static async Task<TDto> SingleOrDefaultAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 2, 0, null, new object[0]))
                .SingleOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the only element of a sequence that satisfies a specified
        ///   condition or a default value if no such element exists; this method throws an
        ///   exception if more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used</param>
        /// <returns>A task that represents the asynchronous operation. The task result is null
        ///  if source is empty; otherwise, the first element in source.
        /// </returns>
        public static async Task<TDto> SingleOrDefaultAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            BaseUserInfo fromUser) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 2, 0, fromUser, new object[0]))
                .SingleOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the only element of a sequence, and throws an exception
        ///     if there is not exactly one element in the sequence.
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns> 
        /// A task that represents the asynchronous operation. The task result contains the single element of the input sequence.
        /// </returns>
        public static async Task<TDto> SingleAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 2, 0, null, new object[0]))
                .Single();
        }

        /// <summary>
        /// Asynchronously returns the only element of a sequence, and throws an exception
        ///     if there is not exactly one element in the sequence.
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used</param>
        /// <returns> 
        /// A task that represents the asynchronous operation. The task result contains the single element of the input sequence.
        /// </returns>
        public static async Task<TDto> SingleAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            BaseUserInfo fromUser) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 2, 0, fromUser, new object[0]))
                .Single();
        }

        /// <summary>
        /// Determines whether a sequence contains any element
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns>
        /// true if the source sequence contains any element; otherwise, false
        /// </returns>
        public static async Task<bool> AnyAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 1, 0, null, new object[0]))
                .Any();
        }

        /// <summary>
        /// Determines whether a sequence contains any element
        /// </summary>
        /// <typeparam name="TDto">Query result will be mapped to this type</typeparam>
        /// <param name="dataService"></param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="fromUser">User object. Is needed for rights check if rights are used</param>
        /// <returns>
        /// true if the source sequence contains any element; otherwise, false
        /// </returns>
        public static async Task<bool> AnyAsync<TDto>(this ICommonDataService dataService,
            Expression<Func<TDto, bool>> predicate,
            BaseUserInfo fromUser) where TDto : class
        {
            return (await dataService.GetDtoAsync(predicate, null, null, 0, 1, 0, fromUser, new object[0]))
                .Any();
        }

        #endregion GetDtoAsync

        #region Add

        public static Guid Add<TEntity>(this ICommonDataService dataService,
            TEntity entity,
            bool? isUpdating) where TEntity : class, IGenericEntity<Guid>
        {
            return dataService.Add<TEntity>(entity, isUpdating, null);
        }

        #endregion

        #region AddDto

        public static Guid AddDto<TEntity>(this ICommonDataService dataService, IGenericEntity<Guid> dto, bool? isUpdating) where TEntity : class, IGenericEntity<Guid>
        {
            return dataService.AddDto<TEntity>(dto, isUpdating, null);
        }

        #endregion AddDto

        #region AddDtoRange

        public static void AddDtoRange<TEntity>(this ICommonDataService dataService, IEnumerable<IGenericEntity<Guid>> collection,
            bool? isUpdating,
            BaseUserInfo fromUser)
            where TEntity : class, IGenericEntity<Guid>
        {
            if (collection != null)
            {
                foreach (var dto in collection)
                {
                    dto.Id = dataService.AddDto<TEntity>(dto, isUpdating, fromUser);
                }
            }
        }

        public static void AddDtoRange<TEntity>(this ICommonDataService dataService, IEnumerable<IGenericEntity<Guid>> collection,
            bool? isUpdating)
            where TEntity : class, IGenericEntity<Guid>
        {
            dataService.AddDtoRange<TEntity>(collection, isUpdating, null);
        }

        #endregion AddDtoRange

        #region Remove

        public static TEntity Remove<TEntity>(this ICommonDataService dataService, Guid id, bool softDeleting) where TEntity : class, IGenericEntity<Guid>
        {
            return dataService.Remove<TEntity>(id, softDeleting, null);
        }

        public static TEntity Remove<TEntity>(this ICommonDataService dataService, Guid id) where TEntity : class, IGenericEntity<Guid>
        {
            return dataService.Remove<TEntity>(id, true);
        }

        #endregion Remove

        #region RemoveDtoRange

        public static void RemoveRange<TEntity>(this ICommonDataService dataService, 
            IEnumerable<Guid> collection,
            bool softDeleting,
            BaseUserInfo fromUser) where TEntity : class, IGenericEntity<Guid>
        {
            if (collection != null && collection.Any())
            {
                foreach (var entityId in collection)
                {
                    dataService.Remove<TEntity>(entityId, softDeleting, fromUser);
                }
            }
        }

        public static void RemoveRange<TEntity>(this ICommonDataService dataService,
            IEnumerable<Guid> collection,
            bool softDeleting) where TEntity : class, IGenericEntity<Guid>
        {
            dataService.RemoveRange<TEntity>(collection, softDeleting, null);
        }

        public static void RemoveRange<TEntity>(this ICommonDataService dataService,
           IEnumerable<Guid> collection) where TEntity : class, IGenericEntity<Guid>
        {
            dataService.RemoveRange<TEntity>(collection, true);
        }

        #endregion RemoveDtoRange

        #region GetEntityAsync

        public static async Task<IEnumerable<TEntity>> GetEntityAsync<TEntity>(this ICommonDataService dataService, Expression<Func<TEntity, bool>> predicate, bool doNotTrackChanges) where TEntity : class, IGenericEntity<Guid>
        {
            return await dataService.GetEntityAsync(predicate, null, 0, 0, doNotTrackChanges, 0, null, null);
        }

        public static async Task<IEnumerable<TEntity>> GetEntityAsync<TEntity>(this ICommonDataService dataService,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            bool doNotTrackChanges) where TEntity : class, IGenericEntity<Guid>
        {
            return await dataService.GetEntityAsync(predicate, orderBy, 0, 0, doNotTrackChanges, 0, null, null);
        }

        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this ICommonDataService dataService,
            Expression<Func<TEntity, bool>> predicate, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, 
            bool doNotTrackChanges) where TEntity : class, IGenericEntity<Guid>
        {
            return (await dataService.GetEntityAsync(predicate, orderBy, 0, 1, doNotTrackChanges, 0, null, null))
                .FirstOrDefault();
        }

        public static async Task<TEntity> SingleOrDefaultAsync<TEntity>(this ICommonDataService dataService,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            bool doNotTrackChanges) where TEntity : class, IGenericEntity<Guid>
        {
            return (await dataService.GetEntityAsync(predicate, orderBy, 0, 2, doNotTrackChanges, 0, null, null))
                .SingleOrDefault();
        }

        public static async Task<TEntity> SingleAsync<TEntity>(this ICommonDataService dataService,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            bool doNotTrackChanges) where TEntity : class, IGenericEntity<Guid>
        {
            return (await dataService.GetEntityAsync(predicate, orderBy, 0, 2, doNotTrackChanges, 0, null, null))
                .Single();
        }

        #endregion GetEntityAsync

        #region GetEntity

        public static IEnumerable<TEntity> GetEntity<TEntity>(this ICommonDataService dataService, Expression<Func<TEntity, bool>> predicate, bool doNotTrackChanges) where TEntity : class, IGenericEntity<Guid>
        {
            return dataService.GetEntity(predicate, null, 0, 0, doNotTrackChanges, 0, null, null);
        }

        #endregion GetEntity

    }
}
