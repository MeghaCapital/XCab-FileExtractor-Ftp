namespace Data.Utils.Helpers
{
    public class CapitalWarehouse
    {
        public static AddressDetail GetwarehouseAddress(int stateId)
        {
            var wareHouseAddress = new AddressDetail();

            switch (stateId)
            {
                case 1:
                    wareHouseAddress.AddressLine1 = "Capital Transport VIC - WH3";
                    wareHouseAddress.AddressLine2 = "636 Wellington Rd";
                    wareHouseAddress.AddressLine3 = "0385620001";
                    wareHouseAddress.Suburb = "Mulgrave";
                    wareHouseAddress.Postcode = "3170";
                    break;
                case 2:
                    wareHouseAddress.AddressLine1 = "Capital Transport";
                    wareHouseAddress.AddressLine2 = "Unit 3, 23-29 South St";
                    wareHouseAddress.AddressLine3 = "0288325188";
                    wareHouseAddress.Suburb = "Rydalmere";
                    wareHouseAddress.Postcode = "2116";
                    break;
                case 3:
                    wareHouseAddress.AddressLine1 = "Moxy Logistics C/O Capital Transport";
                    wareHouseAddress.AddressLine2 = "48 Paradise Rd";
                    wareHouseAddress.AddressLine3 = "0732723691";
                    wareHouseAddress.Suburb = "Acacia Ridge";
                    wareHouseAddress.Postcode = "4011";
                    break;
                case 4:
                    wareHouseAddress.AddressLine1 = "Capital Transport";
                    wareHouseAddress.AddressLine2 = "126 - 130 Richmond Road";
                    wareHouseAddress.AddressLine3 = "0883124243";
                    wareHouseAddress.Suburb = "Marleston";
                    wareHouseAddress.Postcode = "5033";
                    break;
                case 5:
                    wareHouseAddress.AddressLine1 = "Capital Transport";
                    wareHouseAddress.AddressLine2 = "959 Abernethy Road";
                    wareHouseAddress.AddressLine3 = "0893525801";
                    wareHouseAddress.Suburb = "High Wycombe";
                    wareHouseAddress.Postcode = "6057";
                    break;
            }

            return wareHouseAddress;
        }
    }
}
