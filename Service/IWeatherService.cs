using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public interface IWeatherService
    {
        public double ConvertToCelcius(double fahrenheit);
        public Task<WeatherResponse> Get(string city);
    }
}
