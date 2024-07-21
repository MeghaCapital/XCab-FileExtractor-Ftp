using System;

namespace Data.Entities.ExternalClientIntegration
{
    public class GeocodedLocation
    {

        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public DateTime Modified { get; set; }
    }
}
