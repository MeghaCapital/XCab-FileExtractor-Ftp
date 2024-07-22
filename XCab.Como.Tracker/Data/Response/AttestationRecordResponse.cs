using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.common.Data;

namespace xcab.como.tracker.Data.Response
{
    //public class AttestationRecordResponse
    //{
    //    public List<AttestationRecord> clientCodes { get; set; }
    //}

    //public class AttestationRecord : IdentityDefinition
    //{
    //    public AttestationRecordClient usedByClient { get; set; }
    //}

    //public class AttestationRecordClient
    //{
    //    public List<AttestationRecordAccount> accounts { get; set; }
    //}

    //public class AttestationRecordAccount
    //{
    //    public AttestationRecordBusiness businessUnit { get; set; }

    //    public List<AttestationRecordJob> jobs { get; set; }
    //}

    //public class AttestationRecordBusiness
    //{
    //    public string name { get; set; }
    //}

    //public class AttestationRecordJob
    //{
    //    public AttestationRecordJobNumber jobNumber { get; set; }

    //    public List<AttestationRecordSubJob> subJobs { get; set; }
    //}

    //public class AttestationRecordJobNumber
    //{
    //    public string displayName { get; set; }
    //}

    //public class AttestationRecordSubJob
    //{
    //    public AttestationRecordDispatchStatus currentDespatchStatus { get; set; }

    //    public List<AttestationRecordTrackingEvent> trackingEvents { get; set; }

    //    public List<AttestationRecordSubJobLeg> subJobLegs { get; set; }

    //    public List<AttestationRecordDocument> podDocuments { get; set; }

    //    public AttestationRecordVehicleLink allocatedVehicleLink { get; set; }
    //}

    //public class AttestationRecordDispatchStatus
    //{
    //    public string name { get; set; }
    //}

    //public class AttestationRecordTrackingEvent
    //{
    //    public DateTime eventDateTimeUTC { get; set; }

    //    public AttestationRecordTrackingEventType trackingEventType { get; set; }

    //    public List<AttestationRecordDocument> podDocuments { get; set; }
    //}

    //public class AttestationRecordTrackingEventType
    //{
    //    public string displayName { get; set; }
    //}

    //public class AttestationRecordSubJobLeg
    //{
    //    public List<AttestationRecordTrackingEvent> trackingEvents { get; set; }

    //}

    //public class AttestationRecordDocument
    //{
    //    public string name { get; set; }

    //    public DateTime dateCreated { get; set; }

    //    public AttestationRecordDocumentType documentType { get; set; }
    //}

    //public class AttestationRecordDocumentType
    //{
    //    public string name { get; set; }
    //}

    //public class AttestationRecordVehicleLink
    //{
    //    public AttestationRecordAllocationNumber allocationNumber { get; set; }
    //}

    //public class AttestationRecordAllocationNumber
    //{
    //    public string displayName { get; set; }
    //}

    public class AttestationRecordResponse
    {
        public List<AttestestionRecordJob> jobs { get; set; }
    }

    public class AttestestionRecordJob
    { 
        public List<AttestationRecordSubJob> subJobs { get; set; }
    }

    public class AttestationRecordSubJob : IdentityDefinition
    { 
        public List<AttestationRecordSubJobLeg> subJobLegs { get; set; }

        public List<AttestationRecordTrackingEvent> trackingEvents { get; set; }

        public AttestationRecordCompletionState completionState { get; set; }

        public Status status { get; set; }

        public CurrentDespatchStatus currentDespatchStatus { get; set; }

        public bool allocatedDriver { get; set; }

        public AttestationRecordVehicleLink allocatedVehicleLink { get; set; }
    }

    public class AttestationRecordVehicleLink
    {
        public AttestationRecordAllocationNumber allocationNumber { get; set; }
    }

    public class AttestationRecordAllocationNumber
    {
        public string displayName { get; set; }
    }

    public class AttestationRecordSubJobLeg : IdentityDefinition
    { 
        public List<AttestationRecordTrackingEvent> trackingEvents { get; set; }

        public AttestationRecordCompletionState completionState { get; set; }
    }

    public class AttestationRecordDriver : IdentityDefinition
    { 
        public string displayName { get; set; }
    }

    public class AttestationRecordTrackingEvent : IdentityDefinition
    { 
        public string signatureName { get; set; }

        public DateTime? signatureSignedOn { get; set; }

        public DateTime? eventDateTimeUTC { get; set; }

        public AttestationRecordTrackingEventType trackingEventType { get; set; }

        public List<AttestationRecordDocument> pocDocuments { get; set; }

        public List<AttestationRecordDocument> podDocuments { get; set; }

        public GeoCoordinate geoCoordinate { get; set; }
    }

    public class GeoCoordinate : IdentityDefinition
    {
        public decimal? latitude { get; set; }

        public decimal? longitude { get; set; }
    }

    public class AttestationRecordTrackingEventType : IdentityDefinition
    { 
        public string name { get; set; }
    }

    public class AttestationRecordDocument : IdentityDefinition
    {
        public AttestationRecordDocumentContainer right { get; set; }
    }

    public class AttestationRecordDocumentType : IdentityDefinition
    { 
        public string name { get; set; }
    }

    public class AttestationRecordDocumentContainer : IdentityDefinition
    {
        public string name { get; set; }

        public DateTime? dateCreated { get; set; }

        public DateTime? dateLastUpdated { get; set; }

        public string fileContent { get; set; }

        public string contentType { get; set; }

        public AttestationRecordDocumentType documentType { get; set; }

        public string fileName { get; set; }
    }

    public class AttestationRecordCompletionState : IdentityDefinition
    { 
        public string name { get; set; }

        public DateTime? dateCreated { get; set; }
    }

    public class Status
    {
        public string name { get; set; }

        public string code { get; set; }

        public ETrackingEvent enumNumber { get; set; }
	}

    public class CurrentDespatchStatus
    {
        public string name { get; set; }
    }
}
