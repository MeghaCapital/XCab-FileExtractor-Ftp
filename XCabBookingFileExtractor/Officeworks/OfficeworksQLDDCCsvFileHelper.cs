using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace XCabBookingFileExtractor.Officeworks
{
    public class OfficeworksQLDDCCsvFileHelper : IOfficeworksQLDDCCsvFileHelper
    {
        public List<OfficeworksQLDDCCsvRow> GetFilecontents(string filePath)
        {
            var records = new List<OfficeworksQLDDCCsvRow>();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    BadDataFound = null,
                    Delimiter = ",",
                    PrepareHeaderForMatch = args => Regex.Replace(args.Header.ToString(), @"[\/ ( ) . \- \s]", string.Empty)
                };
                using (var reader = new StreamReader(@filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    //csv.Configuration.HasHeaderRecord = true;
                    //csv.Configuration.HeaderValidated = null;
                    //csv.Configuration.MissingFieldFound = null;
                    //csv.Configuration.BadDataFound = null;
                    //csv.Configuration.PrepareHeaderForMatch = header => Regex.Replace(header, @"[\/ ( ) . \- \s]", string.Empty);
                    //csv.Configuration.Delimiter = ",";
                    records = csv.GetRecords<OfficeworksQLDDCCsvRow>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Exception Occurred while reading csv file contents for Officeworks QLD DC, Exception: {e.Message}", "OfficeworksQLDDCBooking");
                return records;
            }
        }
    }
}
