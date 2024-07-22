using Core;
using Data.Api.TrackingEvents;
using Data.Api.TrackingEvents.Model;
using xcab.como.common;
using xcab.como.common.Service;
using xcab.como.tracker.Data.Models;
using xcab.como.tracker.Data.Response;

namespace xcab.como.tracker.Service
{
    public class EventExtractor : ComoDefaultProcessor, IEventExtractor
    {
        public EventExtractor() : base()
        {
            Client.Initialise(base.ApiToken);
            Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);
        }
        public TmsTrackingEvents GetTrackingEventsForComoJobs(TmsTrackingRequest trackingRequest)
        {
            var trackingEvent = new TmsTrackingEvents();
            try
            {
                var comoJobId = trackingRequest.ComoJobId;
                var operation = "TrackingEvents";

                string query = @"query " + operation + @" {
                                    jobs(position: 0, limit: 0, filters: {id: {_eq: " + comoJobId + @"}}) {
                                        subJobs {
                                            id
                                            allocatedDriver
                                            allocatedVehicleLink {
                                                allocationNumber {
                                                    displayName
                                                }
                                            }
                                            status
                                            {
                                            name
                                            code
                                            enumNumber
                                            }
                                            completionState {
                                            id
                                            name
                                            dateCreated
                                            }
                                            trackingEvents {
                                            id
                                            signatureName
                                            signatureSignedOn
                                            trackingEventType {
                                                id
                                                name
                                            }
                                            eventDateTimeUTC
                                            geoCoordinate {
                                                id
                                                latitude
                                                longitude
                                            }
                                            }
                                            subJobLegs {
                                            id
                                            status
                                            {
                                                name
                                                code
                                                enumNumber
                                            }
                                            completionState {
                                                id
                                                name
                                                dateCreated
                                            }
                                            trackingEvents {
                                                id
                                                signatureName
                                                signatureSignedOn
                                                trackingEventType {
                                                id
                                                name
                                                }
                                                eventDateTimeUTC
                                                geoCoordinate {
                                                id
                                                latitude
                                                longitude
                                                }
                                            }
                                            }
                                        }
                                        }
                                    }
                                    ";


                AttestationRecordResponse existingJobResponse = Task.Run(async () => await Client.GetAsync<AttestationRecordResponse>(
                    operation,
                    query
                )).Result;

                if ((existingJobResponse != null) && (existingJobResponse.jobs != null))
                {
                    for (int i = 0; i < existingJobResponse.jobs.Count; i++)
                    {
                        var job = existingJobResponse.jobs[i];

                        if ((job != null) && (job.subJobs != null))
                        {
                            for (int j = 0; j < job.subJobs.Count; j++)
                            {
                                var subJob = job.subJobs[j];
                                var eventToTrack = subJob.trackingEvents[0];

                                if (subJob != null)
                                {
                                    if (subJob.status.enumNumber == ETrackingEvent.CANCELLED)
                                    {
                                        trackingEvent.Cancelled = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (subJob.trackingEvents != null)
                                        {
                                            for (int m = 0; m < subJob.trackingEvents.Count; m++)
                                            {
                                                var subJobTrackingEvent = subJob.trackingEvents[m];

                                                if (subJobTrackingEvent != null)
                                                {
                                                    var subJobTrackingEventType = subJobTrackingEvent.trackingEventType;

                                                    if (string.Equals(subJobTrackingEventType.name.ToUpper(), "ARRIVE"))
                                                    {
                                                        trackingEvent.PickupArriveDateTime = Core.Helpers.DateTimeHelpers.GetLocalDateTimeFromUtc(trackingRequest.StateId, (DateTime)subJobTrackingEvent.eventDateTimeUTC);
                                                        if (subJobTrackingEvent.geoCoordinate != null)
                                                        {
                                                            trackingEvent.PickupArriveLatitude = subJobTrackingEvent.geoCoordinate.latitude.ToString();
                                                            trackingEvent.PickupArriveLongitude = subJobTrackingEvent.geoCoordinate.longitude.ToString();
                                                        }
                                                    }
                                                    else if (string.Equals(subJobTrackingEventType.name.ToUpper(), "COMPLETE"))
                                                    {
                                                        trackingEvent.PickupCompleteDateTime = Core.Helpers.DateTimeHelpers.GetLocalDateTimeFromUtc(trackingRequest.StateId, (DateTime)subJobTrackingEvent.eventDateTimeUTC);
                                                        if (subJobTrackingEvent.geoCoordinate != null)
                                                        {
                                                            trackingEvent.PickupCompleteLatitude = subJobTrackingEvent.geoCoordinate.latitude.ToString();
                                                            trackingEvent.PickupCompleteLongitude = subJobTrackingEvent.geoCoordinate.longitude.ToString();
                                                        }
                                                    }
                                                    //For futile jobs we need to add the logic for futile jobs
                                                    else if (string.Equals(subJobTrackingEventType.name.ToUpper(), "FUTILE"))
                                                    {
                                                        //trackingEvent.Name = ETrackingEvent.JOBMODIFIED;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (subJob.subJobLegs != null)
                                {
                                    for (int k = 0; k < subJob.subJobLegs.Count; k++)
                                    {
                                        var subJobLeg = subJob.subJobLegs[k];

                                        if (subJobLeg != null)
                                        {
                                            if (subJobLeg.trackingEvents != null)
                                            {
                                                for (int m = 0; m < subJobLeg.trackingEvents.Count; m++)
                                                {
                                                    var subJobLegTrackingEvent = subJobLeg.trackingEvents[m];

                                                    if (subJobLegTrackingEvent != null)
                                                    {
                                                        var subJobLegTrackingEventType = subJobLegTrackingEvent.trackingEventType;

                                                        if (string.Equals(subJobLegTrackingEventType.name.ToUpper(), "ARRIVE"))
                                                        {
                                                            trackingEvent.DeliveryArriveDateTime = Core.Helpers.DateTimeHelpers.GetLocalDateTimeFromUtc(trackingRequest.StateId, (DateTime)subJobLegTrackingEvent.eventDateTimeUTC);
                                                            if (subJobLegTrackingEvent.geoCoordinate != null)
                                                            {
                                                                trackingEvent.DeliveryArriveLatitude = subJobLegTrackingEvent.geoCoordinate.latitude.ToString();
                                                                trackingEvent.DeliveryArriveLongitude = subJobLegTrackingEvent.geoCoordinate.longitude.ToString();
                                                            }
                                                        }
                                                        else if (string.Equals(subJobLegTrackingEventType.name.ToUpper(), "COMPLETE"))
                                                        {
                                                            trackingEvent.DeliveryCompleteDateTime = Core.Helpers.DateTimeHelpers.GetLocalDateTimeFromUtc(trackingRequest.StateId, (DateTime)subJobLegTrackingEvent.eventDateTimeUTC);
                                                            if (subJobLegTrackingEvent.geoCoordinate != null)
                                                            {
                                                                trackingEvent.DeliveryCompleteLatitude = subJobLegTrackingEvent.geoCoordinate.latitude.ToString();
                                                                trackingEvent.DeliveryCompleteLongitude = subJobLegTrackingEvent.geoCoordinate.longitude.ToString();
                                                            }
                                                        }
                                                        //For futile jobs we need to add the logic for futile jobs
                                                        else if (string.Equals(subJobLegTrackingEventType.name.ToUpper(), "FUTILE"))
                                                        {
                                                            //trackingEvent.Name = ETrackingEvent.JOBMODIFIED;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                trackingEvent.BookingId = trackingRequest.XCabBookingId;
                trackingEvent.StateId = trackingRequest.StateId;
                trackingEvent.ComoJobId = trackingRequest.ComoJobId;

                return trackingEvent;
            }
            catch (Exception)
            {
                // log
            }

            return trackingEvent;
            
        }

        public TmsTrackingEvents GetAllocationAndCancelEventsForComoJobs(TmsTrackingRequest trackingRequest)
        {
            var trackingEvent = new TmsTrackingEvents();
            try
            {
                var comoJobId = trackingRequest.ComoJobId;
                var operation = "AllocationOrCancellationEvents";

                string query = @"query " + operation + @" {
                        jobs(position: 0, limit: 0, filters: {id: {_eq: " + comoJobId + @"}}) {
                            subJobs {
                                id
                                allocatedDriver
                                allocatedVehicleLink {
                                    allocationNumber {
                                        displayName
                                    }
                                }
                                status
                                {
                                name
                                code
                                enumNumber
                                }
                                completionState {
                                id
                                name
                                dateCreated
                                }
                                trackingEvents {
                                id
                                signatureName
                                signatureSignedOn
                                trackingEventType {
                                    id
                                    name
                                }
                                eventDateTimeUTC
                                geoCoordinate {
                                    id
                                    latitude
                                    longitude
                                }
                                }
                                subJobLegs {
                                id
                                status
                                {
                                    name
                                    code
                                    enumNumber
                                }
                                completionState {
                                    id
                                    name
                                    dateCreated
                                }
                                trackingEvents {
                                    id
                                    signatureName
                                    signatureSignedOn
                                    trackingEventType {
                                    id
                                    name
                                    }
                                    eventDateTimeUTC
                                    geoCoordinate {
                                    id
                                    latitude
                                    longitude
                                    }
                                }
                                }
                            }
                            }
                        }
                        ";


                AttestationRecordResponse existingJobResponse = Task.Run(async () => await Client.GetAsync<AttestationRecordResponse>(
                    operation,
                    query
                )).Result;

                if ((existingJobResponse != null) && (existingJobResponse.jobs != null))
                {
                    for (int i = 0; i < existingJobResponse.jobs.Count; i++)
                    {
                        var job = existingJobResponse.jobs[i];

                        if ((job != null) && (job.subJobs != null))
                        {
                            for (int j = 0; j < job.subJobs.Count; j++)
                            {
                                var subJob = job.subJobs[j];
                                var eventToTrack = subJob.trackingEvents[0];

                                if (subJob != null)
                                {
                                    if (subJob.status.enumNumber == ETrackingEvent.CANCELLED)
                                    {
                                        trackingEvent.Cancelled = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (subJob.trackingEvents[0].trackingEventType.name.ToString().ToUpper() == "ALLOCATED")
                                        {
                                            if (subJob.allocatedDriver && subJob.allocatedVehicleLink != null)
                                            {
                                                if (subJob.allocatedVehicleLink.allocationNumber.displayName != null)
                                                {
                                                    trackingEvent.DriverNumber = int.Parse(subJob.allocatedVehicleLink.allocationNumber.displayName.ToString());
                                                }

                                                if (eventToTrack != null && eventToTrack.eventDateTimeUTC != null)
                                                {
                                                    trackingEvent.AllocationDateTime = Core.Helpers.DateTimeHelpers.GetLocalDateTimeFromUtc(trackingRequest.StateId, (DateTime)eventToTrack.eventDateTimeUTC);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                trackingEvent.BookingId = trackingRequest.XCabBookingId;
                trackingEvent.StateId = trackingRequest.StateId;

                return trackingEvent;
            }
            catch (Exception)
            {
                // log
            }

            return trackingEvent;

        }
    }
}
