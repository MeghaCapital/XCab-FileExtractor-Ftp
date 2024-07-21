using Data.Entities.ExtraReferences;

namespace Data.Repository.EntityRepositories.ExtraReferences.Interface
{
    public interface IExtraReferencesRepository
    {
        ICollection<XCabExtraReferences> GetXCabExtraReferenceses(int bookingId);
        void Insert(ICollection<XCabExtraReferences> xCabExtraReferenceses);
        Task<ICollection<XCabExtraReferences>> GetXCabExtraReferencesesAsync(int bookingId);
    }
}
