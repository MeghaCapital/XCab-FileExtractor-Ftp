using Core;
using Data.Model.Tracking;
using LoginDetails = Core.LoginDetails;

namespace Data.Model
{
    public class XCabTrackingFileContentResponse
    {
        public LoginDetails LoginDetails { get; set; }

        public TrackingResponse TrackingResponse { get; set; }

        public FileExtension FileExtension { get; set; }
    }

    public enum FileExtension
    {
        xml,
        csv
    }
}
