namespace Core.AddressMapping
{
	public class NhpAddressMapping
	{
        public static Booking GetNhpPickupAddress(EStates stateId)
        {
            var nhpAddress = new Booking();

            switch (stateId)
            {
                case EStates.NSW:
                    nhpAddress.FromDetail1 = "NHP ELECTRICAL";
                    nhpAddress.FromDetail2 = "30-34 DAY STREET NORTH";
                    nhpAddress.FromDetail3 = "";
                    nhpAddress.FromSuburb = "SILVERWATER";
                    nhpAddress.FromPostcode = "2128";
                    nhpAddress.StateId = "2";
                    break;
                case EStates.QLD:
                    nhpAddress.FromDetail1 = "NHP ELECTRICAL ENGINEERING";
                    nhpAddress.FromDetail2 = "16 RIVERVIEW PLACE";
                    nhpAddress.FromDetail3 = "";
                    nhpAddress.FromSuburb = "MURARRIE";
                    nhpAddress.FromPostcode = "4172";
                    nhpAddress.StateId = "3";
                    break;
                case EStates.SA:
                    nhpAddress.FromDetail1 = "CRAIG ARTHUR";
                    nhpAddress.FromDetail2 = "138-144 FRANCIS RD";
                    nhpAddress.FromDetail3 = "";
                    nhpAddress.FromSuburb = "WINGFIELD";
                    nhpAddress.FromPostcode = "5013";
                    nhpAddress.StateId = "4";
                    break;
                case EStates.WA:
                    nhpAddress.FromDetail1 = "NHP PERTH";
                    nhpAddress.FromDetail2 = "38 BELMONT AVE";
                    nhpAddress.FromDetail3 = "";
                    nhpAddress.FromSuburb = "RIVERVALE";
                    nhpAddress.FromPostcode = "6103";
                    nhpAddress.StateId = "5";
                    break;
                case EStates.NT:
                    nhpAddress.FromDetail1 = "AJ COURIERS";
                    nhpAddress.FromDetail2 = "9 ANGLISS ROAD";
                    nhpAddress.FromDetail3 = "";
                    nhpAddress.FromSuburb = "BERRIMAH";
                    nhpAddress.FromPostcode = "0828";
                    nhpAddress.StateId = "3";
                    break;
                default:
                    throw new Exception("Unexpected Case");
            }

            return nhpAddress;
        }

        public static Booking GetNhpDeliveryAddress(EStates stateId)
        {
            var nhpAddress = new Booking();

            switch (stateId)
            {
                case EStates.NSW:
                    nhpAddress.AccountCode = "NHP";
                    nhpAddress.ToDetail1 = "NHP ELECTRICAL ENGINEERING PRO";
                    nhpAddress.ToDetail2 = "30-34 DAY STREET NORTH";
                    nhpAddress.ToSuburb = "SILVERWATER";
                    nhpAddress.ToPostcode = "2128";
                    break;
                case EStates.QLD:
                    nhpAddress.AccountCode = "3NHPQLD";
                    nhpAddress.ToDetail1 = "NHP ELECTRICAL ENGINEERING PRO";
                    nhpAddress.ToDetail2 = "16 RIVERVIEW PLACE";
                    nhpAddress.ToSuburb = "MURARRIE";
                    nhpAddress.ToPostcode = "4172";
                    break;
                case EStates.SA:
                    nhpAddress.AccountCode = "NHPSA";
                    nhpAddress.ToDetail1 = "NHP ELECTRICAL ENGINEERING PRO";
                    nhpAddress.ToDetail2 = "37 ASHFORD ROAD";
                    nhpAddress.ToSuburb = "KESWICK";
                    nhpAddress.ToPostcode = "5035";
                    break;
                case EStates.WA:
                    nhpAddress.AccountCode = "NHPWA";
                    nhpAddress.ToDetail1 = "NHP ELECTRICAL ENGINEERING PRO";
                    nhpAddress.ToDetail2 = "38 BELMONT AVE";
                    nhpAddress.ToSuburb = "RIVERVALE";
                    nhpAddress.ToPostcode = "6103";
                    break;
                default:
                    throw new Exception("Unexpected Case");
            }
            return nhpAddress;
        }
    }
}
