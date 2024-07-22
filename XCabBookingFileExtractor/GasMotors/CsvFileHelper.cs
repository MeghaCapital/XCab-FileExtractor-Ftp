using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace XCabBookingFileExtractor.GasMotors
{
    public class CsvFileHelper : ICsvFileHelper
    {
        public List<GasMotorsCsvRow> GetFilecontents(string filePath)
        {
            var records = new List<GasMotorsCsvRow>();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };
                using (var reader = new StreamReader(@filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    //csv.Configuration.HasHeaderRecord = false;
                    records = csv.GetRecords<GasMotorsCsvRow>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Exception Occurred while reading csv file contents for Gas Motors , Exception: {e.Message}", "GasMotorsBooking");
                return records;
            }
        }
    }
}
