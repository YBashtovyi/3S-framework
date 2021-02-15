using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace App.Business.IdentityServer
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public long ExpiresIn { get; set; }
    }

    public class ClientCredentialsManager
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ClientCredentialsManager> _logger;
        private Timer timer;
        private TokenModel tokenModel;
        private const string CLIENT_NAME = "SUIPApi";
        private const string SCOPES = "MisApiScope IdentityApiScope openid offline_access";
        private const string CLIENT_SECRET = "{!Qwerty_1}";

        public ClientCredentialsManager(IConfiguration configuration, ILogger<ClientCredentialsManager> logger)
        {
            this._configuration = configuration;
            _logger = logger;
            //timer = new Timer(GetLocalTokenAsync, null, Token.ExpiresIn, Timeout.Infinite);
        }

        public TokenModel Token
        {
            get
            {
                if (tokenModel == null) tokenModel = new TokenModel {ExpiresIn = 0};
                return tokenModel;
            }
        }

        public void GenerateNewToken()
        {
            //timer = new Timer(GetLocalTokenAsync, null, Token.ExpiresIn, Timeout.Infinite);
        }

        private async void GetLocalTokenAsync(Object obj)
        {
            try
            {
                var client = new HttpClient();

                var discoveryClient = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("Identity:Authority").TrimEnd('/'));
                if (discoveryClient.IsError) throw new Exception(discoveryClient.Error);


                var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _configuration.GetValue<string>("Identity:Authority").TrimEnd('/'),
                    ClientId = CLIENT_NAME,
                    ClientSecret = CLIENT_SECRET,
                    AuthorizationHeaderStyle = BasicAuthenticationHeaderStyle.Rfc2617,
                    Scope = SCOPES,
                });

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    tokenModel.AccessToken = response.AccessToken;
                    tokenModel.TokenType = response.TokenType;
                    tokenModel.ExpiresIn = response.ExpiresIn;

                    timer.Change((int) TimeSpan.FromSeconds(Token.ExpiresIn - 10).TotalMilliseconds, Timeout.Infinite);
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Warning, "Cannot get token from LocalIdentityServer (Will check every 10 second) ",
                    ex.Message);
                timer.Change((int) TimeSpan.FromSeconds(10).TotalMilliseconds, Timeout.Infinite);
            }
        }
    }
}
