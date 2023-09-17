using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Service;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IWeatherService _weatherService;

        public HomeController(ICountryService countryService, ICityService cityService, IWeatherService weatherService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _countryService = countryService;
            _cityService = cityService;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            var countries = _countryService.Get();

            ViewBag.Countries = new SelectList(countries.OrderBy(s => s.Name), "Id", "Name");
            ViewBag.Cities = new SelectList(new List<City>(), "Id", "Name");

            return View(new WeatherInput());
        }

        public IActionResult City(int id)
        {
            var cities = _cityService.Get(id);
            return new JsonResult(cities);
        }

        public async Task<IActionResult> WeatherAsync(string city)
        {
            var weather = await _weatherService.Get(city);
            return new JsonResult(new
            {
                status = weather != null,
                data = weather,
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
