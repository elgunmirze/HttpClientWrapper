using System;
using System.Net.Http;
using System.Threading.Tasks;
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

            string content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }

        public async Task<T> PostAsync<T>(string apiUri, object variable)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(apiUri, variable);

            response.EnsureSuccessStatusCode();

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        public async Task<T> PutAsync<T>(string apiUri, object variable)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(apiUri, variable);

            response.EnsureSuccessStatusCode();

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }

        public async Task<T> DeleteAsync<T>(string apiUri)
        {
            HttpResponseMessage response = await _client.DeleteAsync(apiUri);

            response.EnsureSuccessStatusCode();

            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }
    }
}