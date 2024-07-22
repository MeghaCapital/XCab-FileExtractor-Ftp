using Core;
using System.Collections.Generic;

namespace XCabBookingFileExtractor.Sigma
{
    public interface ISigmaFileHelper
    {
        ICollection<Booking> ConvertTextFile(string filePath);
    }
}
