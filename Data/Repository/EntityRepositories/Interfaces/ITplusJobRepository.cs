using Core.Helpers;
using Data.Entities.Tplus;
using System;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    interface ITplusJobRepository
    {
        List<TplusJobEntity> GetJobsFromState(TplusConnectionString tpcs, List<string> clientCodes);
        List<TplusMultiLegModel> GetJobsFromState(TplusConnectionString tpcs, List<string> clientCodes,
            DateTime bookingDate, int LoginId, bool ignorePermanentBookings);
        /*        List<TplusJobEntity> GetJobsFromStateForXcab(TplusConnectionString tpcs,  List<string> JobNumbers,
                    DateTime bookingDate);*/
        List<TplusMultiLegModel> GetJobsFromStateForXcab(TplusConnectionString tpcs, List<TplusMultiLegModel> Jobs,
          DateTime bookingDate);

        List<TplusMultiLegModel> GetMultiLegJobsFromStateForXcab(TplusConnectionString tpcs, List<TplusMultiLegModel> Jobs,
            DateTime bookingDate);
        TplusMultiLegModel GetDeliveryEta(TplusConnectionString tpcs, string jobNumber,
            DateTime bookingDate);
        List<TplusMultiLegModel> GetMultiLegJobsFromState(TplusConnectionString tpcs, List<string> clientCodes,
            DateTime bookingDate, int LoginId, bool ignorePermanentBookings);
    }
}
