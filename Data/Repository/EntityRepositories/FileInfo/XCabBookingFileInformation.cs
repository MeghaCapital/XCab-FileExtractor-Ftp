using System;

namespace Data.Repository.EntityRepositories.FileInfo
{
    public class XCabBookingFileInformation
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public int StateId { get; set; }
        public int BookingId { get; set; }
        public string FileName { get; set; }
        public string RouteNameFromFile { get; set; }
        public string StoreNameFromFile { get; set; }
        public DateTime FileDateTime { get; set; }
        public string JobType { get; set; }
    }
}
