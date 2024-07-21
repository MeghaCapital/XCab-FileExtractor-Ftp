using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.EntityRepositories
{
    internal class ILogixSettingsrepository : IILogixSettingsrepository
    {
        public bool IsUsingArchivalDatabase(DateTime allocationDateTime)
        {
            bool usingArchiveDatabase = false;
            string archivalDate = " ";
            var connectionString = DbSettings.Default.ILogixMysqlConnection;
            var sql = "SELECT Value FROM appsettings WHERE Id = 1";
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (var cmd = new MySqlCommand(sql, sqlConnection))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            archivalDate = reader["Value"].ToString();
                        }
                    }
                    if (archivalDate.Length > 0)
                    {
                        if (allocationDateTime < DateTime.Parse(archivalDate))
                        {
                            usingArchiveDatabase = true;
                        }
                    }
                }
                catch (Exception)
                {
                    //Core.Logger.SendHtmlEmail("Exception Occurred in ILogixImagesRepository: GetPodImageV2, message: " + ex.Message, "ILogixImagesRepository");
                }               
            }
            return usingArchiveDatabase;
        }
    }
}
