using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OfficeOpenXml;
namespace Core.OfficeUtils
{
    public static class OfficeUtil
    {
        /// <summary>
        ///     Converts the XLSM to CSV.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="sheetToExtract">The sheet to extract.</param>
        /// <param name="outputFilename">The output filename.</param>
        /// <param name="delimiterCharacter">The delimiter character.</param>
        public static void ConvertXlsmToCsv(string filename, string sheetToExtract, string outputFilename,
            char delimiterCharacter)
        {
            try
            {
                using (var p = new ExcelPackage(new FileInfo(filename)))
                {
                    //find the sheet that we need to extract
                    var sheet = p.Workbook.Worksheets[sheetToExtract];
                    using (var writer = File.CreateText(outputFilename))
                    {
                        //read all the contents from the sheet and save them as CSV
                        var rowCount = sheet.Dimension.End.Row;
                        var columnCount = sheet.Dimension.End.Column;
                        for (var r = 1; r <= rowCount; r++)
                        {
                            for (var c = 1; c <= columnCount; c++)
                            {
                                writer.Write(sheet.Cells[r, c].Value);
                                writer.Write(delimiterCharacter);
                            }
                            writer.WriteLine();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ConvertXlsmToCsv(string filename, string outputFilename, char delimiterCharacter)
        {
            try
            {
                using (var p = new ExcelPackage(new FileInfo(filename)))
                {
                    //find the sheet that we need to extract
                    if (p.Workbook.Worksheets.Count < 1) return;
                    //var sheet = p.Workbook.Worksheets[0];
                    var sheet = p.Workbook.Worksheets.ElementAt(0);
                    using (var writer = File.CreateText(outputFilename))
                    {
                        //read all the contents from the sheet and save them as CSV
                        var rowCount = sheet.Dimension.End.Row;
                        var columnCount = sheet.Dimension.End.Column;
                        for (var r = 1; r <= rowCount; r++)
                        {
                            for (var c = 1; c <= columnCount; c++)
                            {
                                var val = sheet.Cells[r, c].Value;
                                if (val != null)
                                {
                                    writer.Write(val.ToString().Replace(',', ' '));

                                }
                                writer.Write(delimiterCharacter);
                            }
                            writer.WriteLine();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     ConvertCsvToXml
        /// </summary>
        /// <param name="inputFilename">The input filename.</param>
        /// <param name="useHeader">if set to <c>true</c> [use header].</param>
        /// <param name="splitCharacter">The split character.</param>
        /// <param name="outputFileName">Name of the output file.</param>
        public static void ConvertCsvToXml(string inputFilename, bool useHeader, char splitCharacter,
            string outputFileName)
        {
            try
            {
                var lines = File.ReadAllLines(inputFilename);
                XElement xml = null;
                if (useHeader)
                {
                    var headers = lines[0].Split(splitCharacter).Select(x => x.Trim('\"')).ToArray();

                    xml = new XElement("TopElement",
                        lines.Where((line, index) => index > 0).Select(line => new XElement("Item",
                            line.Split(splitCharacter)
                                .Select((column, index) => new XElement(headers[index].Replace(" ", ""), column)))));
                }
                else
                {
                    xml = new XElement("TopElement",
                        lines.Select(line => new XElement("Item",
                            line.Split(splitCharacter)
                                .Select((column, index) => new XElement("Column" + index, column)))));
                }
                xml.Save(outputFileName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void ConvertCsvToXml(string inputFilename, bool useHeader, string splitCharacter,
    string outputFileName)
        {
            try
            {
                var lines = File.ReadAllLines(inputFilename);
                XElement xml = null;
                if (useHeader)
                {
                    var headers = lines[0].Split(new[] { splitCharacter }, StringSplitOptions.None).Select(x => x.Trim('\"')).ToArray();

                    xml = new XElement("TopElement",
                        lines.Where((line, index) => index > 0).Select(line => new XElement("Item",
                            line.Split(new[] { splitCharacter }, StringSplitOptions.None)
                                .Select((column, index) => new XElement(headers[index].Replace(" ", ""), column)))));
                }
                else
                {
                    xml = new XElement("TopElement",
                        lines.Select(line => new XElement("Item",
                            line.Split(new[] { splitCharacter }, StringSplitOptions.None)
                                .Select((column, index) => new XElement("Column" + index, column)))));
                }
                xml.Save(outputFileName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void ConvertCsvToXmlIgnoreEmptyValues(string inputFilename, bool useHeader, string splitCharacter,
string outputFileName)
        {
            try
            {
                var lines = File.ReadAllLines(inputFilename);
                XElement xml = null;
                if (useHeader)
                {
                    var headers = lines[0].Split(new[] { splitCharacter }, StringSplitOptions.None).Select(x => x.Trim('\"')).ToArray();

                    xml = new XElement("TopElement",
                        lines.Where((line, index) => index > 0).Select(line => new XElement("Item",
                            line.Split(new[] { splitCharacter }, StringSplitOptions.None)
                                .Select((column, index) => new XElement(headers[index].Replace(" ", ""), column)))));
                }
                else
                {
                    xml = new XElement("TopElement",
                        lines.Where((line, index) => index > 0)
                        .Select(line => new XElement("Item",
                            line.Split(new[] { splitCharacter }, StringSplitOptions.None)
                                .Select((column, index) => new XElement("Column" + index, column)))));
                }
                xml.Save(outputFileName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
