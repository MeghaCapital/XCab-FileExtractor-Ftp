using System.Collections.Generic;

namespace XCabBookingFileExtractor.Utils
{
    public class CsvRow
    {
        public string Caller { get; set; }

        public ICollection<CsvColumn> Columns { get; set; }
        /*   public string Column1 { get; set; }
           public string Column2 { get; set; }
           public string Column3 { get; set; }
           public string Column4 { get; set; }
           public string Column5 { get; set; }
           public string Column6 { get; set; }
           public string Column7 { get; set; }
           public string Column8 { get; set; }
           public string Column9 { get; set; }
           public string Column10 { get; set; }*/
    }
}
