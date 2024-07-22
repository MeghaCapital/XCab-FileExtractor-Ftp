using Core;
using System.Collections.Generic;

namespace XCabBookingFileExtractor.Officeworks
{
    public interface IOfficeworksTextFileHelper
    {
        ICollection<Booking> ConvertTextFile(string filePath, string stateAbrrv, string fileType);
    }
}
