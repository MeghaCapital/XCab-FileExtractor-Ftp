using Data.Entities;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils.Helpers
{
	public abstract class CleanupExtraCharatersHelper
	{
        private const int maxCharacterLength = 50;
        public static XCabBooking GetCleansedXCabBooking (XCabBooking xCabBooking)
		{
            if (!string.IsNullOrEmpty(xCabBooking.Ref1) && xCabBooking.Ref1.Length > maxCharacterLength)
            {
                xCabBooking.Ref1 = GetSubstring(xCabBooking.Ref1);
            }
            if (!string.IsNullOrEmpty(xCabBooking.Ref2) && xCabBooking.Ref2.Length > maxCharacterLength)
            {
                xCabBooking.Ref2 = GetSubstring(xCabBooking.Ref2);
            }
            if (!string.IsNullOrEmpty(xCabBooking.ConsignmentNumber) && xCabBooking.ConsignmentNumber.Length > maxCharacterLength)
            {
                xCabBooking.ConsignmentNumber = GetSubstring(xCabBooking.ConsignmentNumber);
            }
            if (!string.IsNullOrEmpty(xCabBooking.Caller) && xCabBooking.Caller.Length > maxCharacterLength)
            {
                xCabBooking.Caller = GetSubstring(xCabBooking.Caller);
            }
            if (!string.IsNullOrEmpty(xCabBooking.PickupArriveLocation) && xCabBooking.PickupArriveLocation.Length > maxCharacterLength)
            {
                xCabBooking.PickupArriveLocation = GetSubstring(xCabBooking.PickupArriveLocation);
            }
            if (!string.IsNullOrEmpty(xCabBooking.PickupCompleteLocation) && xCabBooking.PickupCompleteLocation.Length > maxCharacterLength)
            {
                xCabBooking.PickupCompleteLocation = GetSubstring(xCabBooking.PickupCompleteLocation);
            }
            if (!string.IsNullOrEmpty(xCabBooking.DeliveryArriveLocation) && xCabBooking.DeliveryArriveLocation.Length > maxCharacterLength)
            {
                xCabBooking.DeliveryArriveLocation = GetSubstring(xCabBooking.DeliveryArriveLocation);
            }
            if (!string.IsNullOrEmpty(xCabBooking.DeliveryCompleteLocation) && xCabBooking.DeliveryCompleteLocation.Length > maxCharacterLength)
            {
                xCabBooking.DeliveryCompleteLocation = GetSubstring(xCabBooking.DeliveryCompleteLocation);
            }

            if (xCabBooking.lstItems != null && xCabBooking.lstItems.Count > 0)
            {
                var cleansedItem = new Core.Item();
                foreach (var item in xCabBooking.lstItems)
                {
                    if (!string.IsNullOrEmpty(item.Description) && item.Description.Length > maxCharacterLength)
                    {
                        cleansedItem.Description = GetSubstring(item.Description);
                        item.Description = cleansedItem.Description;
                    }
                    if (!string.IsNullOrEmpty(item.Barcode) && item.Barcode.Length > maxCharacterLength)
                    {
                        cleansedItem.Barcode = GetSubstring(item.Barcode);
                        item.Barcode = cleansedItem.Barcode;
                    }
                }
            }

            if (xCabBooking.lstContactDetail != null && xCabBooking.lstContactDetail.Count > 0)
            {
                var cleansedDetail = new Core.ContactDetail();
                foreach (var detail in xCabBooking.lstContactDetail)
                {
                    if (!string.IsNullOrEmpty(detail.PhoneNumber) && detail.PhoneNumber.Length > maxCharacterLength)
                    {
                        cleansedDetail.PhoneNumber = GetSubstring(detail.PhoneNumber);
                        detail.PhoneNumber = cleansedDetail.PhoneNumber;
                    }
                }
            }

            return xCabBooking;
		}

        public static XCabAsnBooking GetCleansedXCabAsnBooking(XCabAsnBooking xCabAsnBooking)
        {
            if (!string.IsNullOrEmpty(xCabAsnBooking.Ref1) && xCabAsnBooking.Ref1.Length > maxCharacterLength)
            {
                xCabAsnBooking.Ref1 = GetSubstring(xCabAsnBooking.Ref1);
            }
            if (!string.IsNullOrEmpty(xCabAsnBooking.Ref2) && xCabAsnBooking.Ref2.Length > maxCharacterLength)
            {
                xCabAsnBooking.Ref2 = GetSubstring(xCabAsnBooking.Ref2);
            }
            if (!string.IsNullOrEmpty(xCabAsnBooking.ConsignmentNumber) && xCabAsnBooking.ConsignmentNumber.Length > maxCharacterLength)
            {
                xCabAsnBooking.ConsignmentNumber = GetSubstring(xCabAsnBooking.ConsignmentNumber);
            }
            if (!string.IsNullOrEmpty(xCabAsnBooking.Caller) && xCabAsnBooking.Caller.Length > maxCharacterLength)
            {
                xCabAsnBooking.Caller = GetSubstring(xCabAsnBooking.Caller);
            }
            if (!string.IsNullOrEmpty(xCabAsnBooking.PickupArriveLocation) && xCabAsnBooking.PickupArriveLocation.Length > maxCharacterLength)
            {
                xCabAsnBooking.PickupArriveLocation = GetSubstring(xCabAsnBooking.PickupArriveLocation);
            }
            if (!string.IsNullOrEmpty(xCabAsnBooking.PickupCompleteLocation) && xCabAsnBooking.PickupCompleteLocation.Length > maxCharacterLength)
            {
                xCabAsnBooking.PickupCompleteLocation = GetSubstring(xCabAsnBooking.PickupCompleteLocation);
            }
            if (!string.IsNullOrEmpty(xCabAsnBooking.DeliveryArriveLocation) && xCabAsnBooking.DeliveryArriveLocation.Length > maxCharacterLength)
            {
                xCabAsnBooking.DeliveryArriveLocation = GetSubstring(xCabAsnBooking.DeliveryArriveLocation);
            }
            if (!string.IsNullOrEmpty(xCabAsnBooking.DeliveryCompleteLocation) && xCabAsnBooking.DeliveryCompleteLocation.Length > maxCharacterLength)
            {
                xCabAsnBooking.DeliveryCompleteLocation = GetSubstring(xCabAsnBooking.DeliveryCompleteLocation);
            }

            if (xCabAsnBooking.lstItems != null && xCabAsnBooking.lstItems.Count > 0)
            {
                var cleansedItem = new Core.Item();
                foreach (var item in xCabAsnBooking.lstItems)
                {
                    if (!string.IsNullOrEmpty(item.Description) && item.Description.Length > maxCharacterLength)
                    {
                        cleansedItem.Description = GetSubstring(item.Description);
                        item.Description = cleansedItem.Description;
                    }
                    if (!string.IsNullOrEmpty(item.Barcode) && item.Barcode.Length > maxCharacterLength)
                    {
                        cleansedItem.Barcode = GetSubstring(item.Barcode);
                        item.Barcode = cleansedItem.Barcode;
                    }
                }
            }

            if (xCabAsnBooking.lstContactDetail != null && xCabAsnBooking.lstContactDetail.Count > 0)
            {
                var cleansedDetail = new Core.ContactDetail();
                foreach (var detail in xCabAsnBooking.lstContactDetail)
                {
                    if (!string.IsNullOrEmpty(detail.PhoneNumber) && detail.PhoneNumber.Length > maxCharacterLength)
                    {
                        cleansedDetail.PhoneNumber = GetSubstring(detail.PhoneNumber);
                        detail.PhoneNumber = cleansedDetail.PhoneNumber;
                    }
                }
            }

            return xCabAsnBooking;
        }

        public static string GetSubstring(string stringValue)
		{
			stringValue = stringValue.Substring(0, maxCharacterLength);
			return stringValue;
		}
	}
}
