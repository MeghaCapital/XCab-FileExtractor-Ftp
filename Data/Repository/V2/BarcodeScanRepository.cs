using Core;
using Dapper;
using Data.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
    public class BarcodeScanRepository : IBarcodeScanRepository
    {
        public async Task<ICollection<XCabBarcodeScan>> ExtractXcabScanDetails(string jobNumber, string accountCode, DateTime jobDate, int stateId)
        {
            var xCabBarcodeScans = new List<XCabBarcodeScan>();

            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT DISTINCT A.JobNumber,A.SubJobNumber,A.Status,a.BarCode,B.ExceptionReason,b.AuthName,A.DriverId,A.IsAdHocBarcode
                                        FROM [bcs].[BarcodeScan] A
	                                    LEFT JOIN [bcs].[BarcodeException] B ON B.BarcodeId = A.id
										INNER JOIN [dbo].[xCabBooking] XBC ON XBC.TPLUS_JobNumber = A.JobNumber AND XBC.AccountCode = A.AccountCode AND XBC.StateId = A.StateId AND (CONVERT(DATE, XBC.UploadDateTime) BETWEEN DATEADD(day,-10, A.JobDate) AND A.JobDate)
										WHERE A.JobNumber = {jobNumber} 
                                            AND A.AccountCode = '{accountCode}' 
                                            AND A.StateId = {stateId} 
                                            AND A.JobDate = '{jobDate.ToString("yyyy-MM-dd")}'
                                            AND XBC.Cancelled = 0
                                        UNION
                                        SELECT DISTINCT A.JobNumber,A.SubJobNumber,A.Status,a.BarCode,B.ExceptionReason,b.AuthName,A.DriverId,A.IsAdHocBarcode
                                        FROM [bcs].[BarcodeScan] A
	                                    LEFT JOIN [bcs].[BarcodeException] B ON B.BarcodeId = A.id
										INNER JOIN [dbo].[xCabBooking] XBC ON XBC.TPLUS_JobNumber = A.JobNumber AND XBC.AccountCode = A.AccountCode AND XBC.StateId = A.StateId AND (CONVERT(DATE, XBC.UploadDateTime) BETWEEN DATEADD(day,-10, A.JobDate) AND A.JobDate)
										INNER JOIN [dbo].[xCabClientReferences] XCR ON XCR.PrimaryJobId = XBC.BookingId
                                        INNER JOIN [dbo].[xCabConsolidatedReferences] C ON C.ConsolidateJobId = XBC.BookingId AND C.Barcode = A.BarCode AND C.ConsolidateJobId IS NULL
										WHERE A.JobNumber = {jobNumber} 
                                            AND A.AccountCode = '{accountCode}' 
                                            AND A.StateId = {stateId} 
                                            AND A.JobDate = '{jobDate.ToString("yyyy-MM-dd")}'
                                            AND XBC.Cancelled = 0 
                                            AND UPPER(XBC.Ref1) LIKE '%INVOICE%'
                                        UNION
                                        SELECT DISTINCT A.JobNumber,A.SubJobNumber,A.Status,a.BarCode,B.ExceptionReason,b.AuthName,A.DriverId,A.IsAdHocBarcode
                                        FROM [bcs].[BarcodeScan] A
	                                    LEFT JOIN [bcs].[BarcodeException] B ON B.BarcodeId = A.id
										INNER JOIN [dbo].[xCabBooking] XBC ON XBC.TPLUS_JobNumber = A.JobNumber AND XBC.AccountCode = A.AccountCode AND XBC.StateId = A.StateId AND (CONVERT(DATE, XBC.UploadDateTime) BETWEEN DATEADD(day,-10, A.JobDate) AND A.JobDate)
										INNER JOIN [dbo].[xCabConsolidatedReferences] C ON C.ConsolidateJobId = XBC.BookingId AND C.Barcode = A.BarCode
										INNER JOIN [dbo].[xCabBooking] XBO ON XBO.BookingId =C.PrimaryJobId 
	                                    WHERE A.JobNumber = {jobNumber} 
                                            AND A.AccountCode = '{accountCode}' 
                                            AND A.StateId = {stateId} 
                                            AND A.JobDate = '{jobDate.ToString("yyyy-MM-dd")}'
										    AND XBO.Cancelled = 1";

                        xCabBarcodeScans = (List<XCabBarcodeScan>)await connection.QueryAsync<XCabBarcodeScan>(sql);
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                     "Exception Occurred when extracting barcode scan details in " + nameof(BarcodeScanRepository) + ": " + nameof(ExtractXcabScanDetails) + ", message: " +
                     e.Message, nameof(BarcodeScanRepository));
            }

            return xCabBarcodeScans;
       }
    }
}
