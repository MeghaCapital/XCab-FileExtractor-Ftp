using System.Collections.Generic;

namespace XCabBookingFileExtractor.GasMotors
{
    public interface ICsvFileHelper
    {
        List<GasMotorsCsvRow> GetFilecontents(string filePath);
    }
}
