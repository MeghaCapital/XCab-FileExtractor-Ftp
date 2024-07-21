using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Model
{
    public class ApiTrackingRequest
    {
        public string AccountCode { get; set; }        
        public ICollection<string> References { get; set; }
        public EUniqueReferenceType? ReferenceType { get; set; }
        public EStates State { get; set; }
    }
}
