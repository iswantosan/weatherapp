using System.Collections.Generic;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public interface ICountryService
    {
        public List<Country> Get();
    }
}
