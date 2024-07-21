using Data.Api.Asn;
using Data.Model;

namespace Data.Repository.EntityRepositories.Asn
{
    public interface IAsnRepository
    {
        Task<bool> UpdateAsnBooking(AsnUpdateRequest asnBookingRequest);
        Task<bool> ReleaseAsnBooking(List<XCabAsnBooking> lstXcabAsnBooking);
    }
}
