using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Api.Bookings.Tms
{
    public class TmsBookingRequest
    {
        public string AccountCode { get; set; }
        public string ServiceCode { get; set; }
        public int StateId { get; set; }
        public AddressDetail FromAddressDetail { get; set; }

        public AddressDetail ToAddressDetail { get; set; }

        public string? TotalItems { get; set; }

        public string? TotalWeight { get; set; }

        public string? Ref1 { get; set; }

        public string? Ref2 { get; set; }

        public List<TmsRequestBookingItem>? Items { get; set; }

        public string? Caller { get; set; }

        public TmsBookingExtras BookingExtras { get; set; }

        public ICollection<TmsBookingRemarks> Remarks { get; set; }

        public ICollection<TmsBookingSundry> Sundries { get; set; }

        public TmsBookingContactInformation? BookingContactInformation { get; set; }

        public DateTime DespatchDateTime { get; set; }

        public DateTime? AdvanceDateTime { get; set; }

        public string? TotalVolume { get; set; }

        public string? ConsignmentNumber { get; set; }

        public string? PreAllocatedDriverNumber { get; set; }

        public int LoginId { get; set; }

        public bool IsQueued { get; set; }

        public string? RunCode { get; set; }

        public bool IsBarcodesAllowed { get; set; }

        public bool OnHoldBooking { get; set; }

        public bool TransportDocumentWillAccompanyLoad { get; set; }

        public bool PackagedInAccordanceWithAdg7_4 { get; set; }

        public bool IsWeight { get; set; }

        public string Username { get; set; }

        public List<string>? Barcodes { get; set; }

        public List<CustomFields>? CustomFields { get; set; }

        public bool IsNtJob { get; set; }

        private static string CleanFields(string input)
        {
            if (input == null)
                return "";
            var rgx = new Regex("[^a-zA-Z0-9-' ]");
            return rgx.Replace(input, "");
        }

        private static string CleanFieldsWithSemiColon(string input)
        {
            if (input == null)
                return "";
            var rgx = new Regex("[^a-zA-Z0-9-' :]");
            return rgx.Replace(input, "");
        }

        private static string CleanFieldsPrserveDecimals(string input)
        {
            if (input == null)
                return "";
            var rgx = new Regex("[^a-zA-Z0-9.: -]");
            return rgx.Replace(input, "");
        }
        private static string CleanStreetAddress(string input)
        {
            if (input == null)
                return "";
            var rgx = new Regex("[^a-zA-Z0-9 ,'/-]");
            return rgx.Replace(input, "");
        }
        private static string CleanReference(string input)
        {
            if (input == null)
                return "";
            var rgx = new Regex("[^a-zA-Z0-9.: ,'/-]");
            return rgx.Replace(input, "");
        }
        /// <summary>
        ///     Cleans up.
        /// </summary>
        public void CleanUp()
        {
            //call clean fields method to remove any characters other than alphabets and numbers and a dash
            AccountCode = CleanFields(AccountCode).ToUpper();
            //clean up the service code and make it upper case as TPLUS only expects upper case service codes
            ServiceCode = CleanFields(ServiceCode).ToUpper();
            //clean up address fields
            FromAddressDetail.AddressLine1 = CleanStreetAddress(FromAddressDetail.AddressLine1);
            FromAddressDetail.AddressLine2 = CleanStreetAddress(FromAddressDetail.AddressLine2);
            FromAddressDetail.AddressLine3 = CleanFieldsPrserveDecimals(FromAddressDetail.AddressLine3);
            //renmoved cleaning up line 4 as for Reward we needed square brackets in the format of Pcs[xx] Wght[dd]
            //frmAddressDetail.AddressLine4 = CleanFields(frmAddressDetail.AddressLine4);

            FromAddressDetail.AddressLine5 = CleanFields(FromAddressDetail.AddressLine5);
            //clean up to address fields
            ToAddressDetail.AddressLine1 = CleanStreetAddress(ToAddressDetail.AddressLine1);
            ToAddressDetail.AddressLine2 = CleanStreetAddress(ToAddressDetail.AddressLine2);
            ToAddressDetail.AddressLine3 = CleanFieldsPrserveDecimals(ToAddressDetail.AddressLine3);
            ToAddressDetail.AddressLine4 = CleanFields(ToAddressDetail.AddressLine4);
            ToAddressDetail.AddressLine5 = CleanFields(ToAddressDetail.AddressLine5);
            //clean up postcode fields & suburb
            FromAddressDetail.Postcode = CleanFields(FromAddressDetail.Postcode);
            FromAddressDetail.Suburb = CleanFields(FromAddressDetail.Suburb).ToUpper();
            //celan up postcode fields & suburb for DEL leg
            ToAddressDetail.Postcode = CleanFields(ToAddressDetail.Postcode);
            ToAddressDetail.Suburb = CleanFields(ToAddressDetail.Suburb).ToUpper();
            //clean up caller
            Caller = CleanFieldsWithSemiColon(Caller);
            //clean up References
            Ref1 = CleanReference(Ref1);
            Ref2 = CleanReference(Ref2);
            //clean up all barcodes
            if (Items != null)
                foreach (var barcode in Items)
                    barcode.Barcode = CleanFields(barcode.Barcode);
        }
    }
}
