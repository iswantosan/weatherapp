using System.Collections.Generic;
using System.Linq;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class CityService : ICityService
    {
        public List<City> Get(int countryId)
        {
            List<City> cities = new List<City>()
            {
                new City()
                {
                    Id = 1,
                    CountryId = 1,
                    Name = "Singapore"
                },
                new City{
                    Id = 2,
                    CountryId = 2,
                    Name = "Jakarta"
                },
                new City{
                     Id = 3,
                    CountryId = 2,
                    Name = "Bandung"
                },
                 new City{
                     Id = 4,
                    CountryId = 2,
                    Name = "Denpasar"
                },
                  new City{
                     Id = 5,
                    CountryId = 3,
                    Name = "Bangkok"
                },
                   new City{
                     Id = 6,
                    CountryId = 3,
                    Name = "Pattaya"
                }
            };

            return cities.Where(s => s.CountryId == countryId).OrderBy(s => s.Name).ToList();
        }
    }
}
