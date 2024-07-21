namespace Core.AddressMapping
{
    public class DuskAndDawnAddressMapping
    {
        public static Booking GetDuskAndDawnPickupAddress(EStates stateId)
        {
            var pickupAddress = new Booking();

            switch (stateId)
            {            
                case EStates.QLD:
                    pickupAddress.FromDetail1 = "Capital Transport";
                    pickupAddress.FromDetail2 = "420 Nudgee Rd";
                    pickupAddress.FromDetail3 = "SHED 18A";
                    pickupAddress.FromSuburb = "HENDRA";
                    pickupAddress.FromPostcode = "4011";
                    break;            
                default:
                    throw new Exception("Unexpected Case");
            }

            return pickupAddress;
        }
    }
}
