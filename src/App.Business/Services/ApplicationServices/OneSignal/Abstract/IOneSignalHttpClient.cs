using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Business.Services.ApplicationServices
{
    /// <summary>
    /// Definition of methods of OneSignal http client
    /// </summary>
    public interface IOneSignalHttpClient
    {
        /// <summary>
        /// Get data from server using httpClient
        /// </summary>
        /// <typeparam name="TResponse">response data</typeparam>
        /// <param name="requestUri">request uri</param>
        /// <param name="headers">httpclient headers</param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string requestUri, Dictionary<string, string> headers = null) 
            where TResponse : class;

        /// <summary>
        /// Post data on server using httpClient
        /// </summary>
        /// <typeparam name="TResponse">response data type</typeparam>
        /// <typeparam name="TRequest">request data type</typeparam>
        /// <param name="requestUrl">request url</param>
        /// <param name="content">data</param>
        /// <param name="headers">httpclient headers</param>
        /// <returns></returns>
        Task<TResponse> PostAsync<TResponse, TRequest>(string requestUrl, TRequest content, Dictionary<string, string> headers = null)
            where TRequest : class
            where TResponse : class;
    }
}
