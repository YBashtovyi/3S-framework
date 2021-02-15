using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Business.Services.ApplicationServices;
using App.Data.Dto.Administration;
using App.Data.Dto.Common;
using App.Data.Dto.Common.NotMapped;
using App.Data.Enums;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Common.Extensions;
using Core.Data.Common;
using Core.Data.Dto.Common;
using Core.Mvc.Helpers;
using Core.Services;
using Core.Services.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;

namespace App.Business.Services.ApiControllerServices
{
    public class NotificationService
    {
        #region properties: public
        public ICommonDataService DataService { get; }
        #endregion


        #region properties: private

        private readonly IStringLocalizer<NotificationService> _localizer;
        private readonly IOneSignalService _oneSignalService;
        private readonly IUserInfoService _userInfoService;
        private readonly IConfiguration _configuration;
        private readonly DefaultValuesService _defaultValuesService;
        #endregion


        #region constructor
        public NotificationService(ICommonDataService dataService,
            IStringLocalizer<NotificationService> localizer,
            IOneSignalService oneSignalService,
            IUserInfoService userInfoService,
            IConfiguration configuration,
            DefaultValuesService defaultValuesService)
        {
            DataService = dataService;
            _localizer = localizer;
            _oneSignalService = oneSignalService;
            _userInfoService = userInfoService;
            _configuration = configuration;
            _defaultValuesService = defaultValuesService;
        }
        #endregion


        #region methods: public        

        /// <summary>
        /// Gets main data and related entities for create/edit mode 
        /// </summary>
        /// <param name="notificationId">Guid - object id in application, if exists <see cref="Notification"/></param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> GetNotificationEditDataAsync(Guid notififcationId)
        {
            var keyValuePairs = new Dictionary<string, object>();

            if (notififcationId != Guid.Empty)
            {
                var notification = await GetNotificationByIdAsync<NotificationEditDto>(notififcationId);
                keyValuePairs.Add(nameof(notification), notification);

                var receivers = await GetReceiversByNotificationIdAsync<NotificationReceiverListDto>(notififcationId);
                keyValuePairs.Add(nameof(receivers), receivers);
            }


            var notificationTypes = await DataService.GetDtoAsync<EnumRecordDto>(x => x.Group == nameof(EnumType.NotificationType));
            var currentUser = await _userInfoService.GetCurrentUserInfoAsync();
            var adminProfileId = _configuration.GetValue<Guid>("Identifiers:AdminProfileId");

            //if (currentUser.ProfileId != adminProfileId)
            //{
            //    notificationTypes = notificationTypes.Where(x => x.ItemNumber != (int)NotificationType.System);
            //}

            keyValuePairs.Add(nameof(notificationTypes), notificationTypes);

            return keyValuePairs;
        }


        /// <summary>
        /// Get all notifications from applications by author
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NotificationByAuthorListDto>> GetNotificationByAuthorListAsync(IDictionary<string, string> paramList)
        {
            var (pageSize, pageNumber, orderBy, otherParameters) = HttpQueryStringHelper.GetQueryParametersFromQueryParamList(paramList);
            return await DataService.GetDtoAsync<NotificationByAuthorListDto>(orderBy, null, otherParameters, (pageNumber - 1) * pageSize, pageSize, 0, null, null);
        }


        /// <summary>
        /// Get all notifications from applications by receiver
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NotificationByReceiverListDto>> GetNotificationByReceiverListAsync(IDictionary<string, string> paramList)
        {
            var (pageSize, pageNumber, orderBy, otherParameters) = HttpQueryStringHelper.GetQueryParametersFromQueryParamList(paramList);
            var currentUser = await _userInfoService.GetCurrentUserInfoAsync();

            // Notifications in which employee is the receiver or empty (null - its means, that notification is system)
            Expression<Func<NotificationByReceiverListDto, bool>> predicate = x => !x.ReceiverId.HasValue || x.ReceiverId == currentUser.Id;
            var notifications = await DataService.GetDtoAsync(orderBy, predicate, otherParameters, (pageNumber - 1) * pageSize, pageSize, 0, null, null);

            return notifications;
        }


        /// <summary>
        /// Get notification details
        /// </summary>
        /// <param name="id">notification identifier <see cref="Notification"/></param>
        /// <returns></returns>
        public async Task<NotificationDetailDto> GetNotificationDetailsByIdAsync(Guid id)
        {
            var notification = await GetNotificationByIdAsync<NotificationDetailDto>(id);

            notification.Receivers = await DataService.GetDtoAsync<NotificationReceiverListDto>(x => x.NotificationId == id);

            return notification;
        }


        /// <summary>
        /// Create notification in application
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> CreateNotificationAsync(NotificationEditDto notification)
        {
            var notificationNotTransferedState = await DataService.FirstOrDefaultAsync<EnumRecordDto>(x => x.Group == nameof(EnumType.NotificationState) &&
                x.ItemNumber == (int)NotificationState.NotTransfered);

            if (notificationNotTransferedState != null)
            {
                notification.StateId = notificationNotTransferedState.Id;
            }

            var currentOrganizationId = await _defaultValuesService.GetCurrentEmployeeOrganizationAsync();

            notification.OrganizationId = currentOrganizationId;
            notification.Id = DataService.AddDto<Notification>(notification, false);

            foreach (var receiver in notification.Receivers)
            {
                receiver.NotificationId = notification.Id;
                receiver.OrganizationId = currentOrganizationId;
                DataService.AddDto<NotificationReceiver>(receiver, false);
            }
            await DataService.SaveChangesAsync();

            return new Dictionary<string, string>();
        }


        /// <summary>
        /// Update notification in application
        /// </summary>
        /// <param name="id">notification identifier</param>
        /// <param name="notification">notification data</param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> UpdateNotificationAsync(Guid id, NotificationEditDto notification)
        {
            var notificationStates = await DataService.GetDtoAsync<EnumRecordDto>(x => x.Group == nameof(EnumType.NotificationState));

            var notificationSuccessfulState = notificationStates.FirstOrDefault(x => x.ItemNumber == (int)NotificationState.Successful);

            if (notificationSuccessfulState != null &&
                notification.StateId == notificationSuccessfulState.Id)
            {
                throw new AppException(_localizer["Notification in state Successful cannot be updated"], _localizer["Error while updating notification"]);
            }

            var notificationNotTransferedState = notificationStates.FirstOrDefault(x => x.ItemNumber == (int)NotificationState.NotTransfered);

            if (notificationNotTransferedState != null)
            {
                notification.StateId = notificationNotTransferedState.Id;
            }

            DataService.AddDto<Notification>(notification, true);

            //remove old receivers (hard)
            var receiversFromDb = await GetReceiversByNotificationIdAsync<NotificationReceiverEditDto>(id);
            var receiversFromDbIds = receiversFromDb.Select(x => x.Id);
            DataService.RemoveRange<NotificationReceiver>(receiversFromDbIds, false);

            //set actual receivers
            foreach (var receiver in notification.Receivers)
            {
                receiver.NotificationId = id;
                receiver.OrganizationId = notification.OrganizationId;
            }
            DataService.AddDtoRange<NotificationReceiver>(notification.Receivers, false);


            await DataService.SaveChangesAsync();
            return new Dictionary<string, string>();
        }


        /// <summary>
        /// Gets data about notification from OneSignal
        /// </summary>
        /// <param name="notificationId">Guid - object id in application, if exists <see cref="Notification"/></param>
        /// <returns></returns>
        public async Task<JObject> GetNotificationFromOneSignalByIdAsync(Guid notificationId)
        {
            var notification = await GetNotificationByIdAsync<NotificationEditDto>(notificationId);

            if (!notification.OneSignalId.HasValue)
            {
                throw new AppException();
            }

            var response = await _oneSignalService.GetNotificationByIdAsync(notification.OneSignalId.Value);

            return response;
        }


        /// <summary>
        /// Create notification in OneSignal
        /// </summary>
        /// <param name="notificationId">Guid - object id in application, if exists <see cref="Notification"/></param>
        /// <returns></returns>
        public async Task<NotificationCreateResponseDto> CreateNotificationInOneSignalAsync(Guid notificationId)
        {
            var notification = await GetNotificationByIdAsync<NotificationEditDto>(notificationId);
            notification.Receivers = await GetReceiversByNotificationIdAsync<NotificationReceiverEditDto>(notificationId);

            var receiverIds = notification.Receivers.Select(x => x.ReceiverId);
            //var employees = await DataService.GetDtoAsync<EmployeeListDto>(x => receiverIds.Contains(x.Id));
            //var personIds = employees.Select(x => x.PersonId);
            //var persons = await DataService.GetDtoAsync<PersonListDto>(x => personIds.Contains(x.Id));

            var users = new List<Guid>();

            //foreach (var item in persons)
            //{
            //    if (Guid.TryParse(item.UserId, out var userId))
            //    {
            //        if (!users.Contains(userId))
            //        {
            //            users.Add(userId);
            //        }
            //    }
            //}

            //if (notification.Receivers.Any() && !users.Any())
            //{
            //    throw new AppException(_localizer["Receivers have not user id on identity server"], _localizer["Error while sending notification"]);
            //}

            try
            {
                var oneSignalResponse = await CreateInOneSignalAsync(notification, users);
                await ProcessResponseAfterCreateInOneSignal(notification, oneSignalResponse);
                return oneSignalResponse;
            }
            catch (AppException ex)
            {
                await ProcessResponseIfCreateFailedInOneSignal(notification, ex.Message);
                throw new AppException(ex.Message, _localizer["Error while sending notification"], ex);
            }
        }

        #endregion


        #region methods: private

        private async Task<T> GetNotificationByIdAsync<T>(Guid notificationId) where T : BaseNotificationDto
        {
            var notification = await DataService.FirstOrDefaultAsync<T>(x => x.Id == notificationId, _userInfoService.SystemUser);

            if (notification == null)
            {
                throw new AppException(_localizer["Notification does not exist in the application"]);
            }

            return notification;
        }


        private async Task<IEnumerable<T>> GetReceiversByNotificationIdAsync<T>(Guid notificationId) where T : BaseNotificationReceiverDto
        {
            return await DataService.GetDtoAsync<T>(x => x.NotificationId == notificationId);
        }


        private async Task<NotificationCreateResponseDto> CreateInOneSignalAsync(NotificationEditDto notification, IEnumerable<Guid> users)
        {
            var notificationUrl = string.IsNullOrEmpty(notification.Url) ? 
                $"{_configuration.GetValue<string>("OneSignal:DefaultNotificationUrl")}{notification.Id}"
                : notification.Url;

            NotificationCreateResponseDto response;

            if (!notification.ParticularTime.HasValue)
            {
                response = await _oneSignalService.CreateNotificationAsync(notification.Title,
                    notification.Message, notificationUrl, users);
            }
            else
            {
                response = await _oneSignalService.CreateNotificationAsync(notification.Title,
                    notification.Message, notificationUrl, notification.ParticularTime.Value, users);
            }

            return response;
        }


        private async Task ProcessResponseIfCreateFailedInOneSignal(NotificationEditDto notification, string error)
        {
            var notificationFailedState = await DataService.FirstOrDefaultAsync<EnumRecordDto>(x => x.Group == nameof(EnumType.NotificationState) &&
                x.ItemNumber == (int)NotificationState.Failed);

            if (notificationFailedState != null)
            {
                notification.StateId = notificationFailedState.Id;
            }

            notification.Error = error;
            DataService.AddDto<Notification>(notification, true);
            await DataService.SaveChangesAsync();
        }


        private async Task ProcessResponseAfterCreateInOneSignal(NotificationEditDto notification, NotificationCreateResponseDto oneSignalResponse)
        {
            if (oneSignalResponse != null)
            {
                notification.OneSignalId = oneSignalResponse.Id;
                var notificationTransferedStates = await DataService.GetDtoAsync<EnumRecordDto>(x => x.Group == nameof(EnumType.NotificationState));

                if (!oneSignalResponse.Id.HasValue || oneSignalResponse.Errors?.Any() == true)
                {
                    var erroredState = notificationTransferedStates.FirstOrDefault(x => x.ItemNumber == (int)NotificationState.Errored);

                    if (erroredState != null)
                    {
                        notification.StateId = erroredState.Id;
                    }

                    var localizedErrors = new List<string>();

                    foreach (var error in oneSignalResponse.Errors)
                    {
                        localizedErrors.Add(_localizer[error]);
                    }

                    notification.Error = string.Join(". ", localizedErrors);

                    if (notification.Error.Length > 1000)
                    {
                        notification.Error = notification.Error.Substring(0, 1000);
                    }

                }
                else
                {
                    var successfulState = notificationTransferedStates.FirstOrDefault(x => x.ItemNumber == (int)NotificationState.Successful);

                    if (successfulState != null)
                    {
                        notification.StateId = successfulState.Id;
                    }
                    notification.CreateInOneSignalDate = DateTime.UtcNow;
                    notification.Error = string.Empty;
                }

                DataService.AddDto<Notification>(notification, true);
            }

            await DataService.SaveChangesAsync();
        }
        #endregion
    }
}
