using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.DTO
{
    public class WeatherDTO
    {
        public WeatherMainDTO Main { get; set; }

        public WeatherDataDTO[] Weather { get; set; }

        public WeatherCoordsDTO Coord { get; set; }

        public WeatherWindDTO Wind { get; set; }
    }

    public class WeatherMainDTO
    {
        public float Temp { get; set; }

        public float pressure { get; set; }


    }

    public class WeatherDataDTO
    {
        public string Main { get; set; }
    }

    public class WeatherCoordsDTO
    {
        public float Lat { get; set; }

        public float Lon { get; set; }

    }

    public class WeatherWindDTO
    {
        public float speed { get; set; }

        public float deg { get; set; }

        public float gust { get; set; }
    }
    
}
