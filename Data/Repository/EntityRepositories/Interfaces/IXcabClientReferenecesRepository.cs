using Data.Entities.ConsolidatedReferences;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabClientReferenecesRepository
    {
        void Insert(ICollection<XCabClientReferences> xCabClientReferences);
        ICollection<XCabClientReferences> GetXCabClientReferences(int bookingId);
    }
}