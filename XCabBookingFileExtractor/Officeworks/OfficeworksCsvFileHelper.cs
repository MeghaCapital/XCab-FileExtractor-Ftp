using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace XCabBookingFileExtractor.Officeworks
{
    public class OfficeworksCsvFileHelper : IOfficeworksCsvFileHelper
    {
        public List<OfficeworkskCsvRow> GetFilecontents(string filePath)
        {
            var records = new List<OfficeworkskCsvRow>();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };
                using (var reader = new StreamReader(@filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    // csv.Configuration.HasHeaderRecord = false;
                    records = csv.GetRecords<OfficeworkskCsvRow>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Exception Occurred while reading csv file contents for Officeworks, Exception: {e.Message}", "OfficeworksBooking");
                return records;
            }
        }
    }
}

