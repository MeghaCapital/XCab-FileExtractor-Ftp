using System;

namespace Data.Model.Driver.DriverImages
{
    public class DriverPayload
    {

        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public string DomicileState { get; set; }
        public string AccountCode { get; set; }
        public DateTime PodDate { get; set; }
        public string ImageType { get; set; }
        public string PodName { get; set; }
        public string base64Image { get; set; }

    }
}
