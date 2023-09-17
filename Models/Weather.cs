using System;
using System.Collections.Generic;

namespace WeatherApp.Models
{
    public class WeatherResponse
    {
        public long Dt { get; set; }
        public DateTime Timestamp { get; set; }
        public List<WeatherData> Weather { get; set; }
        public WindData Wind { get; set; }
        public MainData Main { get; set; }
    }

    public class WeatherData
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double TempCelcius { get; set; }
        public double TempMinCelcius { get; set; }
        public double TempMaxCelcius { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
    }

    public class WindData
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }
}
