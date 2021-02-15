using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Base.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Business.Services.ApplicationServices
{
    public class OneSignalHttpClient: IOneSignalHttpClient
    {
        #region fields: private
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<SharedOneSignalResource> _oneSignalLocalizer;
        #endregion

        #region construtor
        public OneSignalHttpClient(HttpClient httpClient,
            IConfiguration configuration,
            IStringLocalizer<SharedOneSignalResource> oneSignalLocalizer)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _oneSignalLocalizer = oneSignalLocalizer;
        }
        #endregion

        #region methods: public

        ///  <inheritdoc />
        public async Task<TResponse> GetAsync<TResponse>(string requestUrl, Dictionary<string, string> headers = null) where TResponse : class
        {
            SetHttpClientHeaders(headers);

            var fullRequestUrl = GetFullRequestUrl(requestUrl);

            var response = await _httpClient.GetAsync(fullRequestUrl);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                HandleAppException(result);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<TResponse>(result);
                }
            }
            return null;
        }

        ///  <inheritdoc />
        public async Task<TResponse> PostAsync<TResponse, TRequest>(string requestUrl, TRequest content, Dictionary<string, string> headers = null)
            where TResponse : class
            where TRequest : class
        {
            SetHttpClientHeaders(headers);

            var fullRequestUrl = GetFullRequestUrl(requestUrl);

            var contentBytes = GetByteArrayContent(content);
            var response = await _httpClient.PostAsync(fullRequestUrl, contentBytes);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                HandleAppException(result);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<TResponse>(result);
                }
            }

            return null;
        }
        #endregion

        #region methods: private

        private void HandleAppException(string result)
        {
            var localizedErrors = new List<string>();
            var separator = " ";

            var jObject = JObject.Parse(result);

            if (jObject["errors"] != null)
            {
                var errors = jObject["errors"].ToObject<JArray>();

                foreach (var error in errors)
                {
                    localizedErrors.Add(_oneSignalLocalizer[error.ToString()]);
                }
            }

            var errorMessage = string.Join(separator, localizedErrors);

            throw new AppException(errorMessage);
        }


        private void SetHttpClientHeaders(Dictionary<string, string> headers)
        {
            var apiKey = _configuration.GetValue<string>("OneSignal:ApiKey");

            var authorizationKey = "Authorization";

            if (!_httpClient.DefaultRequestHeaders.Contains(authorizationKey))
            {
                _httpClient.DefaultRequestHeaders.Add(authorizationKey, $"Basic {apiKey}");
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!_httpClient.DefaultRequestHeaders.Contains(header.Key))
                    {
                        _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
        }

        private string GetFullRequestUrl(string requestUrl)
        {
            var apiUri = _configuration.GetValue<string>("OneSignal:ApiUri");
            return $"{apiUri}{requestUrl}";
        }

        private ByteArrayContent GetByteArrayContent<TRequest>(TRequest content) where TRequest : class
        {
            var myContent = JsonConvert.SerializeObject(content);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }
        #endregion
    }
}
