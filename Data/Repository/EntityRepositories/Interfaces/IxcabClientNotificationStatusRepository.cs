using Data.Entities.EmailNotification;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabClientNotificationStatusRepository
    {
        Task<ICollection<XCabClientNotificationStatus>> GetClientNotificationStatuses(string bookingId);
        Task UpdateClientNotificationStatuses(ICollection<XCabClientNotificationStatus> xcabClientNotificationStatuses);
    }
}
