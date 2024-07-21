using Data.Entities.Ftp;
using Data.Entities.GenericIntegration;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabClientFtpIntegrationRepository
    {
        ICollection<XCabClientFtpIntegration> GetClientFtpIntegrations();
        ICollection<XCabClientFtpIntegration> GetClientFtpCsvIntegrations();
        XCabClientIntegrationCsvColumnMap GetClientIntegrationCsvColumnMaps(int clientId);
    }
}
