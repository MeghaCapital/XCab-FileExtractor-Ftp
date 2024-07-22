using System.Collections.Generic;

namespace XCabBookingFileExtractor.Officeworks
{
    public interface IOfficeworksCsvFileHelper
    {
        List<OfficeworkskCsvRow> GetFilecontents(string filePath);
    }
}
