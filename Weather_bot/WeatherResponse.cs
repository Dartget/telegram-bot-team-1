using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherBot
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }

        public string Name { get; set; }

        public Window Wind {get;set;}

        public int DT{get;set;}

    }
}
