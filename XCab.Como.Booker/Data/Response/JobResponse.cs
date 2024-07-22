using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.booker.Data.Response
{
    public class JobResponse
    {
        public InternalJobResponse job { get; set; }
    }

    public class InternalJobResponse
    {
        public string displayName { get; set; }

        public List<SubJobResponse> subJobs { get; set; }
    }

    public class SubJobResponse
    {
        public List<SubJobLegResponse> subJobLegs { get; set; }
    }

    public class SubJobLegResponse
    {
        public ClientPricingResponse clientPricingItem { get; set; }

        public DateTime calculatedETA { get; set; }
    }

    public class ClientPricingResponse
    {
        public List<ItemPriceResponse> chargingMechanismPricingItems { get; set; }
    }

    public class ItemPriceResponse
    {
        public ItemResponse itemPrice { get; set; }
    }

    public class ItemResponse
    {
        public double price { get; set; }
    }
}
