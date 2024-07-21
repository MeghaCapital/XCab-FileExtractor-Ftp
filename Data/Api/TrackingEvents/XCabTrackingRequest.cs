using Core;
using Data.Api.TrackingEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents
{
    public class XCabTrackingRequest
    {
        public string AccountCode { get; set; }
        public ICollection<string> UniqueReferenceValue { get; set; }
        public EUniqueReferenceType UniqueReferenceType { get; set; }
        public EStates State { get; set; }
    }
}
