using System.Collections.Generic;

namespace BrewCoffee.Services.Helpers
{
    public class ApiSettings
    {
        public List<ApiInterface> ApiInterfaces { get; set; }
    }

    public class ApiInterface
    {
        public string Id { get; set; }
        public string ApiKey { get; set; }
        public string Url { get; set; }
    }
}
