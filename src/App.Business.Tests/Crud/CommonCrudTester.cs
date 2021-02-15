using System;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Helpers;
using Xunit;

namespace App.Business.Tests.Crud
{
    public class CommonCrudTester
    {
        [Theory]
        [InlineData(typeof(EnumRecord), null)]
        //[InlineData(typeof(EhealthOrganization), null)]
        //[InlineData(typeof(EhealthOrganization), typeof(EhealthOrganizationDto))]
        public void CanCrud(Type type, Type dtoType)
        {
            // skip model testing if dto is passed to method
            if (dtoType == null)
            {
                var wasAdded = (bool)ReflectionHelper.InvokeGenericMethod(this, type, nameof(Add), null);
                Assert.True(wasAdded);
                if (!wasAdded)
                {
                    return;
                }

                var id = (Guid)ReflectionHelper.InvokeGenericMethod(this, type, nameof(AddSave), null);
                Assert.NotEqual(Guid.Empty, id);
                if (id == Guid.Empty)
                {
                    return;
                }

                var savedEntityWasRead = (bool)ReflectionHelper.InvokeGenericMethod(this, type, nameof(ReadSaved), new object[] { id });
                Assert.True(savedEntityWasRead);
                if (!savedEntityWasRead)
                {
                    return;
                }

                var wasUpdated = (bool)ReflectionHelper.InvokeGenericMethod(this, type, nameof(AddUpdate), new object[] { id });
                Assert.True(wasUpdated);
                if (!wasUpdated)
                {
                    return;
                }

                var deletedSoftly = (bool)ReflectionHelper.InvokeGenericMethod(this, type, nameof(DeleteSoftly), new object[] { id });
                Assert.True(deletedSoftly);
                if (!deletedSoftly)
                {
                    return;
                }

                var deletedTotally = (bool)ReflectionHelper.InvokeGenericMethod(this, type, nameof(DeleteTotally), new object[] { id });
                Assert.True(deletedTotally);
                if (!deletedTotally)
                {
                    return;
                }
            }
            else
            {
                var types = new Type[] { type, dtoType };
                var wasAdded = (bool)ReflectionHelper.InvokeGenericMethod(this, types, nameof(AddDto), null);
                Assert.True(wasAdded);
                if (!wasAdded)
                {
                    return;
                }

                var id = (Guid)ReflectionHelper.InvokeGenericMethod(this, types, nameof(AddSaveDto), null);
                Assert.NotEqual(Guid.Empty, id);
                if (id == Guid.Empty)
                {
                    return;
                }

                var savedDtoWasRead = (bool)ReflectionHelper.InvokeGenericMethod(this, dtoType, nameof(ReadSavedDto), new object[] { id });
                Assert.True(savedDtoWasRead);
                if (!savedDtoWasRead)
                {
                    return;
                }
                
                var wasUpdated = (bool)ReflectionHelper.InvokeGenericMethod(this, types, nameof(AddUpdateDto), new object[] { id });
                Assert.True(wasUpdated);
                if (!wasUpdated)
                {
                    return;
                }

                var deletedSoftly = (bool)ReflectionHelper.InvokeGenericMethod(this, types, nameof(DeleteDtoSoftly), new object[] { id });
                Assert.True(deletedSoftly);
                if (!deletedSoftly)
                {
                    return;
                }

                var deletedTotally = (bool)ReflectionHelper.InvokeGenericMethod(this, types, nameof(DeleteDtoTotally), new object[] { id });
                Assert.True(deletedTotally);
                if (!deletedTotally)
                {
                    return;
                }

            }
        }

        #region Add

        private bool Add<TEntity>() where TEntity : class, IEntity
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.Add<TEntity>(dataService);
        }

        private bool AddDto<TEntity, TDto>() where TEntity : class, IEntity where TDto: BaseDto
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.AddDto<TEntity, TDto>(dataService);
        }

        #endregion Add

        #region AddWithSave

        private Guid AddSave<TEntity>() where TEntity : class, IEntity
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.AddSave<TEntity>(dataService);
        }

        private Guid AddSaveDto<TEntity, TDto>() where TEntity : class, IEntity where TDto : BaseDto
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.AddSaveDto<TEntity, TDto>(dataService);
        }

        #endregion AddWithSave

        #region ReadSaved

        private bool ReadSaved<TEntity>(Guid id) where TEntity : class, IEntity
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.ReadSaved<TEntity>(dataService, id);
        }

        private bool ReadSavedDto<TDto>(Guid id) where TDto : BaseDto
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.ReadSavedDto<TDto>(dataService, id);
        }

        #endregion ReadSaved

        #region AddUpdateSave

        private bool AddUpdate<TEntity>(Guid id) where TEntity : class, IEntity
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.AddUpdate<TEntity>(dataService, id);
        }

        private bool AddUpdateDto<TEntity, TDto>(Guid id) where TEntity : class, IEntity where TDto : BaseDto
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.AddUpdateDto<TEntity, TDto>(dataService, id);
        }

        #endregion

        #region DeleteSave

        private bool DeleteSoftly<TEntity>(Guid id) where TEntity : class, IEntity, IRecordState
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.DeleteSoftly<TEntity>(dataService, id);
        }

        private bool DeleteTotally<TEntity>(Guid id) where TEntity : class, IEntity, IRecordState
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.DeleteTotally<TEntity>(dataService, id);
        }

        private bool DeleteDtoSoftly<TEntity, TDto>(Guid id) where TEntity : class, IEntity, IRecordState where TDto : BaseDto
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.DeleteDtoSoftly<TEntity, TDto>(dataService, id);
        }

        private bool DeleteDtoTotally<TEntity, TDto>(Guid id) where TEntity : class, IEntity where TDto : BaseDto
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            return DataServiceCrudHelper.DeleteDtoTotally<TEntity, TDto>(dataService, id);
        }

        #endregion DeleteSave
    }
}
