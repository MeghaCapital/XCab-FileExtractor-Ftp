using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interface.ExternalClientIntegration
{
    public interface IExternalClientIntegrationRepository
    {        
        Task<Entities.ExternalClientIntegration.ExternalClientIntegration> GetExternalClientIntegrationForId(string id);
    }
}
