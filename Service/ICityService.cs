using System.Collections.Generic;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public interface ICityService
    {
        public List<City> Get(int countryId);
    }
}
