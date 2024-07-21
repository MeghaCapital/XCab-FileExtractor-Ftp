using Data.Entities.Ftp;
using Data.Model;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IXCabFtpLoginDetailsRepository
    {
        ICollection<XCabFtpLoginDetailsRestfulWsModel> GetXCabFtpLoginDetailsRestfulWs();
        int IsUserAuthenticatedForRestfulWs(string username, string password);
        ICollection<XCabFtpLoginDetails> GetXCabFtpLoginDetailsCsvClients();
        ICollection<XCabFtpLoginDetails> GetXCabFtpLoginDetailsForTmsTrackingClients();

        int AuthenticateUser(string username, string password);
    }
}
