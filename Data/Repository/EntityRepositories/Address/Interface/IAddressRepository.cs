using Core;

namespace Data.Repository.EntityRepositories.Address.Interface
{
    interface IAddressRepository
    {
        Booking CleanAddressFields(Booking Booking, string State, char SplitChar);
        bool ValidateSuburb(Booking booking, string state, out string failureNote);
    }
}
