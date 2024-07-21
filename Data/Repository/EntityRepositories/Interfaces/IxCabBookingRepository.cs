using Core;
using Data.Entities;
using Data.Entities.Tplus;
using Data.Model;
using System;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabBookingRepository
    {
        ICollection<TrackingJob> GetTrackingUpdates(ICollection<XCabBooking> allBookings, EStates stateToConnect);
        Task<ICollection<XCabBooking>> GetBookingsForClientNotification(string accountCode, string stateId, string loginId);
        void UpdateTotalDeliveryLegs(int bookingId, int numberOfDelLegs);
        void UpdateJobCompleted(int bookingId);
        ICollection<TplusMultiLegModel> GetJobsNotInXCab(ICollection<TplusMultiLegModel> tplusJobs);
        ICollection<TplusMultiLegModel> GetJobsNotInXCabMultiLegs(ICollection<TplusMultiLegModel> tplusJobs);
        ICollection<TplusMultiLegModel> InsertJobsIntoXCab(ICollection<TplusMultiLegModel> tplusJobs);
        void InsertJobsIntoXCabMultiLegs(ICollection<TplusMultiLegModel> tplusJobs);
        ICollection<XCabBooking> GetBookingForClientReference(DateTime jobDate, string clientReference1, string clientReference2 = "");
        int InsertBooking(XCabBooking booking);
        ICollection<XCabBooking> GetBookingsForTmsTracking(int days);
        int GetBookingIdForMultiLeg(TplusMultiLegModel multiLeg);
        void UpdateStagedBookings(ICollection<int> BookingIds);
        List<XCabBookingDetails> GetPedningTplusJobs(int? testBookingId = null);
    }
}
