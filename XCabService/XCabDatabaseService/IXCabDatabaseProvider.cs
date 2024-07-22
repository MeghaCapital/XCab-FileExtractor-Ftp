using Data.Entities;
using Data.Entities.Items;
using Data.Model;

namespace XCabService.XCabDatabaseService
{
	public interface IXCabDatabaseProvider
	{
		Task<int> InsertXCabBooking(XCabBooking xcabBooking);
		Task<bool> AddXCabItems(int loginId, string accountCode, int stateId, EReferenceType referenceType, string referenceValue, ICollection<XCabItems> xcabItems);
		Task<bool> AddXCabItems(int loginId, EReferenceType referenceType, string referenceValue, ICollection<XCabItems> xcabItems);
		Task<ICollection<XCabBookingDetails>> GetPendingXCabBookings(int? testId);
		Task<bool> CancelXCabBooking(int bookingId);
		Task<bool> UpdateXCabBookingWithBookingDetailsCreatedInTms(XCabBooking xCabBooking);
		Task<List<XCabAsnBooking>> GetAsnXCabBookingsForAccount(string accountCode, int stateId, DateTime fromDateTime, DateTime toDateTime);
	}
}
