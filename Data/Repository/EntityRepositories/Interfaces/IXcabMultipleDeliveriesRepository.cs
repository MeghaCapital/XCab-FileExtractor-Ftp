using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    interface IXCabMultipleDeliveriesRepository
    {
        ICollection<Entities.XCabMultipleDeliveries.XCabMultipleDeliveries> GetXCabMultipleDeliveries(int BookingId);
    }
}
