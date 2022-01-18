using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class WeatherCalculator
    {
        public void _weatherCalculator(float perlin, out float temperature, out int weatherType, out float windSpeed)
        {
            temperature = (perlin * 40) - 40;
            weatherType = 2;
            windSpeed = 1;
            if (perlin <= 0.3)
            {
                weatherType = 1;
                windSpeed = 1;
            }
            if (perlin > 0.3 && perlin <= .59)
            {
                weatherType = 2;
                windSpeed = 0.7f;

            }
            if (perlin > 0.6 && perlin <= 1)
            {
                weatherType = 3;
                windSpeed = 1;

            }
        }
    }
}
