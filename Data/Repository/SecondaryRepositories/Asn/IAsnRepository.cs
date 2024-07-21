using Data.Api.Asn;

namespace Data.Repository.SecondaryRepositories.Asn
{
    public interface IAsnRepository
    {
        Task<bool> UpdateAsnBooking(AsnUpdateRequest asnBookingRequest);
    }
}
