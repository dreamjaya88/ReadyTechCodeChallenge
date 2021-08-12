using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BrewCoffee.Services.Helpers;
using BrewCoffee.Services.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BrewCoffee.Services.APIProxy
{
    public interface IWeatherProxy
    {
        Root GetWeather(string cityId);
    }

    public class WeatherProxy : IWeatherProxy
    {
        private readonly IOptions<ApiSettings> _settings;
        private readonly HttpClient _client;

        public WeatherProxy(IOptions<ApiSettings> settings, HttpClient client)
        {
            _settings = settings;
            _client = client;
        }

        public Root GetWeather(string cityId)
        {

            var response = InvokeSync(cityId).Result;
            if (response != null && response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var messsage = response.Content.ReadAsStringAsync().Result;
                return (Root)JsonConvert.DeserializeObject(messsage, typeof(Root));
            }

            return null;
        }

        private ApiInterface GetApiProxyDetails(string interfaceId)
        {
            return _settings.Value.ApiInterfaces.FirstOrDefault(i => i.Id == interfaceId);
        }

        private async Task<HttpResponseMessage> InvokeSync(string cityId)
        {
            var interfaceDetails = GetApiProxyDetails("weather");
            var route = $"?id={cityId}& appid={interfaceDetails.ApiKey}";
            var request = new HttpRequestMessage(HttpMethod.Get, $"{interfaceDetails.Url}{route}");
            return await _client.SendAsync(request).ConfigureAwait(false);
        }
    }
}
