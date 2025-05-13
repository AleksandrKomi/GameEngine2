using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.DTO
{
    public class LocationDTO
    {
        public FeaturesDTO[] Features { get; set; }
    }

    public class FeaturesDTO
    {
        public PropertiesDTO Properties { get; set; }
    }

    public class PropertiesDTO
    {
        public string Name { get; set; }

        public CoordinatesDTO Coordinates { get; set; }
    }

    public class CoordinatesDTO
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }
    }
}
