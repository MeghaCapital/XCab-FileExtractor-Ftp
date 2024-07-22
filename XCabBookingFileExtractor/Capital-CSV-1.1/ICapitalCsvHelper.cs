using System.Collections.Generic;

namespace XCabBookingFileExtractor.Capital_CSV_1_1
{
    public interface ICapitalCsvHelper
    {
        List<CapitalCsvRow> GetCSVFilecontents(string filePath);
    }
}
