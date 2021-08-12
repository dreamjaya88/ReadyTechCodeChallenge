using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BrewCoffeeTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;
        public HttpRequestMessage Request { get; set; }

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.Request = request;
            return await Task.Run(() => _response, cancellationToken);
        }
    }
}
