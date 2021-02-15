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
    public abstract class BaseCrudTester<TEntity> where TEntity: class, IEntity
    {
        [Fact, TestPriority(-100)]
        public abstract void CanPrepareTestData();

        [Fact, TestPriority(0)]
        public virtual void CanAdd()
        {
            var entity = GetTestObject();
            var added = DataServiceCrudHelper.Add<TEntity>(GetDataService(), entity);

            Assert.True(added);
        }

        [Fact, TestPriority(5)]
        public virtual void CanAddAndSave()
        {
            var entity = GetTestObject();

            entity.Id = Guid.Empty;
            entity.Id = DataServiceCrudHelper.AddSave<TEntity>(GetDataService(), entity);

            Assert.NotEqual(Guid.Empty, entity.Id);
        }

        [Fact, TestPriority(10)]
        public virtual void CanReadSavedEntity()
        {
            var wasRead = DataServiceCrudHelper.ReadSaved<TEntity>(GetDataService(), GetTestObject().Id);

            Assert.True(wasRead);
        }

        [Fact, TestPriority(15)]
        public virtual void CanAddAndUpdate()
        {
            var entity = GetTestObject();
            var updated = DataServiceCrudHelper.AddUpdate<TEntity>(GetDataService(), GetTestObject().Id, entity);
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

        protected abstract TEntity GetTestObject();

        protected virtual ICommonDataService GetDataService() => IntegrationBase.CreateDataServiceInstance();
    }
}
