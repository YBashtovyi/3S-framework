using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Base.Data;
using Core.Services.Data;

namespace App.Business.Tests.Crud
{
    public static class DataServiceCrudHelper
    {
        public static bool Add<TEntity>(ICommonDataService dataService, TEntity instance = null) where TEntity : class, IEntity
        {
            if (instance == null)
            {
                instance = InstanceCreator.Create<TEntity>();
            }

            dataService.Add<TEntity>(instance, null);
            return instance.Id != Guid.Empty;
        }

        public static bool AddDto<TEntity, TDto>(ICommonDataService dataService, TDto instance = null) where TEntity : class, IEntity where TDto : BaseDto
        {
            if (instance == null)
            {
                instance = InstanceCreator.Create<TDto>(typeof(TEntity));
            }

            var id = dataService.AddDto<TEntity>(instance, null);
            return id != Guid.Empty;
        }

        public static Guid AddSave<TEntity>(ICommonDataService dataService, TEntity instance = null) where TEntity : class, IEntity
        {
            if (instance == null)
            {
                instance = InstanceCreator.Create<TEntity>();
            }

            var id = dataService.Add<TEntity>(instance, false);
            dataService.SaveChanges();

            return id;
        }

        public static Guid AddSaveDto<TEntity, TDto>(ICommonDataService dataService, TDto instance = null) where TEntity : class, IEntity where TDto : BaseDto
        {
            if (instance == null)
            {
                instance = InstanceCreator.Create<TDto>(typeof(TEntity));
            }

            var id = dataService.AddDto<TEntity>(instance, false);
            dataService.SaveChanges();

            return id;
        }

        public static bool ReadSaved<TEntity>(ICommonDataService dataService, Guid id) where TEntity : class, IEntity
        {
            var entity = dataService.GetEntity<TEntity>(predicate: x => x.Id == id, true).Single();
            return entity.Id == id;
        }

        public static bool ReadSavedDto<TDto>(ICommonDataService dataService, Guid id) where TDto : BaseDto
        {
            var entity = dataService.GetDto<TDto>(x => x.Id == id, null, null, 0, 0, 0, null, new object[0]).Single();
            return entity.Id == id;
        }

        public static bool AddUpdate<TEntity>(ICommonDataService dataService, Guid id, TEntity instance = null) where TEntity : class, IEntity
        {
            if (instance == null)
            {
                instance = InstanceCreator.Create<TEntity>();
            }

            instance.Id = id;
            instance.RecordState = RecordState.Project;
            dataService.Add<TEntity>(instance, true);

            dataService.SaveChanges();

            var entity = dataService.GetEntity<TEntity>(predicate: x => x.Id == id, false).Single();

            return entity.RecordState == RecordState.Project;
        }

        public static bool AddUpdateDto<TEntity, TDto>(ICommonDataService dataService, Guid id, TDto instance = null) where TEntity : class, IEntity where TDto : BaseDto
        {
            if (instance == null)
            {
                instance = InstanceCreator.Create<TDto>(typeof(TEntity));
            }

            var caption = "Updated caption";
            instance.Id = id;
            instance.Caption = caption;
            dataService.AddDto<TEntity>(instance, true);

            dataService.SaveChanges();

            var dto = dataService.GetDto<TDto>(x => x.Id == id, null, null, 0, 0, 0, null, new object[0]).Single();

            return dto.Caption == caption;
        }

        public static bool DeleteSoftly<TEntity>(ICommonDataService dataService, Guid id) where TEntity : class, IEntity, IRecordState
        {
            var entity = Delete<TEntity>(dataService, id, true);
            return entity.RecordState == RecordState.Deleted;
        }

        public static bool DeleteTotally<TEntity>(ICommonDataService dataService, Guid id) where TEntity : class, IEntity, IRecordState
        {
            var entity = Delete<TEntity>(dataService, id, false);
            var entityDeleted = dataService.GetEntity<TEntity>(x => x.Id == id, false).SingleOrDefault() == null;

            return entityDeleted;
        }

        public static bool DeleteDtoSoftly<TEntity, TDto>(ICommonDataService dataService, Guid id) where TEntity : class, IEntity, IRecordState where TDto : BaseDto
        {
            var entity = Delete<TEntity>(dataService, id, true);
            var dto = dataService.GetDto<TDto>(x => x.Id == id, null, null, 0, 0, 0, null, new object[0]).SingleOrDefault();
            var entityDeleted = dataService.GetEntity<TEntity>(x => x.Id == id, false).SingleOrDefault() == null;

            return dto == null && !entityDeleted;
        }

        public static bool DeleteDtoTotally<TEntity, TDto>(ICommonDataService dataService, Guid id) where TEntity : class, IEntity where TDto : BaseDto
        {
            var entity = Delete<TEntity>(dataService, id, false);
            var dto = dataService.GetDto<TDto>(x => x.Id == id, null, null, 0, 0, 0, null, new object[0]).SingleOrDefault();
            var entityDeleted = dataService.GetEntity<TEntity>(x => x.Id == id, false).SingleOrDefault() == null;

            return dto == null && entityDeleted;
        }

        private static TEntity Delete<TEntity>(ICommonDataService dataService, Guid id, bool softDeleting) where TEntity : class, IEntity
        {
            var entity = dataService.Remove<TEntity>(id, softDeleting);
            dataService.SaveChanges();

            return entity;
        }
    }
}
