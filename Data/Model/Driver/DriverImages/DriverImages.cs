using System;

namespace Data.Model.Driver.DriverImages
{
    public class PodModel
    {
        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public string DriverNumber { get; set; }
        // single character - S-Sydney M-Melbourne etc
        public string DomicileState { get; set; }
        // default to S-signature and created for future proofing
        public string ImageType { get; set; } = "S";
        // who signed for the delivery/pickup
        public string PodName { get; set; }
        // the account code for the job being received
        public string AccountCode { get; set; }
        // job Date 
        public DateTime PODDate { get; set; }
        // image content
        public string Base64Image { get; set; }
    }
}
