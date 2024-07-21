namespace Data.Model.PodStreamer
{
    public class ILogixJobDetails
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string JobNumber { get; set; } = string.Empty;
        public string SubJobNumber { get; set; } = string.Empty ;

    }
}
