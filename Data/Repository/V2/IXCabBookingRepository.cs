using Data.Api.TrackingEvents;
using Data.Api.TrackingEvents.Model;
using Data.Entities;
using Data.Entities.Items;
using Data.Model;
using Data.Model.Tracking;

namespace Data.Repository.V2
{
	public interface IXCabBookingRepository
	{
		Task<int> InsertBooking(XCabBooking xCabBooking);
		Task<XCabBookingRepository.UpdateStatus> UpdateBooking(XCabBooking xCabBooking);
		Task<bool> AddItems(int loginId, string accountCode, int stateId, EReferenceType referenceType, string referenceValue, ICollection<XCabItems> xCabItems);
		Task<bool> AddItems(int loginId, EReferenceType referenceType, string referenceValue, ICollection<XCabItems> xCabItems);
		Task<bool> UpdateXCabTrackingEvents(ICollection<XCabTrackingEvent> xCabTrackingEvent, ICollection<TrackingJob> trackingJobs);
		Task<ICollection<XCabTrackingEvent>> GetXCabTrackingEvents(XCabTrackingRequest xCabTrackingRequest);
		Task<bool> UpdateXCabTrackingEventsForCancellationAndDriverAllocation(ICollection<XCabTrackingEvent> xCabTrackingEvent);
		Task<ICollection<XCabBookingDetails>> GetPendingXCabBookings(int? testBookingId);
		Task<bool> CancelXCabBooking(int bookingId);
		Task<XCabTrackingEvent> GetUltimateLegDetailsOfFutileJob(string ultimateLegConsignmentNumber);
		Task<XCabBooking> GetBookingByReferenceLoginAndCode(string reference, int login, string serviceCode);
		Task<XCabBooking> GetBookingByReferenceAndLogin(string reference, int login);

		Task<ICollection<Reference>> GetReferencesByPrimaryJob(int primaryJob);
		Task<bool> UpdateTrackingEventsDetails(ICollection<TrackingJob> trackingJobs);
		Task<XCabTrackingEvent> GetTrackingStatusForOw(DateTime fromDate, DateTime toDate, string accountCode, int stateId, string reference1);
		Task<bool> UpdateXCabBookingWithBookingDetailsCreatedInTms(XCabBooking xCabBooking);
		Task<List<XCabAsnBooking>> GetAsnXCabBookingsForAccount(string accountCode, int stateId, DateTime fromDateTime, DateTime toDateTime);
		Task<int> InsertBookingForConsolidation(XCabAsnBooking xCabAsnBooking);
		Task<ICollection<int>> ActivateBookings(ActivateBookingsRequest activateBookingsRequest);
		Task<bool> UpdateBookingRoute(XCabBooking booking, bool updateCaller = false);
		Task<List<XCabBooking>> GetBookingsByBookingIdAndLogin(string bookingId, int login);
		Task<bool> IsBookingFutiledForReference(string reference, int loginId);
		Task<bool> UpdateBookingETA(int bookingId, string ETA);
	}
}
