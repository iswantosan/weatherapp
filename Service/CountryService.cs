using System.Collections.Generic;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class CountryService : ICountryService
    {
        public List<Country> Get()
        {
            List<Country> countries = new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    Name = "Singapore"
                },
                new Country{
                    Id = 2,
                    Name = "Indonesia"
                },
                new Country{
                    Id = 3,
                    Name = "Thailand"
                }
            };

            return countries;
        }
    }
}
