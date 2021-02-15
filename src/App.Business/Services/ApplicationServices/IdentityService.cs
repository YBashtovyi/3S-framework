using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Business.IdentityServer;
using Microsoft.Extensions.Configuration;

namespace App.Business.Services.ApplicationServices
{
    public class IdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityHttpClient _identityHttpClient;

        public IdentityService(IConfiguration configuration, IIdentityHttpClient identityHttpClient)
        {
            _configuration = configuration;
            _identityHttpClient = identityHttpClient;
        }

        public async Task GetAccounts()
        {
            var result = await _identityHttpClient.GetResponseAsync("user");
            if (result.IsSuccessStatusCode)
            {
                var jsonResult = await result.Content.ReadAsStringAsync();
            }
        }
    }
}
