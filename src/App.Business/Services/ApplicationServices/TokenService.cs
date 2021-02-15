using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Data.Dto.Administration.NotMapped;
using Core.Base.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace App.Business.Services.ApplicationServices
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TokenInfo> GetIdGovUaToken(string code)
        {
            var authUri = _configuration["IdGovUaProvider:Url"];
            var clientId = _configuration["IdGovUaProvider:ClientId"];
            var clientSecret = _configuration["IdGovUaProvider:ClientSecret"];

            var values = new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"client_id", clientId},
                {"client_secret", clientSecret},
                {"code", code},
            };

            var content = new FormUrlEncodedContent(values);

            using var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri(authUri);

                client.DefaultRequestHeaders.Clear();

                var response = await client.PostAsync($"{authUri}/get-access-token", content);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(resp);
                    return tokenInfo;
                }
            }
            catch (OperationCanceledException ex)
            {
                throw new AppException($"Error getting token from id.gov.ua {ex.Message}");
            }

            return null;
        }

        public async Task<IdGovUaUserInfo> GetIdGovUaUserInfo(TokenInfo tokenModel)
        {
            var authUri = _configuration["IdGovUaProvider:Url"];
            var values = new Dictionary<string, string>
            {
                {"access_token", tokenModel.access_token},
                {"user_id", tokenModel.user_id}
            };
            var content = new FormUrlEncodedContent(values);

            using var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri(authUri);
                client.DefaultRequestHeaders.Clear();
                var response = await client.PostAsync($"{authUri}/get-user-info", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    var test = JsonConvert.DeserializeObject<IdGovUaUserInfo>(resp);

                    return test;
                }
            }
            catch (OperationCanceledException ex)
            {
                throw new AppException($"Error getting user info from id.gov.ua {ex.Message}");
            }

            return null;
        }
    }
}
