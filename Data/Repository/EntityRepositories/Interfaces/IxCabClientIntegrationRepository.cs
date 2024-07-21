using Data.Entities.GenericIntegration;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabClientIntegrationRepository
    {
        /// <summary>
        /// Gets the client integrations.
        /// </summary>
        /// <returns></returns>
        ICollection<XCabClientIntegration> GetClientIntegrations(int clientIntId = 0);
        /// <summary>
        /// Gets the client integration CSV column maps.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        XCabClientIntegrationCsvColumnMap GetClientIntegrationCsvColumnMaps(int clientId);
        /// <summary>
        /// Gets the CAB client integration NFS details.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        XCabClientIntegrationNfsDetail GetCabClientIntegrationNfsDetails(int clientId);

        XCabClientIntegration GetDefaultAddressDetails(string ClientCode, int stateId);
        ICollection<XCabClientIntegration> GetDefaultAddressDetails(List<string> CLientCodes);
    }
}
