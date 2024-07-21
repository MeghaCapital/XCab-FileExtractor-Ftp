using Data.Entities.EmailNotification;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    interface IXCabEmailClientRepository
    {
        Task<ICollection<XCabEmailClient>> GetXCabEmailClients();
        XCabEmailClient GetClientForId(string id);
        //ICollection<XCabEmailClientTemplate> GetXCabEmailClientTemplates(string emailClientId);
    }
}
