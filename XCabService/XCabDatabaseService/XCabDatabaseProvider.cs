using Data.Entities;
using Data.Entities.Items;
using Data.Model;
using Data.Repository.V2;

namespace XCabService.XCabDatabaseService
{
	public class XCabDatabaseProvider : IXCabDatabaseProvider
	{
		IXCabBookingRepository _xCabBookingRepository;
		public XCabDatabaseProvider(IXCabBookingRepository xCabBookingRepository)
		{
			_xCabBookingRepository = xCabBookingRepository;
		}
		public XCabDatabaseProvider()
		{
			_xCabBookingRepository = new XCabBookingRepository();
		}
		public async Task<int> InsertXCabBooking(XCabBooking xcabBooking)
		{
			var xCabBookingId = await _xCabBookingRepository.InsertBooking(xcabBooking);
			return xCabBookingId;
		}

		public async Task<bool> AddXCabItems(int loginId, string accountCode, int stateId, EReferenceType referenceType, string referenceValue, ICollection<XCabItems> xcabItems)
		{
			var added = await _xCabBookingRepository.AddItems(loginId, accountCode, stateId, referenceType, referenceValue, xcabItems);
			return added;
		}

		public async Task<bool> AddXCabItems(int loginId, EReferenceType referenceType, string referenceValue, ICollection<XCabItems> xcabItems)
		{
			var added = await _xCabBookingRepository.AddItems(loginId, referenceType, referenceValue, xcabItems);
			return added;
		}

		public Task<ICollection<XCabBookingDetails>> GetPendingXCabBookings(int? testId)
		{
			return _xCabBookingRepository.GetPendingXCabBookings(testId);
		}

		public Task<bool> CancelXCabBooking(int bookingId)
		{
			return _xCabBookingRepository.CancelXCabBooking(bookingId);
		}

		public Task<bool> UpdateXCabBookingWithBookingDetailsCreatedInTms(XCabBooking xCabBooking)
		{
			return _xCabBookingRepository.UpdateXCabBookingWithBookingDetailsCreatedInTms(xCabBooking);
		}
		public Task<List<XCabAsnBooking>> GetAsnXCabBookingsForAccount(string accountCode, int stateId, DateTime fromDateTime, DateTime toDateTime)
		{
			return _xCabBookingRepository.GetAsnXCabBookingsForAccount(accountCode, stateId, fromDateTime, toDateTime);
		}

	}
}
