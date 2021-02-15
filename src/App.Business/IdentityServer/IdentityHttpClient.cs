using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace App.Business.IdentityServer
{
    public interface IIdentityHttpClient
    {
        Task<HttpResponseMessage> GetResponseAsync(string request);
        Task<HttpResponseMessage> PostResponseAsync(string request, object value);
    }

    public class IdentityHttpClient : IIdentityHttpClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IdentityHttpClient> _logger;
        private readonly ClientCredentialsManager _clientCredentialsManager;

        private HttpClient client;

        public IdentityHttpClient(IConfiguration configuration, ILogger<IdentityHttpClient> logger, ClientCredentialsManager clientCredentialsManager)
        {
            this._configuration = configuration;
            _logger = logger;
            _clientCredentialsManager = clientCredentialsManager;
        }

        private HttpClient GetHttpClient()
        {
            client ??= new HttpClient { BaseAddress = new Uri(_configuration["Authority"]) };
            AddTokenToClient();
            return client;
        }

        private void AddTokenToClient()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _clientCredentialsManager.Token.AccessToken);
        }

        public async Task<HttpResponseMessage> GetResponseAsync(string request)
        {
            try
            {
                var client = GetHttpClient();
                var response = await client.GetAsync(request);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _clientCredentialsManager.GenerateNewToken();
                    AddTokenToClient();
                    response = await client.GetAsync(request);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error ,$"Error during try to create new token for get response. {ex.Message}");
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> PostResponseAsync(string request, object content)
        {
            try
            {
                var client = GetHttpClient();
                var response = await client.PostAsync(request, GetHttpContent(content));
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _clientCredentialsManager.GenerateNewToken();
                    AddTokenToClient();
                    response = await client.PostAsync(request, GetHttpContent(content));
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.Log( LogLevel.Error,$"Error during try to create new token for get response. {ex.Message}");
                throw ex;
            }
        }

        private HttpContent GetHttpContent(object content)
        {
            var myContent = JsonConvert.SerializeObject(content);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }
    }
}
