using Data.Entities.Ftp;
using Data.Model;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabFtpLoginDetailsRepository
    {
        ICollection<XCabFtpLoginDetailsRestfulWsModel> GetXCabFtpLoginDetailsRestfulWs();
        int IsUserAuthenticatedForRestfulWs(string username, string password);
        Task<int> IsUserAuthenticatedForRestfulWsAsync(string username, string password);
        Task<int> IsUserAuthenticatedForRestfulWsAsyncFromTest(string username, string password);
        ICollection<XCabFtpLoginDetails> GetXCabFtpLoginDetailsCsvClients();
        ICollection<XCabFtpLoginDetails> GetXCabFtpLoginDetailsForTmsTrackingClients();

        Task<int> IsUserAuthenticatedForRestfulWsAsync(string username, string password, string apiKey);
        Task<int> IsUserAuthenticatedForRestfulWsAsyncFromTest(string username, string password, string apiKey);
    }
}
