using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.tracker.Data.Response
{
    public class TrackingImageResponse
    {
        public TrackingImageResponse()
        {
            Events = new List<ImageTrackingEvent>();
        }

        public List<ImageTrackingEvent> Events { get; set; }
    }

    public class ImageTrackingEvent
    {
        public ImageTrackingEvent()
        {
            Documents = new List<TrackingDocument>();
        }

        // Id of either subJob or subJobLeg
        public int WorkId { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }
        
        public List<TrackingDocument> Documents { get; set; }
    }

    public class TrackingDocument
    { 
        public byte[] Content { get; set; }

        public string Type { get; set; }
    }
}
