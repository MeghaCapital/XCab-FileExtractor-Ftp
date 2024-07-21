using System;

namespace Core
{
    public class ModifiedBooking : Booking
    {
        //public String OkToUpload { get; set; }
        public String Cancelled { get; set; }
        public String Completed { get; set; }
        //public String UploadedToTplus { get; set; }
        public String ActionImmediate { get; set; }
        //public String TPLUS_JobNumber { get; set; }
        public DateTime TPLUS_JobAllocationDate { get; set; }
        // public DateTime UploadDateTime { get; set; }   
        public ModifiedBooking()
        {
        }

    }
}
