using Core;
using Dapper;
using Data.Model;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.Brand
{
    public class BrandRepository : IBrandRepository
    {
        public async Task<BusinessBrand> GetBrandDetails(string accountCode, int stateId)
        {
            const string BrandName = "Capital Transport";
            const string LogoFileName = "CapitalTransportLogo.jpg";
            var businessBrand = new BusinessBrand
            {
                BrandName = BrandName,
                BrandLogoFileName = LogoFileName
            };
            var sql = @"  select xb.brandname, xb.BrandLogoFilename 
                          from [xCabAuthorizedAccounts] xa join xCabBusinessBrand xb
                          on xa.brandid = xb.id
                          where accountCode = @accountCode and stateId = @stateId";
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("accountCode", accountCode);
                    dynamicParams.Add("stateId", stateId);
                    var sqlResult = await connection.QueryFirstOrDefaultAsync<BusinessBrand>(sql, dynamicParams);
                    if (sqlResult != null)
                    {
                        businessBrand.BrandName = !string.IsNullOrWhiteSpace(sqlResult.BrandName) ? sqlResult.BrandName : BrandName;
                        businessBrand.BrandLogoFileName = !string.IsNullOrWhiteSpace(sqlResult.BrandLogoFileName) ? sqlResult.BrandLogoFileName : LogoFileName;
                    }
                }
                catch (Exception ex)
                {
                    await Logger.Log("Exception Occurred while extracting brand name. Message : " + ex.Message, Name());
                }
            }
            return businessBrand;
        }

        private string Name()
        {
            return GetType().Name;
        }
    }
}
