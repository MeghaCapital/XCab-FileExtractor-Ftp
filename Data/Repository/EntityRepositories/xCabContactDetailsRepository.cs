using Dapper;
using Data.Entities.ContactDetails;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.EntityRepositories
{
    public class xCabContactDetailsRepository
    {
        public List<XCabContactDetails> GetXCabContactDetails(int bookingId)
        {
            var Contacts = new List<XCabContactDetails>();

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("BookingId", bookingId);
                const string sql = @"SELECT [BookingId],[AreaCode],[PhoneNumber] FROM [dbo].[xCabContactDetails] WHERE BookingId = @BookingId";
                Contacts = connection.Query<XCabContactDetails>(sql, dynamicParams).ToList();
            }
            return Contacts;
        }
    }
}
