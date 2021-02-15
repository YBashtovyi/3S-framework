using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace App.Api.Controllers
{
    public class DigitalSignatureHandlerMiddleware
    {
        private readonly string _httpRequestParameterAddress = "address";
        private readonly string _httpContentTypeBase64 = "X-user/base64-data";
        private readonly int _httpMaxContentSize = 10000000;
        private readonly int _httpBufferChunk = 0xFFFF;

        private readonly bool _useProxy = false;
        private readonly string _proxyAddress = "";
        private readonly int _proxyPort = 3128;
        private readonly string _proxyUser = "";
        private readonly string _proxyPassword = "";

        private readonly string[] _knownHosts = {
            "czo.gov.ua",
            "acskidd.gov.ua",
            "ca.informjust.ua",
            "csk.uz.gov.ua",
            "masterkey.ua",
            "ocsp.masterkey.ua",
            "tsp.masterkey.ua",
            "ca.ksystems.com.ua",
            "csk.uss.gov.ua",
            "csk.ukrsibbank.com",
            "acsk.privatbank.ua",
            "ca.mil.gov.ua",
            "acsk.dpsu.gov.ua",
            "acsk.er.gov.ua",
            "ca.mvs.gov.ua",
            "canbu.bank.gov.ua",
            "uakey.com.ua",
            "altersign.com.ua",
            "ca.altersign.com.ua",
            "ocsp.altersign.com.ua",
            "acsk.uipv.org",
            "ocsp.acsk.uipv.org",
            "acsk.treasury.gov.ua",
            "ocsp.treasury.gov.ua",
            "ca.oschadbank.ua",
            "ca.gp.gov.ua"
        };

        private bool IsKnownHost(string uriValue)
        {
            if (!string.IsNullOrEmpty(uriValue))
            {
                if (!uriValue.Contains("://"))
                {
                    uriValue = "http://" + uriValue;
                }
                var uri = new Uri(uriValue);
                var host = uri.Host;
                if (host == null || host == "")
                {
                    host = uriValue;
                }
                foreach (var knownHost in _knownHosts)
                {
                    if (knownHost == host)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private async Task<byte[]> SafeReadDataStreamAsync(Stream stream)
        {
            var buffer = new byte[_httpBufferChunk];
            using (var memoryStream = new MemoryStream())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    int count;
                    while ((count = await streamReader.BaseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, count);
                        if (memoryStream.Length > _httpMaxContentSize)
                        {
                            return null;
                        }
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        private HttpClientHandler GetHttpClientHandler()
        {
            var proxyUri = string.Format("{0}:{1}", _proxyAddress, _proxyPort);
            var proxy = new WebProxy(proxyUri, false)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_proxyUser, _proxyPassword)
            };
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.Proxy = proxy;
            return httpClientHandler;
        }
        private async Task<HttpStatusCode> HandleRequest(HttpContext context)
        {
            string requestAddress = context.Request.Query[_httpRequestParameterAddress];
            if (requestAddress == null || requestAddress == "" ||
                !IsKnownHost(requestAddress))
            {
                return HttpStatusCode.BadRequest;
            }
            using (var client = _useProxy ? new HttpClient(GetHttpClientHandler()) : new HttpClient())
            {
                HttpResponseMessage reponse;
                if (context.Request.Method == "POST")
                {
                    byte[] serverRequestData;
                    if (!context.Request.ContentType.Contains(_httpContentTypeBase64))
                    {
                        return HttpStatusCode.BadRequest;
                    }
                    var requestData = await SafeReadDataStreamAsync(context.Request.Body);
                    if (requestData == null)
                    {
                        return HttpStatusCode.RequestEntityTooLarge;
                    }
                    var requestDataBase64String = Encoding.UTF8.GetString(requestData);
                    serverRequestData = Convert.FromBase64String(requestDataBase64String);
                    var content = new ByteArrayContent(serverRequestData);
                    reponse = await client.PostAsync(requestAddress, content);
                }
                else
                {
                    reponse = await client.GetAsync(requestAddress);
                }
                var clientResponseData = await reponse.Content.ReadAsByteArrayAsync();
                context.Response.ContentType = _httpContentTypeBase64;
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync(Convert.ToBase64String(clientResponseData));
                return HttpStatusCode.OK;
            }
        }

        public DigitalSignatureHandlerMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext context)
        {
            HttpStatusCode status;
            try
            {
                var requestType = context.Request.Method;
                if (requestType == "GET" || requestType == "POST")
                {
                    status = await HandleRequest(context);
                }
                else
                {
                    status = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception e)
            {
                await context.Response.WriteAsync("Виникла помилка при обробці запиту" + e);
                return;
            }
            if (status != HttpStatusCode.OK)
            {
                await context.Response.WriteAsync("Виникла помилка при обробці запиту");
                context.Response.StatusCode = (int)status;
            }
        }
    }
    public static class DigitalSignatureHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseDigitalSignatureHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DigitalSignatureHandlerMiddleware>();
        }
    }

    public class DigitalSignatureHandlerMiddlewarePipeline
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseDigitalSignatureHandlerMiddleware();
        }
    }
}
