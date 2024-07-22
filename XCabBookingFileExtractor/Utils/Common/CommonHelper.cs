using Core;
using Data.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XCabBookingFileExtractor.Utils.Common
{
    public class CommonHelper
    {
        public static string GetDeliveryTimeSlot(string firstTimeSlot, string firstTimeSlotRange, string secondTimeSlot, string secondTimeSlotRange, string thirdTimeSlot, string thirdTimeSlotRange)
        {
            string timeSlot = DateTime.Now.AddMinutes(30).ToShortTimeString();
            try
            {
                if (!string.IsNullOrWhiteSpace(firstTimeSlot) && DateTime.Now < Convert.ToDateTime(firstTimeSlot))
                    timeSlot = firstTimeSlotRange;
                else if (!string.IsNullOrWhiteSpace(secondTimeSlot) && DateTime.Now < Convert.ToDateTime(secondTimeSlot))
                    timeSlot = secondTimeSlotRange;
                else if (!string.IsNullOrWhiteSpace(thirdTimeSlot) && DateTime.Now < Convert.ToDateTime(thirdTimeSlot))
                    timeSlot = thirdTimeSlotRange;
            }
            catch (Exception ex)
            {
                Logger.Log(
                    $"Exception Occurred for delievry time slot. Details are : {ex.Message}", "GetDeliveryTimeSlotForEmailText");
            }
            return timeSlot;
        }

        public bool validateForMetroSuburs(string suburb, string postCode, ICollection<Suburb> metroList)
        {
            try
            {
                if (!string.IsNullOrEmpty(suburb) && string.IsNullOrEmpty(postCode))
                {
                    if (metroList.Where(x => x.Name.Trim().ToUpper() == suburb.ToUpper()).ToList().Count > 0)
                        return true;
                    else
                        return false;
                }
                else if (string.IsNullOrEmpty(suburb) && !string.IsNullOrEmpty(postCode))
                {
                    if (metroList.Where(x => x.PostCode.Trim() == postCode).ToList().Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (metroList.Where(x => x.Name.Trim().ToUpper() == suburb.ToUpper()).Where(x => x.PostCode.Trim() == postCode).ToList().Count > 0)
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception e)
            {
                Logger.Log("Exception Occurred while validating the metro suburb. Suburb : " + suburb + ", Exception:" + e.Message, "validateForMetroSuburs");
                return false;
            }

        }
    }
}
