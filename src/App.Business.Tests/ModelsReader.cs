using System;
using System.Linq;
using App.Data.Models;
using Core.Common.Helpers;
using Core.Data;
using Core.Models;
using Core.Security.Models;
using Xunit;

namespace App.Business.Tests
{
    public class ModelsReader
    {
        private bool CanReadModelInternal<TModel>() where TModel : class
        {
            var context = IntegrationBase.CreateContextInstance();
            context.Set<TModel>().Any();

            return true;
        }

        [Theory]
        [InlineData(typeof(AuditHistory))]
        [InlineData(typeof(NumberCounter))]
        [InlineData(typeof(Person))]
        [InlineData(typeof(OrgUnit))]
        [InlineData(typeof(EntityRelation))]
        [InlineData(typeof(PrintedFormTemplate))]
        [InlineData(typeof(Organization))]
        [InlineData(typeof(Department))]
        [InlineData(typeof(Employee))]
        [InlineData(typeof(EnumRecord))]
        [InlineData(typeof(ExtendedProperty))]
        [InlineData(typeof(EntityExtendedPropertyValue))]
        [InlineData(typeof(FileStore))]
        [InlineData(typeof(CryptoSignFieldSetting))]
        //
        [InlineData(typeof(ApplicationSetting))]
        //
        [InlineData(typeof(FieldRight))]
        [InlineData(typeof(Profile))]
        [InlineData(typeof(ProfileRole))]
        [InlineData(typeof(ProfileRight))]
        [InlineData(typeof(Right))]
        [InlineData(typeof(Role))]
        [InlineData(typeof(RoleRight))]
        [InlineData(typeof(ApplicationRowLevelRight))]
        [InlineData(typeof(RowLevelRight))]
        [InlineData(typeof(RowLevelSecurityObject))]
        [InlineData(typeof(OperationRight))]
        [InlineData(typeof(RoleOperationRight))]
        [InlineData(typeof(ProfileOperationRight))]
        [InlineData(typeof(UserProfile))]
        [InlineData(typeof(UserDefaultValue))]
        //
        [InlineData(typeof(ScheduleSetting))]
        [InlineData(typeof(ScheduleResource))]
        [InlineData(typeof(ScheduleSettingProperty))]
        [InlineData(typeof(ScheduleSlot))]
        [InlineData(typeof(ScheduleTime))]
        #region notification
        [InlineData(typeof(Notification))]
        [InlineData(typeof(NotificationReceiver))]
        #endregion
        public void CanReadModel(Type modelType)
        {
            var wasRead = (bool)ReflectionHelper.InvokeGenericMethod(this, modelType, nameof(CanReadModelInternal), null);
            Assert.True(wasRead);
        }
    }
}
