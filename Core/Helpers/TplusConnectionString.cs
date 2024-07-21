namespace Core.Helpers
{
    public enum TplusConnectionString
    {
        [StringValue("127.0.0.1")]
        NotSet = 0,
        [StringValue("192.168.160.2")]
        Melbourne = 1,
        [StringValue("192.168.163.3")]
        Sydney = 2,
        [StringValue("192.168.167.3")]
        Brisbane = 3,
        [StringValue("192.168.169.3")]
        Adelaide = 4,
        [StringValue("192.168.165.3")]
        Perth = 5,

    }

    public static class TplusConnectionStringsExtensions
    {
        public static string GetConnectionString(this TplusConnectionString tpcs)
        {
            return $@"Data Source =(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL= TCP)(HOST = {tpcs.GetValueAttr()})(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = TPLUS)));User Id=tplus_user;password=iam34;";
        }
    }
}
