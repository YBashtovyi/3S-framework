using System;
using App.Data.Dto.Administration;
using App.Data.Dto.Common;
using App.Data.Dto.ElectronicQueue;
using App.Data.Dto.System;
using Core.Common.Helpers;
using Core.Data.Helpers;
using Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace App.Business.Tests
{
    public class DtoReader
    {
        #region FieldsAndProperties
        private static readonly Mock<ILogger<PostgresQueryTextService>> _textServiceLoggerMock;
        private static readonly Mock<PostgresQueryConditionsBuilder> _queryConditionsBuilderMock;
        #endregion FieldsAndProperties

        #region Constructors
        static DtoReader()
        {
            _textServiceLoggerMock = new Mock<ILogger<PostgresQueryTextService>>();
            _queryConditionsBuilderMock = new Mock<PostgresQueryConditionsBuilder>();
        }
        #endregion Constructors

        #region Tests
        [Theory]
        [InlineData(typeof(EnumRecordDto))]
        [InlineData(typeof(PrintedFormTemplateDto))]
        [InlineData(typeof(FileStoreDto))]
        [InlineData(typeof(FileStoreFullDto))]
        [InlineData(typeof(CryptoSignFieldSettingDto))]
        [InlineData(typeof(SysEvaluatedValueDto))]
        [InlineData(typeof(PendingChangeDto))]
        [InlineData(typeof(PendingChangeDetailDto))]

        #region Security
        [InlineData(typeof(ApplicationRowLevelRightDto))]
        [InlineData(typeof(ApplicationRowLevelRightListDto))]
        [InlineData(typeof(ApplicationRowLevelRightDetailDto))]
        [InlineData(typeof(FieldRightDto))]
        [InlineData(typeof(FieldRightListDto))]
        [InlineData(typeof(FieldRightDetailDto))]
        [InlineData(typeof(ProfileDto))]
        [InlineData(typeof(ProfileListDto))]
        [InlineData(typeof(ProfileDetailDto))]
        [InlineData(typeof(ProfileRightDto))]
        [InlineData(typeof(ProfileRightListDto))]
        [InlineData(typeof(ProfileRightDetailDto))]
        [InlineData(typeof(ProfileRoleDto))]
        [InlineData(typeof(ProfileRoleListDto))]
        [InlineData(typeof(ProfileRoleDetailDto))]
        [InlineData(typeof(RightDto))]
        [InlineData(typeof(RightDetailDto))]
        [InlineData(typeof(RoleDto))]
        [InlineData(typeof(RoleDetailDto))]
        [InlineData(typeof(RoleRightDto))]
        [InlineData(typeof(RoleRightDetailDto))]
        [InlineData(typeof(RowLevelRightDto))]
        [InlineData(typeof(RowLevelRightListDto))]
        [InlineData(typeof(RowLevelRightDetailDto))]
        [InlineData(typeof(RowLevelSecurityObjectDto))]
        [InlineData(typeof(RowLevelSecurityObjectListDto))]
        [InlineData(typeof(RowLevelSecurityObjectDetailDto))]
        [InlineData(typeof(OperationRightDto))]
        [InlineData(typeof(OperationRightDetailDto))]
        [InlineData(typeof(OperationRightListDto))]
        [InlineData(typeof(RoleOperationRightDto))]
        [InlineData(typeof(RoleOperationRightDetailDto))]
        [InlineData(typeof(RoleOperationRightListDto))]
        [InlineData(typeof(ProfileOperationRightDto))]
        [InlineData(typeof(ProfileOperationRightDetailDto))]
        [InlineData(typeof(ProfileOperationRightListDto))]
        [InlineData(typeof(UserDefaultValueDto))]
        [InlineData(typeof(UserDefaultValueListDto))]
        [InlineData(typeof(UserDefaultValueDetailDto))]
        [InlineData(typeof(UserFullDto))]
        [InlineData(typeof(UserProfileDto))]
        [InlineData(typeof(UserProfileListDto))]
        [InlineData(typeof(UserProfileDetailDto))]
        [InlineData(typeof(UserLoginDto))]
        #endregion Security

        [InlineData(typeof(ScheduleSettingDto))]
        #region Notification
        [InlineData(typeof(NotificationDetailDto))]
        [InlineData(typeof(NotificationEditDto))]
        [InlineData(typeof(NotificationByAuthorListDto))]
        [InlineData(typeof(NotificationByReceiverListDto))]
        [InlineData(typeof(NotificationReceiverEditDto))]
        [InlineData(typeof(NotificationReceiverListDto))]
        #endregion
        public void CanReadDto(Type dtoType)
        {
            var wasRead = (bool)ReflectionHelper.InvokeGenericMethod(this, dtoType, nameof(CanReadDtoInternal), null);
            Assert.True(wasRead);
        }
        #endregion Tests

        #region PrivateMethods
        private bool CanReadDtoInternal<TDto>() where TDto : class
        {
            var dataService = IntegrationBase.CreateDataServiceInstance();
            //var context = IntegrationBase.CreateContextInstance();
            //var sqlText = (new PostgresQueryTextService(_queryConditionsBuilderMock.Object, _textServiceLoggerMock.Object)).GetParameterizedQueryString(typeof(TDto), null, false, null, null);
            //var query = context.Set<TDto>().FromSqlInterpolated(sqlText);
            //var query = context.Query<TDto>().FromSqlInterpolated(sqlText);

            // read dto
            dataService.GetDto<TDto>("", null, null, 0, 0, 0, null, null);

            return true;
        }
        #endregion PrivateMethods
    }
}
