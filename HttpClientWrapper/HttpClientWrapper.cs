using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClientWrapper.Exceptions;
using HttpClientWrapper.Interfaces;
using Newtonsoft.Json;

namespace HttpClientWrapper
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private HttpClient _client;

        public HttpClientWrapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("your service url")
            };

            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "subscription key");
            _client.DefaultRequestHeaders.Add("other headers", "other headers");
        }

        public async Task<T> GetAsync<T>(string apiUri)
        {
            HttpResponseMessage response = await _client.GetAsync(apiUri);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            string content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(content);

            return result;

        }

        public async Task<T> PostAsync<T>(string apiUri, object variable)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(apiUri, variable);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;

        }

        public async Task<T> PutAsync<T>(string apiUri, object variable)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(apiUri, variable);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;

        }

        public async Task<T> DeleteAsync<T>(string apiUri)
        {
            HttpResponseMessage response = await _client.DeleteAsync(apiUri);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;

        }

        public async Task<T> GetAsyncByHttpRequest<T>(string apiUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(apiUrl));

            HttpResponseMessage response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode) throw HttpClientExceptions.ThrowException(response);

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

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
    }
}