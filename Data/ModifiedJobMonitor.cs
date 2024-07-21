namespace Data
{
    public class ModifiedJobMonitor : Monitor
    {
        public string JobClientRef { get; set; }
        public string JobClientRef2 { get; set; }
        public string Caller { get; set; }
        public string EventDateTime { get; set; } //this has been added to show when a job was modified
        //public LoginDetails LoginDetails { get; set; }
    }
}
