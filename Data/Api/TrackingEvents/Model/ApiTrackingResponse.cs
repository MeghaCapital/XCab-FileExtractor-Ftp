using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Model
{
    public class ApiTrackingResponse
    {
        public string DriverNumber { get; set; }
        public string Reference { get; set; }
        public string AccountCode { get; set; }
        public string JobNumber { get; set; }
        public ApiErrorType? ErrorType { get; set; }
        public ApiTrackingStatus TrackingStatus { get; set; }
    }
}
