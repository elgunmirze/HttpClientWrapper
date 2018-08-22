using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClientWrapper.Exceptions;
using HttpClientWrapper.Interfaces;
using Newtonsoft.Json;

namespace HttpClientWrapper
{
    /// <summary>
    /// HttpClient Wrapper class which is implementing main functionality
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private HttpClient _client;

        public HttpClientWrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Creating instance to HttpClient class and setting parameters
        /// </summary>
        private void Initialize()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("your service url")
            };

            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "subscription key");
            _client.DefaultRequestHeaders.Add("other headers", "other headers");
        }

        /// <summary>
        /// Generic GetAsync method which is getting values in json and deserialized into the particular type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string apiUri)
        {
            HttpResponseMessage response = await _client.GetAsync(apiUri);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            string content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }

        /// <summary>
        /// Generic PostAsync method which is accepting 'variable' as parameter and sending to the service. We used here object type instead of any particular types.
        /// You can get response in json and deserialized into the particular type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string apiUri, object variable)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(apiUri, variable);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        /// <summary>
        /// Generic PutAsync method which is accepting 'variable' as parameter and sending to the service. We used here object type instead of any particular type.
        /// You can get response in json and deserialized into the particular type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string apiUri, object variable)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(apiUri, variable);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        /// <summary>
        /// Generic DeleteAsync which is deleting any record from the service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync<T>(string apiUri)
        {
            HttpResponseMessage response = await _client.DeleteAsync(apiUri);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        /// <summary>
        /// Generic GetAsync method which is getting values by http request in json and deserialized into the particular type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        public async Task<T> GetAsyncByHttpRequest<T>(string apiUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(apiUri));

            HttpResponseMessage response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        /// <summary>
        /// Generic PostAsync method which is accepting 'variable' as parameter and sending to the service by http request. We used here object type instead of any particular types.
        /// You can get response in json and deserialized into the particular type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public async Task<T> PostAsyncByHttpRequest<T>(string apiUri, object variable)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(apiUri))
            {
                Content = new StringContent(JsonConvert.SerializeObject(variable), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        /// <summary>
        /// Generic PutAsync method which is accepting 'variable' as parameter and sending to the service by http request. We used here object type instead of any particular type.
        /// You can get response in json and deserialized into the particular type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public async Task<T> PutAsyncByHttpRequest<T>(string apiUri, object variable)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, new Uri(apiUri))
            {
                Content = new StringContent(JsonConvert.SerializeObject(variable), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        /// <summary>
        /// Generic DeleteAsync which is deleting any record from the service by http request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsyncByHttpRequest<T>(string apiUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(apiUri));

            HttpResponseMessage response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }
    }
}