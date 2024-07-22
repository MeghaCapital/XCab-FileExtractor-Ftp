using Core;
using System.Collections.Generic;

namespace XCabBookingFileExtractor.Utils.Pdf
{
    interface IExtractor
    {
        Dictionary<string, List<Booking>> Extract(string fileName);        
    }
}
