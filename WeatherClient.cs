using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MicroserviceApi
{
    class WeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        // Use of Record Types in dotnet 5 Example
        //TODO: Lookup Record Types and what they are fore
        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] weather, Main main, long dt);

        // Call to get the current forecast
        public async Task<Forecast> GetCurrentForecastAsync(string city)
        {
            var forecast = await _httpClient.GetFromJsonAsync<Forecast>($"https://{_settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={_settings.ApiKey}&units=metric"); //TODO: Make notes on this method call
            return forecast;
        }


    }
}