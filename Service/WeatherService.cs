using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Configurations;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OpenWeatherMapConfiguration _openWeatherMapConfiguration;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherMapConfiguration> openWeatherMapConfiguration, ILogger<WeatherService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _openWeatherMapConfiguration = openWeatherMapConfiguration.Value;
        }

        public double ConvertToCelcius(double fahrenheit)
        {
            return Math.Round((fahrenheit - 32) * 5 / 9, 2);
        }

        public async Task<WeatherResponse> Get(string city)
        {
            var httpClient = _httpClientFactory.CreateClient("OpenWeatherApiClient");

            var response = await httpClient.GetAsync($"data/2.5/weather?q={city}&units=imperial&units=imperial&appid={_openWeatherMapConfiguration.Key}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    var weather = JsonConvert.DeserializeObject<WeatherResponse>(content);
                    weather.Timestamp = new DateTime(weather.Dt);

                    weather.Main.TempCelcius = ConvertToCelcius(weather.Main.Temp);
                    weather.Main.TempMaxCelcius = ConvertToCelcius(weather.Main.TempMax);
                    weather.Main.TempMinCelcius = ConvertToCelcius(weather.Main.TempMin);

                    return weather;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                return null;
            }
            else
            {
                _logger.LogError("Fail to get weather data");
                return null;
            }

        }
    }
}
