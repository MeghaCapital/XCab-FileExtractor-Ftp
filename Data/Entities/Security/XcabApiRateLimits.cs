namespace Data.Entities.Security
{
    public class XcabApiRateLimits
    {
        public string Apikey { get; set; }
        public int PerSec { get; set; }
        public int PerMin { get; set; }
        public int PerHour { get; set; }
        public int PerDay { get; set; }
        public int PerWeek { get; set; }
        public bool StandardLimits { get; set; }
    }
}
