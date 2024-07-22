using System.Collections.Generic;

namespace XCabBookingFileExtractor.Officeworks
{
    interface IOfficeworksQLDDCCsvFileHelper
    {
        List<OfficeworksQLDDCCsvRow> GetFilecontents(string filePath);
    }
}
