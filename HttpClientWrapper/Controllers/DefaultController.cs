using System.Web.Http;
using HttpClientWrapper.Interfaces;

namespace HttpClientWrapper.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IHttpClientWrapper _clientWrapper;
        public DefaultController(IHttpClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }
    }
}