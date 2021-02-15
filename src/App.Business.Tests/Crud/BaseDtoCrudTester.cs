using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Services.Data;
using Xunit;

namespace App.Business.Tests.Crud
{
    [TestCaseOrderer("App.Business.Tests.PriorityOrderer", "App.Business.Tests")]
    public abstract class BaseDtoCrudTester<TEntity, TDto> where TEntity : class, IEntity where TDto: BaseDto
    {
        [Fact, TestPriority(-100)]
        public abstract void CanPrepareTestData();

        [Fact, TestPriority(0)]
        public virtual void CanAdd()
        {
            var dto = GetTestObject();
            var added = DataServiceCrudHelper.AddDto<TEntity, TDto>(GetDataService(), dto);

            Assert.True(added);
        }

        [Fact, TestPriority(5)]
        public virtual void CanAddAndSave()
        {
            var dto = GetTestObject();

            dto.Id = Guid.Empty;
            dto.Id = DataServiceCrudHelper.AddSaveDto<TEntity, TDto>(GetDataService(), dto);

            Assert.NotEqual(Guid.Empty, dto.Id);
        }

        [Fact, TestPriority(10)]
        public virtual void CanReadSavedEntity()
        {
            var wasRead = DataServiceCrudHelper.ReadSavedDto<TDto>(GetDataService(), GetTestObject().Id);

            Assert.True(wasRead);
        }

        [Fact, TestPriority(15)]
        public virtual void CanAddAndUpdate()
        {
            var dto = GetTestObject();
            var updated = DataServiceCrudHelper.AddUpdateDto<TEntity, TDto>(GetDataService(), GetTestObject().Id, dto);
            Assert.True(updated);
        }

        [Fact, TestPriority(15)]
        public virtual void CanDeleteSoftly()
        {
            var deleted = DataServiceCrudHelper.DeleteSoftly<TEntity>(GetDataService(), GetTestObject().Id);
            Assert.True(deleted);
        }

        [Fact, TestPriority(15)]
        public virtual void CanDeleteTotally()
        {
            var deleted = DataServiceCrudHelper.DeleteTotally<TEntity>(GetDataService(), GetTestObject().Id);
            Assert.True(deleted);
        }

        protected abstract TDto GetTestObject();

        protected virtual ICommonDataService GetDataService() => IntegrationBase.CreateDataServiceInstance();
    }
}
