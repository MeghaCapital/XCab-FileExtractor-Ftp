using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.common;
using xcab.como.common.Data;

namespace xcab.como.booker.Data.Variable
{
    public class SubJob : OrderDefinition
    {
        public SubJob(int order, DateTime requestedDespatchDateTime, string itemDescription, IEnumerable<SubJobLeg> subJobLegs, int serviceId, string addressLine1, int suburbId, string name, string addressLine2 = null, string addressifyString = null, string extraInformation = null, bool? useTolls = null, bool? palletReturnRequired = null, bool? requiresHandUnload = null, string externalBookingReference = null, double? totalWeight = null, int? totalPieces = null, bool isAdvancedBooking = false, string unsPhone = null, string unsEmail = null, List<Barcode> barcodes = null, List<Remarks> remarks = null)
        {
            this.subJobLegs = new List<SubJobLeg>();
            if (subJobLegs != null)
            {
                this.subJobLegs.AddRange(subJobLegs);
            }
            this.address = new Address()
            {
                line1 = addressLine1,
                line2 = addressLine2 ?? string.Empty,
                name = name,
                addressifyString = addressifyString ?? addressLine1
            };
            this.address.suburb.id = suburbId;
            this.service = new Service()
            {
                id = serviceId
            };
            this.dangerousGoods = new List<string>();
            this.order = order;
            this.externalBookingReference = externalBookingReference ?? string.Empty;
            this.requestedDespatchDateTime = requestedDespatchDateTime;
            this.requiresHandUnload = requiresHandUnload;
            this.palletReturnRequired = palletReturnRequired;
            this.useTolls = useTolls;
            this.itemDescription = itemDescription;
            this.extraInformation = extraInformation;
            if (totalWeight.HasValue)
            {
                this.totalWeight = totalWeight.Value;
            }
            if (totalPieces.HasValue)
            {
                this.totalPieces = totalPieces.Value;
            }
            this.isAdvancedBooking = isAdvancedBooking;
            this.unsPhone = unsPhone;
            this.unsEmail = unsEmail;
            this.barcodes = new List<Barcode>();
            if (barcodes != null)
            {
                this.barcodes.AddRange(barcodes);
            }
            this.remarks = new List<Remarks>();
            if (remarks != null)
            {
                this.remarks.AddRange(remarks);
            }
        }

        public SubJob(int porder, DateTime requestedDespatchDateTime, string itemDescription, IEnumerable<SubJobLeg> subJobLegs, int serviceId, int savedLocationId, string addressLine1, int suburbId, string name, string addressLine2 = null, string addressifyString = null, IEnumerable<string> bookingContacts = null, IEnumerable<string> bookingContactSettings = null, string extraInformation = null, double? totalWeight = null, int? totalPieces = null, bool isAdvancedBooking = false, string unsPhone = null, string unsEmail = null, List<Barcode> barcodes = null, bool ? useTolls = null, bool? palletReturnRequired = null, bool? requiresHandUnload = null, string externalBookingReference = null, List<Remarks> remarks = null)
        : this(porder, requestedDespatchDateTime, itemDescription, subJobLegs, serviceId, addressLine1, suburbId, name, addressLine2, addressifyString, extraInformation, useTolls, palletReturnRequired, requiresHandUnload, externalBookingReference, totalWeight, totalPieces, isAdvancedBooking, unsPhone, unsEmail, barcodes, remarks)
        {
            this.bookingContacts = new List<string>();
            this.bookingContactSettings = new List<string>();
            //this.savedLocation = new DiskLocation();
            if (bookingContacts != null)
            {
                this.bookingContacts.AddRange(bookingContacts);
            }
            if (bookingContactSettings != null)
            {
                this.bookingContactSettings.AddRange(bookingContactSettings);
            }
            //this.savedLocation.id = savedLocationId;
        }

        public string externalBookingReference { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'")]
        public DateTime requestedDespatchDateTime { get; set; }

        public bool? requiresHandUnload { get; set; }

        public bool? palletReturnRequired { get; set; }

        public bool? useTolls { get; set; }

        public string itemDescription { get; set; }

        public string extraInformation { get; set; }

        public List<SubJobLeg> subJobLegs { get; private set; }

        public virtual List<string> bookingContacts { get; private set; }

        public bool isAdvancedBooking { get; set; }

        public Address address { get; private set; }

        public Service service { get; private set; }

        public bool overridePrice { get; set; }

        public virtual List<string> bookingContactSettings { get; private set; }

        //Add weight of all items
        public double totalWeight { get; set; }

        //Add quantity of all items
        public int totalPieces { get; set; }

        public int? heaviestItem { get; set; }

        public string unsPhone { get; set; }

        public string unsEmail { get; set; }

        public List<string> dangerousGoods { get; private set; }

        //public virtual DiskLocation savedLocation { get; private set; }

        public List<Barcode> barcodes { get; set; }

        public List<Remarks> remarks { get; set; }

        public bool ShouldSerializebookingContacts()
        {
            return (this.bookingContacts != null);
        }

        public bool ShouldSerializebookingContactSettings()
        {
            return (this.bookingContactSettings != null);
        }

        //public bool ShouldSerializesavedLocation()
        //{
        //    return (this.savedLocation != null);
        //}
    }

    public class Service : IdentityDefinition
    {

    }

    public class Barcode
    {
        public string barcodeString { get; set; }

        public BarcodeScanType barcodeScanType { get; set; }
    }

    public class Remarks 
    {
        public string remarkValue { get; set; }
    }

    public class BarcodeScanType : IdentityDefinition
    {

    }
}
