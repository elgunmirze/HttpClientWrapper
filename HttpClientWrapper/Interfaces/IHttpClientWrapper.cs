using System.Threading.Tasks;

namespace HttpClientWrapper.Interfaces
{
    public interface IHttpClientWrapper
    {
        Task<T> GetAsync<T>(string apiUri);

        Task<T> PostAsync<T>(string apiUri, object variable);

        Task<T> PutAsync<T>(string apiUri, object variable);

        Task<T> DeleteAsync<T>(string apiUri);

        Task<T> GetAsyncByHttpRequest<T>(string apiUrl);

        Task<T> PostAsyncByHttpRequest<T>(string apiUri, object variable);
    }
}