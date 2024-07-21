namespace Data.Model.Address
{
    public class Suburb
    {
        /// <summary>
        ///    Suburb name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///    Postcode  of the Suburb
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        ///    State of the Suburb
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///    Latitude of the Suburb
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        ///    Longitude of the Suburb
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        ///    Is it a metro suburs
        /// </summary>
        public string IsMetro { get; set; }
    }
}
