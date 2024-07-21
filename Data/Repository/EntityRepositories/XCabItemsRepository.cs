using Core;
using Dapper;
using Data.Entities.Items;
using Data.Repository.EntityRepositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories
{
    public class XCabItemsRepository : IXCabItemsRepository
    {
        public List<XCabItems> GetXcabItems(int bookingId)
        {
            List<XCabItems> Items = null;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("BookingId", bookingId);
                const string sql = @"SELECT [ItemId]
                                  ,[BookingId]
                                  ,[Description]
                                  ,[Length]
                                  ,[Width]
                                  ,[Height]
                                  ,[Weight]
                                  ,[Cubic]
                                  ,[Barcode]
                                  ,[Qantity]
                                  ,[Status]
                              FROM [dbo].[xCabItems] WHERE BookingId = @BookingId";
                Items = connection.Query<XCabItems>(sql, dynamicParams).ToList();
            }
            return Items;
        }

        public async Task<List<string>> GetBarcodes(int bookingId)
        {
            List<string> barcodes = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("BookingId", bookingId);
                    const string sql = @"SELECT 
                                  [Barcode]
                              FROM [dbo].[xCabItems] WHERE BookingId = @BookingId and (Barcode is not null and Len(Barcode) != 0)";
                    barcodes = (List<string>)await connection.QueryAsync<string>(sql, dynamicParams);
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                       $"Exception Occurred while extracting barcodes. Details:{ex.Message}", "XCabItemsRepository");
                }
            }
            return barcodes;
        }
    }
}
