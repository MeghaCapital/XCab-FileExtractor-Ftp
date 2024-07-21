using Data.Model.Address;
using System.Collections.Generic;


namespace Data.Repository.EntityRepositories.DeliverySuburb.Interface
{
    public interface IXCabDeliverySuburbRepository
    {
        bool IsDeliverySuburbAllowed(string suburb, string postcode, int stateId);
        Task<bool> IsPickupOrDeliverySuburbAllowed(string suburb, string postcode, int stateId);
        Task<bool> IsNonMetroSuburb(string serviceCode, int stateId);
        bool CheckStandardAllowedForZone(int StateId, string fromSuburb, string fromPostcode, string toSuburb, string toPostcode);
        Task<bool> CheckStandardAllowedForZone(int StateId, string fromSuburb, string toSuburb);
        ICollection<Suburb> GetMetroSuburbs(string stateId = null);
        Task<bool> IsValidInterstateBooking(string fromPostcode, string toPostcode);
        bool IsZoneServiceAllowed(string FromSuburb, string FromPostcode, string ToSuburb, string ToPostcode, int stateId);
        Task<bool> IsZoneServiceAllowed(string FromSuburb, string ToSuburb, int stateId);
        bool IsStandardServiceAllowed(string FromSuburb, string FromPostcode, string ToSuburb, string ToPostcode, int StateId, string ServiceCode);
        Task<bool> IsStandardServiceAllowed(string FromSuburb, string ToSuburb, int StateId, string ServiceCode);
        Task<bool> IsInMyerQLDZoneDelivery(string FromSuburb, string ToSuburb, int stateId);
        Task<bool> IsZoneServiceAllowedHubSystem(string FromSuburb, string ToSuburb, int stateId);
    }
}
