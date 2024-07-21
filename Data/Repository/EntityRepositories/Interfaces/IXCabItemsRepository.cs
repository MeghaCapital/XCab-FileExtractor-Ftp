
using Data.Entities.Items;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabItemsRepository
    {
        List<XCabItems> GetXcabItems(int bookingId);
        Task<List<string>> GetBarcodes(int bookingId);
    }
}
