using Data.Model.ConsolidatedBooking;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Data.Model.ServiceCodes
{
    /// <summary>
    ///     Defines the Model associated with a Service Quote request
    /// </summary>
    public class ShipmentModel
    {
        /// <summary>
        ///     Defines the structure of an Quote service booking request
        /// </summary>
        public class ServiceQuoteRequest
        {
            /// <summary>
            ///     User Credentials for request authentication
            /// </summary>
            public Credentials UserCredentials { get; set; }
            /// <summary>
            ///     Account code
            /// </summary>
            public string AccountCode { get; set; }
            /// <summary>
            /// Defines the State where the client is based
            /// </summary>
            public ShipmentState State { get; set; }
            /// <summary>
            ///     Defines the Service type.
            /// </summary>
            public ServiceType? ServiceType { get; set; }
            /// <summary>
            ///     Defines the Ugency level
            /// </summary>
            public UrgencyLevel? UrgencyLevel { get; set; }
            /// <summary>
            ///       Defines the number of pallets in the shipment
            /// </summary>
            public int? NumberOfPallets { get; set; }
            /// <summary>
            ///     Defines the total weight of the shipemnt in KG
            /// </summary>
            public int? TotalWeightInKg { get; set; }
            /// <summary>
            ///     Defines the total cubic is square cm
            /// </summary>
            public double? TotalCubic { get; set; }
            /// <summary>
            ///       Defines the length of the largest item in cm
            /// </summary>
            public double? MaxLengthInCm { get; set; }
            /// <summary>
            ///       Defines the width of the largest item in cm
            /// </summary>
            public double? MaxWidthInCm { get; set; }
            /// <summary>
            ///       Defines the height of the largest item in cm
            /// </summary>
            public double? MaxHeightInCm { get; set; }
            /// <summary>
            ///       Defines the weight of the largest item in kg
            /// </summary>
            public double? MaxWeightInKg { get; set; }
            /// <summary>
            ///       Defines the post code where the shipment should be picked
            /// </summary>
            public string? FromPostCode { get; set; }
            /// <summary>
            ///       Defines the suburb where the shipment should be picked
            /// </summary>
            public string FromSuburb { get; set; }
            /// <summary>
            ///       Defines the post code where the shipment should be delivered
            /// </summary>
            public string? ToPostCode { get; set; }
            /// <summary>
            ///       Defines the suburb where the shipment should be delivered
            /// </summary>
            public string ToSuburb { get; set; }
            /// <summary>            
            ///     Defines whether the goods can be carried on racks 
            /// </summary>
            public bool? AllowOnRacks { get; set; }
            /// <summary>
            ///     Defines whether the vehicle should be enclosed
            /// </summary>
            public bool? EnclosedVehicle { get; set; }
            /// <summary>
            ///     Defines whether the vehicle should contain a crane
            /// </summary>
            public bool? WithCrane { get; set; }
            /// <summary>
            ///      Defines whether the vehicle should contain a hydraulic tailgate
            /// </summary>
            public bool? WithTailgate { get; set; }

            /// <summary>
            /// Defines the handling type - supported values are "any", "manual", "mechanical"
            /// </summary>
            public string? HandlingType { get; set; }

            /// <summary>
            /// List of Items
            /// </summary>
            public virtual List<BaseBookingItem>? BookingItems { get; set; }
        }

        /// <summary>
        ///     Service Type 
        /// </summary>  
        public class ServiceQuoteResponse
        {
            /// <summary>
            ///     Defines status code of the response
            /// </summary> 
            [JsonProperty("StatusCode")]
            public ShipmentStatusCode StatusCode { get; set; }

            /// <summary>
            ///     Description of the response status
            /// </summary>   
            [JsonProperty("StatusDescription")]
            public string StatusDescription { get; set; }

            /// <summary>
            ///     Defines list of Quote Service
            /// </summary>
            [JsonProperty("QuoteService")]
            public List<ServicePrice> QuoteService { get; set; }
        }
        /// <summary>
        /// Defines the Service Filter
        /// </summary>
        public class ServiceFilter : ServicePrice
        {
            /// <summary>
            ///     Defines Accountcode which ghad madethe request
            /// </summary>     
            public string AccountCode { get; set; }

            /// <summary>
            ///     From Postcode (Postcode from where the goods will be picked up)
            /// </summary>
            public string FromPostcode { get; set; }

            /// <summary>
            ///     From Suburb (Suburb from where the goods will be picked up)
            /// </summary>
            public string FromSuburb { get; set; }

            /// <summary>
            ///     To Postcode (Postcode to where the goods will be delivered)
            /// </summary>
            public string ToPostcode { get; set; }

            /// <summary>
            ///     To Suburb (Suburb to where the goods will be delivered)
            /// </summary>
            public string ToSuburb { get; set; }
        }

        /// <summary>
        ///     Service Details
        /// </summary>  
        public class ServicePrice
        {
            /// <summary>
            ///     Service code
            /// </summary>
            [JsonProperty("ServiceCode")]
            public string ServiceCode { get; set; }
            /// <summary>
            ///     Description of the Service code
            /// </summary>
            [JsonProperty("Description")]
            public string Description { get; set; }
            /// <summary>
            ///    Estimated time for Pickup
            /// </summary>
            [JsonProperty("PickupEta")]
            public string PickupEta { get; set; }
            /// <summary>
            ///    Estimated time for Delivery
            /// </summary>
            [JsonProperty("DeliveryEta")]
            public string DeliveryEta { get; set; }
            /// <summary>
            ///     Freight cost for the Quote
            /// </summary>
            [JsonProperty("FreightCost")]
            public FreightCost FreightCost { get; set; }

        }
        /// <summary>
        /// Defines the Freight Cost
        /// </summary>
        public class FreightCost
        {
            /// <summary>
            ///     Job Price in AUD Excluding GST
            /// </summary>
            [JsonProperty("JobPriceExGst")]
            public string JobPriceExGst { get; set; }

            /// <summary>
            ///     GST in AUD for the Booking
            /// </summary>
            [JsonProperty("Gst")]
            public string Gst { get; set; }

            /// <summary>
            ///     Job Total Price Including GST in AUD
            /// </summary>
            [JsonProperty("JobTotalPrice")]
            public string JobTotalPrice { get; set; }
        }

        /// <summary>
        /// Defines the Freight Cost with SOAP request and response details
        /// </summary>
        public class SOAPFreightResponse
        {
            /// <summary>
            ///     Request made to SOAP to get the quote
            /// </summary>
            public string SOAPRequest { get; set; }
            /// <summary>
            ///    Response form the SOAP for the request
            /// </summary>
            public string SOAPResponse { get; set; }
            /// <summary>
            ///    Freight Cost
            /// </summary>
            public FreightCost FreightCost { get; set; }
        }

        /// <summary>
        /// Defines the Shipment Status Codes
        /// </summary>
        public enum ShipmentStatusCode
        {
            /// <summary>Booking Successfully Created</summary>
            [JsonProperty("Ok")]
            Ok = 1,
            /// <summary>Authentication Failed</summary>
            [JsonProperty("AuthenticationFailed")]
            AuthenticationFailed = 2,
            /// <summary>Invalid Booking Request - please check that all required parameters are provided with the request</summary>
            [JsonProperty("InvalidQuoteRequest")]
            InvalidQuoteRequest = 3,
            /// <summary>System Error</summary>
            [JsonProperty("SystemError")]
            SystemError = 4,
            /// <summary>Authorization Failed</summary>
            [JsonProperty("AuthorizationFailed")]
            AuthorizationFailed = 5,
            /// <summary>Exceeds guidelines for HandlingType</summary>
            [JsonProperty("ExceedsGuidelinesError")]
            ExceedsGuidelinesError = 6
        }

        /// <summary>
        ///     Defines the State
        /// </summary>
        public enum ShipmentState
        {
            /// <summary>Victoria</summary>
            Victoria = 1,
            /// <summary>New South Wales</summary>
            NewSouthWales = 2,
            /// <summary>Queensland</summary>
            Queensland = 3,
            /// <summary>South Australia</summary>
            SouthAustralia = 4,
            /// <summary>Western Australia</summary>
            WesternAustralia = 5
        }
    }

    /// <summary>
    ///     Service code filtered according to a criteria
    /// </summary>
    public class ServiceCodeFilter
    {
        /// <summary>Service code</summary>
        public string ServiceCode { get; set; }
        /// <summary>Description of the service code</summary>
        public string ServiceCodeDesc { get; set; }
        /// <summary>ETA for pickup</summary>
        public string PickUpEta { get; set; }
        /// <summary>ETA for delivery</summary>
        public string DeliveryEta { get; set; }
    }
    /// <summary>
    /// Defines the Service Type
    /// </summary>
    public enum ServiceType
    {
        /// <summary>Courier</summary>
        Courier = 1,
        /// <summary>Taxi Truck - Fixed</summary>
        TaxiTruckFixed = 2,
        /// <summary>Taxi Truck - Hourly</summary>
        TaxiTruckHourly = 3
    }

    /// <summary>
    ///     Defines the Ugency level
    /// </summary>
    public enum UrgencyLevel
    {
        /// <summary>Standard</summary>
        All = 0,
        /// <summary>Standard</summary>
        Standard = 1,
        /// <summary>Express</summary>
        Express = 2,
        /// <summary>Very Important</summary>
        Vip = 3,
        /// <summary>Cheapest Services</summary>
        Cheapest = 4,
        /// <summary>Quickest Services</summary>
        Quickest = 5
    }

    /// <summary>
    ///     Description for providing User Credentials
    /// </summary>
    public class Credentials
    {
        /// <summary>
        ///     Username to be provided with every request
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Shared Key to be provided with every request
        /// </summary>
        public string SharedKey { get; set; }
    }

    public class ManualLiftRestrictions
    {
        public string ItemDescription { get; set; }

        public double MaximumDimension { get; set; }

        public double MedianDimension { get; set; }

        public double MinimumDimension { get; set; }

        public double ItemWeight { get; set; }
    }
}