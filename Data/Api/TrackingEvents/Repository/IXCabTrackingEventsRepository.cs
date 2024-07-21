using Data.Api.TrackingEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Repository
{
    public interface IXCabTrackingEventsRepository
    {
        Task<XCabTrackingEvent> GetTrackingEventsForReference(DateTime fromDate, DateTime toDateTime, string accountCode, string reference);        
        bool UpdateXCabTrackingEvents(ICollection<XCabTrackingEvent> trackingEvents);   
    }
}
