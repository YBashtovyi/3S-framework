using Core.Base.Data;
using Core.Common.Helpers;
using Core.Data;
using Core.Services.Data;
using Core.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Business.Services
{
    public class CommonLongIdDataService : ICommonGenericDataService<long>
    {
        #region Fields_And_Properties

        protected ICommonGenericRepository<long> Repository { get; }
        private readonly IObjectMapper _mapper;

        #endregion Fields_And_Properties

        #region Constructors

        public CommonLongIdDataService(ICommonGenericRepository<long> repository, IObjectMapper objectMapper)
        {
            Repository = repository;
            _mapper = objectMapper;
        }

        #endregion Constructors

        #region Methods_Public

        public IEnumerable<Type> GetApplicationModels(bool withPrimaryKeysOnly)
        {
            return Repository.GetApplicationModels(withPrimaryKeysOnly);
        }

        public IEnumerable<TDto> GetDto<TDto>(Expression<Func<TDto, bool>> predicate, Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            return Repository.GetDto(predicate, orderBy, parameters, skip, take, cacheResultDuration, fromUser, extraParameters);
        }

        public IEnumerable<TDto> GetDto<TDto>(string orderBy, Expression<Func<TDto, bool>> predicate, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            return Repository.GetDto(orderBy, predicate, parameters, skip, take, cacheResultDuration, fromUser, extraParameters);
        }

        public async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(Expression<Func<TDto, bool>> predicate, Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            return await Repository.GetDtoAsync(predicate, orderBy, parameters, skip, take, cacheResultDuration, fromUser, extraParameters);
        }

        public async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(string orderBy, Expression<Func<TDto, bool>> predicate, IDictionary<string, string> parameters, int skip, int take, int cacheResultDuration, BaseUserInfo fromUser, object[] extraParameters) where TDto : class
        {
            return await Repository.GetDtoAsync(orderBy, predicate, parameters, skip, take, cacheResultDuration, fromUser, extraParameters);
        }

        public void SaveChanges()
        {
            Repository.Save();
        }

        public async Task SaveChangesAsync()
        {
            await Repository.SaveAsync();
        }

        public long Add<TEntity>(TEntity entity, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            return AddInternal(entity, isUpdating, fromUser);
        }

        public long AddDto<TEntity>(IGenericEntity<long> dto, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            return AddDtoInternal<TEntity>(dto, isUpdating, fromUser);
        }

        public void AddDtoRange<TEntity>(IEnumerable<IGenericEntity<long>> collection, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            if (collection != null)
            {
                foreach (var dto in collection)
                {
                    dto.Id = AddDtoInternal<TEntity>(dto, isUpdating, fromUser);
                }
            }
        }

        public IEnumerable<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int skip, int take, bool doNotTrackChanges, int cacheResultDuration, BaseUserInfo fromUser, params string[] includeProperties)
            where TEntity : class, IGenericEntity<long>
        {
            // TODO: implement caching
            return Repository.GetEntity(predicate, orderBy, skip, take, doNotTrackChanges, fromUser, includeProperties);
        }

        public async Task<IEnumerable<TEntity>> GetEntityAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int skip,
            int take,
            bool doNotTrackChanges,
            int cacheResultDuration,
            BaseUserInfo fromUser,
            params string[] includeProperties) where TEntity : class, IGenericEntity<long>
        {
            // TODO: implement caching
            return await Repository.GetEntityAsync(predicate, orderBy, skip, take, doNotTrackChanges, fromUser, includeProperties);
        }

        public async Task<TEntity> RemoveAsync<TEntity>(long id, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            var entity = (await Repository.GetEntityAsync<TEntity>(x => x.Id == id,
                null,
                0,
                0,
                false,
                fromUser,
                null)).SingleOrDefault();
            return RemoveInternal(entity, softDeleting, fromUser);
        }

        public TEntity Remove<TEntity>(long id, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            var entity = Repository.GetEntity<TEntity>(x => x.Id == id,
                null,
                0,
                0,
                false,
                fromUser,
                null).SingleOrDefault();
            return RemoveInternal(entity, softDeleting, fromUser);
        }

        public TEntity Remove<TEntity>(TEntity entity, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            return RemoveInternal(entity, softDeleting, fromUser);
        }

        #endregion MethodsPublic

        #region Methods_Private

        private long AddInternal<TEntity>(TEntity entity, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            if (isUpdating == false)
            {
                Repository.Add(entity, fromUser);
            }
            else if (isUpdating == true)
            {
                Repository.Update(entity, fromUser);
            }
            else
            {
                var entityToUpdate = Repository.GetEntity<TEntity>(x => x.Id == entity.Id,
                    null,
                    0,
                    0,
                    false,
                    fromUser,
                    null).FirstOrDefault();
                if (entityToUpdate == null)
                {
                    isUpdating = false;
                    Repository.Add(entity, fromUser);
                }
                else
                {
                    isUpdating = true;
                    Repository.Update(_mapper.Map(entity, entityToUpdate), fromUser);
                }
            }

            if (entity is IDerivedEntity derived && derived.BaseType != null)
            {
                var currentType = typeof(TEntity);
                var baseEntity = _mapper.Map(derived, currentType, derived.BaseType);
                if (baseEntity is IDerivableEntity derivable)
                {
                    derivable.DerivedEntity = currentType.Name;
                }
                if (baseEntity != null)
                {
                    ReflectionHelper.InvokeGenericMethod(this, derived.BaseType, nameof(AddInternal), new object[] { baseEntity, isUpdating, fromUser });
                }
            }

            return entity.Id;
        }

        private long AddDtoInternal<TEntity>(IGenericEntity<long> dto, bool? isUpdating, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
        {
            TEntity entity = null;
            if (isUpdating == false)
            {
                entity = _mapper.Map<TEntity>(dto);
                AddInternal(entity, isUpdating, fromUser);
            }
            else
            {
                var entityToUpdate = Repository.GetEntity<TEntity>(x => x.Id == dto.Id,
                    null,
                    0,
                    0,
                    false,
                    fromUser,
                    null).FirstOrDefault();
                if (entityToUpdate == null)
                {
                    if (isUpdating == true)
                    {
                        return default;
                    }
                    else
                    {
                        entity = _mapper.Map<TEntity>(dto);
                        AddInternal(entity, false, fromUser);
                    }
                }
                else
                {
                    entity = _mapper.Map(dto, entityToUpdate);
                    AddInternal(entity, true, fromUser);
                }
            }

            return entity == null ? default : entity.Id;
        }

        private TEntity RemoveInternal<TEntity>(TEntity entity, bool softDeleting, BaseUserInfo fromUser) where TEntity : class, IGenericEntity<long>
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
                    Repository.Update(entity, fromUser);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot softly delete an entity of type {entity.GetType().Name} " +
                        $"because this entity does not implement {nameof(IRecordState)} interface");
                }
            }
            else
            {
                Repository.Remove(entity, fromUser);
            }

            if (entity is IDerivedEntity derived && derived?.BaseType != null)
            {
                var currentType = typeof(TEntity);
                var baseEntity = _mapper.Map(derived, currentType, derived.BaseType);
                if (baseEntity != null)
                {
                    ReflectionHelper.InvokeGenericMethod(this, derived.BaseType, nameof(RemoveInternal), new object[] { baseEntity, softDeleting, fromUser });
                }
            }

            return entity;
        }

        #endregion Methods_Private

    }
}
