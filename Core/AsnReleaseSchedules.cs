namespace Core
{
    public class AsnReleaseSchedules
    { 
        public string AccountCode { get; set; } 
        public int StateId { get; set; }
        public TimeSpan StartTime { get; set; }
        public int RunMinutes { get; set; }
    }
}
