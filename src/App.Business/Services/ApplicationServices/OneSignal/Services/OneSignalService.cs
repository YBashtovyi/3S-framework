using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Dto.Common.NotMapped;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace App.Business.Services.ApplicationServices
{
    public class OneSignalService: IOneSignalService
    {
        #region fields : private
        private readonly IOneSignalHttpClient _oneSignalHttpClient;
        private readonly IConfiguration _configuration;
        #endregion

        #region constructor
        public OneSignalService(IOneSignalHttpClient oneSignalHttpClient,
            IConfiguration configuration)
        {
            _oneSignalHttpClient = oneSignalHttpClient;
            _configuration = configuration;
        }
        #endregion

        ///  <inheritdoc />
        public async Task<JObject> GetNotificationByIdAsync(Guid id)
        {
            var applicationId = _configuration.GetValue<string>("OneSignal:AppId");
            var queryParameters = new Dictionary<string, string>
            {
                { "app_id", applicationId }
            };
            var requestUrl = $"/notifications/{id}{GetQueryParams(queryParameters)}";
            return await _oneSignalHttpClient.GetAsync<JObject>(requestUrl);
        }

        ///  <inheritdoc />
        public async Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, IEnumerable<Guid> users = null)
        {
            var applicationId = _configuration.GetValue<string>("OneSignal:AppId");

            var jObject = GetBaseNotificationBody(title, content, applicationId, users);

            var requestUrl = $"/notifications";
            var response = await _oneSignalHttpClient.PostAsync<NotificationCreateResponseDto, JObject>(requestUrl, jObject);

            return response;
        }

        ///  <inheritdoc />
        public async Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, string url, IEnumerable<Guid> users = null)
        {
            var applicationId = _configuration.GetValue<string>("OneSignal:AppId");

            var jObject = GetBaseNotificationBody(title, content, applicationId, users);
            jObject.Add("url", url);

            var requestUrl = $"/notifications";
            var response = await _oneSignalHttpClient.PostAsync<NotificationCreateResponseDto, JObject>(requestUrl, jObject);

            return response;
        }

        ///  <inheritdoc />
        public async Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, DateTime particularTime, IEnumerable<Guid> users = null)
        {
            var applicationId = _configuration.GetValue<string>("OneSignal:AppId");

            var jObject = GetBaseNotificationBody(title, content, applicationId, users);
            jObject.Add("send_after", particularTime);

            var requestUrl = $"/notifications";
            var response = await _oneSignalHttpClient.PostAsync<NotificationCreateResponseDto, JObject>(requestUrl, jObject);

            return response;
        }

        ///  <inheritdoc />
        public async Task<NotificationCreateResponseDto> CreateNotificationAsync(string title, string content, string url, DateTime particularTime, IEnumerable<Guid> users = null)
        {
            var applicationId = _configuration.GetValue<string>("OneSignal:AppId");

            var jObject = GetBaseNotificationBody(title, content, applicationId, users);
            jObject.Add("url", url);
            jObject.Add("send_after", particularTime);

            var requestUrl = $"/notifications";
            var response = await _oneSignalHttpClient.PostAsync<NotificationCreateResponseDto, JObject>(requestUrl, jObject);

            return response;
        }

        #region methods: private 

        private JObject GetBaseNotificationBody(
            string title,
            string content,
            string applicationId,
            IEnumerable<Guid> users)
        {
            var jObject = new JObject
            {
                { "app_id", applicationId },
                { "external_id", Guid.NewGuid() }
            };

            var heading = new JObject()
            {
                { "uk", title },
                { "en", title }
            };
            jObject.Add("headings", heading);

            var contents = new JObject()
            {
                { "uk", content },
                { "en", content }
            };
            jObject.Add("contents", contents);

            if (users?.Any() == true)
            {
                jObject.Add("include_external_user_ids", new JArray(users));
            }
            else
            {
                jObject.Add("included_segments", new JArray("Subscribed Users"));
            }

            return jObject;
        }

        protected string GetQueryParams(IDictionary<string, string> paramList)
        {
            var requestParams = "";
            if (paramList != null && paramList.Count > 0)
            {
                requestParams += "?";
                foreach (var param in paramList)
                {
                    requestParams += param.Key + "=" + param.Value + "&";
                }
                requestParams = requestParams.TrimEnd('&');
            }
            return requestParams;
        }
        #endregion
    }
}
