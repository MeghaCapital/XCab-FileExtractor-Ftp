using Core;
using Data.Entities;
using Data.Entities.Tplus;
using System;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IXCabBookingRepository
    {
        ICollection<TrackingJob> GetTrackingUpdates(ICollection<XCabBooking> allBookings, EStates stateToConnect);
        ICollection<XCabBooking> GetBookingsForClientNotification(string accountCode, string stateId, string loginId);
        void UpdateTotalDeliveryLegs(int bookingId, int numberOfDelLegs);
        void UpdateJobCompleted(int bookingId);
        ICollection<TplusJobEntity> GetJobsNotInXCab(ICollection<TplusJobEntity> tplusJobs);
        void InsertJobsIntoXCab(ICollection<TplusJobEntity> tplusJobs);
        ICollection<XCabBooking> GetBookingForClientReference(DateTime jobDate, string clientReference1, string clientReference2 = "");
        int InsertBooking(XCabBooking booking);
        ICollection<XCabBooking> GetBookingsForTmsTracking(int days);
    }
}
