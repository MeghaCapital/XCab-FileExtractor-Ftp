using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.common;
using xcab.como.common.Data;

namespace xcab.como.booker.Data.Variable
{
    public class SubJobLeg : OrderDefinition
    {
        public SubJobLeg(int order, IEnumerable<Reference> orderedReferences, string addressLine1, int suburbId, string name, DateTime? eta = null, IEnumerable<string> jobPriceSettings = null, bool authorityToLeaveSafe = false, bool etaIsOverridden = false, bool etaIsDeadline = false, string addressLine2 = null, string addressifyString = null, string extraInformation = null, double clientPricing = 0.0)
        {
            this.orderedReferences = new List<Reference>();
            if (orderedReferences != null)
            {
                this.orderedReferences.AddRange(orderedReferences);
            }
            this.address = new Address()
            {
                line1 = addressLine1,
                line2 = addressLine2 ?? string.Empty,
                name = name,
                addressifyString = addressifyString ?? addressLine1
            };
            this.address.suburb.id = suburbId;
            if (eta.HasValue)
            {
                this.internalFields = new Estimation()
                {
                    eTA = eta.Value,
                    etaIsDeadline = etaIsDeadline,
                    eTAOverridden = etaIsOverridden
                };
            }
            this.jobPriceSettings = new List<string>();
            this.extraInformation = extraInformation;
            this.order = order;
            this.authorityToLeaveSafe = authorityToLeaveSafe;
            if (jobPriceSettings != null)
            {
                this.jobPriceSettings.AddRange(jobPriceSettings);
            }

            if(clientPricing > 0)
            {
                var serviceChargingMechanismPricingItem = new List<serviceChargingMechanismPricingItem>();

                serviceChargingMechanismPricingItem.Add(new serviceChargingMechanismPricingItem() { enteredQuantity = clientPricing });

                this.driverPricingItem = new legPricingItem()
                {
                    chargingMechanismPricingItems = serviceChargingMechanismPricingItem
                };                
            }
        }

        public SubJobLeg(int order, IEnumerable<Reference> orderedReferences, string addressLine1, int suburbId, string name, legPricingItem driverPricing, double clientPricing, DateTime? eta = null, IEnumerable<string> bookingContacts = null, IEnumerable<string> bookingContactSettings = null, IEnumerable<string> jobPriceSettings = null, bool authorityToLeaveSafe = false, bool etaIsOverridden = false, bool etaIsDeadline = false, string addressLine2 = null, string addressifyString = null, string extraInformation = null)
            : this(order, orderedReferences, addressLine1, suburbId, name, eta, jobPriceSettings, authorityToLeaveSafe, etaIsOverridden, etaIsDeadline, addressLine2, addressifyString, extraInformation, clientPricing)
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
            if (driverPricing != null)
            {
                this.driverPricingItem = driverPricing;
            }
        }

        public List<Reference> orderedReferences { get; private set; }

        public string extraInformation { get; set; }

        public Address address { get; private set; }

        public Estimation internalFields { get; set; }

        public bool authorityToLeaveSafe { get; set; }

        public virtual legPricingItem driverPricingItem { get; private set; }

        public virtual legPricingItem clientPricingItem { get; private set; }

        public virtual List<string> bookingContacts { get; private set; }

        public virtual List<string> bookingContactSettings { get; private set; }

        public List<string> jobPriceSettings { get; private set; }

        //public virtual DiskLocation savedLocation { get; private set; }

        public bool ShouldSerializedriverPricingItem()
        {
            return (driverPricingItem != null);
        }

        public bool ShouldSerializeclientPricingItem()
        {
            return (this.clientPricingItem != null);
        }

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

    public class Estimation
    {
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'")]
        public DateTime eTA { get; set; }

        public bool etaIsDeadline { get; set; }

        public bool eTAOverridden { get; set; }
    }

    public class legPricingItem
    {
        public legPricingItem()
        {
            this.chargingMechanismPricingItems = new List<serviceChargingMechanismPricingItem>();
            this.surchargePricingItems = new List<double>();
        }

        public List<serviceChargingMechanismPricingItem> chargingMechanismPricingItems { get;  set; }

        public List<double> surchargePricingItems { get; private set; }

        public double fuel { get; set; }

        public double total { get; set; }
    }

    public class serviceChargingMechanismPricingItem
    {
        public serviceChargingMechanismPricingItem()
        {
            this.chargingMechanism = new ServiceChargingMechanism();
        }
        public double enteredQuantity { get; set; }     

        public ServiceChargingMechanism chargingMechanism { get; private set; }
    }

    public class ServiceChargingMechanism : IdentityDefinition
    {
        
    }

    public class Reference : OrderDefinition
    {
        public string value { get; set; }
    }
}
