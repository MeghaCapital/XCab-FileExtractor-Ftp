using Core;
using Dapper;
using Data.Model.Tracking.IKEA;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.Ikea
{
    public class IkeaTrackingStatisticsRepository : IIkeaTrackingStatisticsRepository
    {
        private static string dateToCheckRecords = DateTime.Today.ToString("yyyy-MM-dd");
        string sqlLegacyPickupJobCount = $@"select count(*)  from xCabBooking xb join eint.xCabExtraReferences xr   on xb.BookingId = xr.PrimaryBookingId  where LoginId = 160 and TPLUS_JobAllocationDate >= '{dateToCheckRecords}' and PickupComplete is not null   and xr.Name = 'ShipmentType' and Value = 'LEGACY'";
        string sqlCFBPickupJobCount = $"select count(*)   from xCabBooking xb join eint.xCabExtraReferences xr   on xb.BookingId = xr.PrimaryBookingId  join xCabItems xi on xb.BookingId = xi.BookingId  where LoginId = 160 and TPLUS_JobAllocationDate >= '{dateToCheckRecords}' and PickupComplete is not null   and xr.Name = 'ShipmentType' and Value = 'LCD' and xi.Barcode is not null and xi.Barcode != ''";
        string sqlCDCPickupJobCount = $"select count(*)   from xCabBooking xb join eint.xCabExtraReferences xr   on xb.BookingId = xr.PrimaryBookingId  join xCabItems xi on xb.BookingId = xi.BookingId  where LoginId = 160 and TPLUS_JobAllocationDate >= '{dateToCheckRecords}' and PickupComplete is not null   and xr.Name = 'ShipmentType' and Value = 'CCD' and xi.Barcode is not null and xi.Barcode != ''";

        string sqlFutileJobCount = $"select count(*) from JobFutile where AccountCode in ('KMIKS', 'KMIKCC', 'KMIKR', 'KSIKR', 'KPIKP') and LastUpdated >= '{dateToCheckRecords}'";

        // Delievry jobs count
        string sqlLegacyDeliveryJobCount = $@"select count(*)  from xCabBooking xb join eint.xCabExtraReferences xr   on xb.BookingId = xr.PrimaryBookingId  where LoginId = 160 and TPLUS_JobAllocationDate >= '{dateToCheckRecords}' and Completed = 1   and xr.Name = 'ShipmentType' and Value = 'LEGACY'";
        string sqlCFBDeliveryJobCount = $"select count(*)   from xCabBooking xb join eint.xCabExtraReferences xr   on xb.BookingId = xr.PrimaryBookingId  join xCabItems xi on xb.BookingId = xi.BookingId  where LoginId = 160 and TPLUS_JobAllocationDate >= '{dateToCheckRecords}' and Completed =1   and xr.Name = 'ShipmentType' and Value = 'LCD' and xi.Barcode is not null and xi.Barcode != ''";
        string sqlCDCDeliveryJobCount = $"select count(*)   from xCabBooking xb join eint.xCabExtraReferences xr   on xb.BookingId = xr.PrimaryBookingId  join xCabItems xi on xb.BookingId = xi.BookingId  where LoginId = 160 and TPLUS_JobAllocationDate >= '{dateToCheckRecords}' and Completed =1   and xr.Name = 'ShipmentType' and Value = 'CCD' and xi.Barcode is not null and xi.Barcode != ''";

        public async Task<IkeaTrackingStatisticModel> GetStatisticsForIkeaTrackingEvents()
        {
            IkeaTrackingStatisticModel ikeaTrackingStatisticModel = new IkeaTrackingStatisticModel();
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();

                    // Expected file count for pickup jobs
                    ikeaTrackingStatisticModel.ExpectedFileCountForLegacyPickedUpJobs = await connection.QueryFirstOrDefaultAsync<int>(sqlLegacyPickupJobCount);
                    ikeaTrackingStatisticModel.ExpectedFileCountForCFBPickedUpJobs = await connection.QueryFirstOrDefaultAsync<int>(sqlCFBPickupJobCount);
                    ikeaTrackingStatisticModel.ExpectedFileCountForCDCPickedUpJobs = await connection.QueryFirstOrDefaultAsync<int>(sqlCDCPickupJobCount);
                    // Expected file count for futile jobs
                    ikeaTrackingStatisticModel.ExpectedFileCountForFutileJobs = await connection.QueryFirstOrDefaultAsync<int>(sqlFutileJobCount);

                    // Expected file count for delivery jobs
                    ikeaTrackingStatisticModel.ExpectedFileCountForLegacyDeliveredJobs = await connection.QueryFirstOrDefaultAsync<int>(sqlLegacyDeliveryJobCount);
                    ikeaTrackingStatisticModel.ExpectedFileCountForCFBDeliveredJobs = await connection.QueryFirstOrDefaultAsync<int>(sqlCFBDeliveryJobCount);
                    ikeaTrackingStatisticModel.ExpectedFileCountForCDCDeliveredJobs = await connection.QueryFirstOrDefaultAsync<int>(sqlCDCDeliveryJobCount);

                    var expectedLegacyFileCount = ikeaTrackingStatisticModel.ExpectedFileCountForLegacyDeliveredJobs * 2 + (ikeaTrackingStatisticModel.ExpectedFileCountForLegacyPickedUpJobs - ikeaTrackingStatisticModel.ExpectedFileCountForLegacyDeliveredJobs) * 1;
                    var expectedCFBFileCount = ikeaTrackingStatisticModel.ExpectedFileCountForCFBDeliveredJobs * 3 + (ikeaTrackingStatisticModel.ExpectedFileCountForCFBPickedUpJobs - ikeaTrackingStatisticModel.ExpectedFileCountForCFBDeliveredJobs) * 2;
                    var expectedCDCFileCount = ikeaTrackingStatisticModel.ExpectedFileCountForCDCDeliveredJobs * 3 + (ikeaTrackingStatisticModel.ExpectedFileCountForCDCPickedUpJobs - ikeaTrackingStatisticModel.ExpectedFileCountForCDCDeliveredJobs) * 2;

                    ikeaTrackingStatisticModel.TotalExpectedFileCount = expectedLegacyFileCount + expectedCFBFileCount + expectedCDCFileCount + ikeaTrackingStatisticModel.ExpectedFileCountForFutileJobs;
                    ikeaTrackingStatisticModel.ActualUploadedFileCount = GetNumberOfFiles();
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                   $"Exception Occurred while retrieving data for Ikea tracking statistics.Details: {ex.Message}", Name());
            }
            return ikeaTrackingStatisticModel;
        }

        public async Task InsertTrackingStatistics(IkeaTrackingStatisticModel ikeaTrackingStatisticModel)
        {
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Execute("insert into xCabIkeaTrackingStatistics " +
                        "(ExpectedFileCountForLegacyPickedUpJobs, ExpectedFileCountForCFBPickedUpJobs, ExpectedFileCountForCDCPickedUpJobs, ExpectedFileCountForFutileJobs, ExpectedFileCountForLegacyDeliveredJobs, ExpectedFileCountForCFBDeliveredJobs, ExpectedFileCountForCDCDeliveredJobs, TotalExpectedFileCount, ActualUploadedFileCount) values " +
                        "(@ExpectedFileCountForLegacyPickedUpJobs, @ExpectedFileCountForCFBPickedUpJobs, @ExpectedFileCountForCDCPickedUpJobs, @ExpectedFileCountForFutileJobs, @ExpectedFileCountForLegacyDeliveredJobs, @ExpectedFileCountForCFBDeliveredJobs, @ExpectedFileCountForCDCDeliveredJobs, @TotalExpectedFileCount, @ActualUploadedFileCount)",
                                              new
                                              {
                                                  ikeaTrackingStatisticModel.ExpectedFileCountForLegacyPickedUpJobs,
                                                  ikeaTrackingStatisticModel.ExpectedFileCountForCFBPickedUpJobs,
                                                  ikeaTrackingStatisticModel.ExpectedFileCountForCDCPickedUpJobs,
                                                  ikeaTrackingStatisticModel.ExpectedFileCountForFutileJobs,
                                                  ikeaTrackingStatisticModel.ExpectedFileCountForLegacyDeliveredJobs,
                                                  ikeaTrackingStatisticModel.ExpectedFileCountForCFBDeliveredJobs,
                                                  ikeaTrackingStatisticModel.ExpectedFileCountForCDCDeliveredJobs,
                                                  ikeaTrackingStatisticModel.TotalExpectedFileCount,
                                                  ikeaTrackingStatisticModel.ActualUploadedFileCount
                                              });
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                  $"Exception Occurred inserting data for Ikea tracking statistics.Details: {ex.Message}", Name());
            }
        }

        private int GetNumberOfFiles()
        {
            var fileName = $"*_{DateTime.Today.ToString("d_M_yyyy")}*.xml";
            return Directory.GetFiles(@"\\challenge\national\FTP\Home\ikea\Tracking\Processed", fileName, SearchOption.TopDirectoryOnly).Length;
        }

        private static string Name()
        {
            return "IkeaTrackingStatisticsRepository";
        }
    }
}
