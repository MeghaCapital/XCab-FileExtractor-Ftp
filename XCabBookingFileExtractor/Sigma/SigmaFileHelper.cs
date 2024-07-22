using Core;
using Core.Helpers;
using Data;
using Data.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XCabBookingFileExtractor.Sigma
{
	public class SigmaFileHelper : ISigmaFileHelper
	{
		private const string deliveryServiceCode = "CPOD";
		private const string returnServiceCode = "CPOR";
		private const string bookingAcceptTime = "16:30:00";
		private const string bookingCutOffTime = "16:30:00";
		private const int barcodeUpperLimit = 192;
		public ICollection<Booking> ConvertTextFile(string filePath)
		{
			var bookings = new List<Booking>();
			Booking booking = null;
			var items = new List<Item>();
			var item = new Item();
			var lineType = string.Empty;
			var lineData = string.Empty;
			var itemType = string.Empty;
			var barcode = string.Empty;
			var numDrugDependencyItems = 0;
			var bookingReference = string.Empty;
			var accountCode = string.Empty;
			var returnBookingItems = new List<Item>();
			Booking returnBooking = null;

			if (Path.GetExtension(filePath) == string.Empty)
			{
				try
				{
					var lines = File.ReadLines(filePath);
					foreach (var line in lines)
					{
						if (string.IsNullOrWhiteSpace(line.Trim()))
							continue;
						lineType = line.Trim().Substring(0, 2);
						lineData = line.Trim().Length > 2 ? (line.Substring(2, line.Length - 2)).Trim() : string.Empty;

						if (lineType == "CA")
						{
							if (booking is not null)
							{
								if (items.Count > 0)
								{
									booking.lstItems = items;
								}
								if (numDrugDependencyItems > 0)
									booking.ExtraDelInformation = numDrugDependencyItems + " Drug of dependency item";
								if (numDrugDependencyItems > 1)
								{
									booking.ExtraDelInformation += "s";
								}
								if (booking.lstItems != null && booking.lstItems.Count > 0)
								{
									var bookingsWithBarcodes = GetBookingsWithinBarcodeLimit(booking);
									if (bookingsWithBarcodes != null && bookingsWithBarcodes.Count > 0)
									{
										bookings.AddRange(bookingsWithBarcodes);
									}
								}
							}

							booking = new Booking();
							items = new List<Item>();
							numDrugDependencyItems = 0;
							booking.Ref1 = lineData;
							bookingReference = lineData;
							if (returnBooking != null && !returnBooking.Ref1.Equals(booking.Ref1))
							{
								if (returnBooking.lstItems != null && returnBooking.lstItems.Count > 0)
								{
									var bookingsWithBarcodes = GetBookingsWithinBarcodeLimit(returnBooking);
									if (bookingsWithBarcodes != null && bookingsWithBarcodes.Count > 0)
									{
										bookings.AddRange(bookingsWithBarcodes);
									}
								}
								returnBooking = null;
								returnBookingItems = new List<Item>();
							}
						}
						else if (lineType == "AN")
						{
							booking.ToDetail1 = lineData;
						}
						else if (lineType == "AL")
						{
							if (string.IsNullOrEmpty(booking.ToDetail2))
								booking.ToDetail2 = lineData;
							else if (string.IsNullOrEmpty(booking.ToSuburb))
								booking.ToSuburb = lineData;
						}
						else if (lineType == "PT")
						{
							booking.ToPostcode = lineData.Substring(0, 4);
							if (lineData.Length > 4)
								booking.ToDetail3 = lineData.Substring(4, lineData.Length - 4);
						}
						else if (lineType == "BR")
						{
							var pickupAddressDetails = GetSigmaPickupAddressDetails(lineData);
							if (pickupAddressDetails != null && pickupAddressDetails.Item2 > 0)
							{
								booking.FromDetail1 = pickupAddressDetails.Item1.AddressLine1;
								booking.FromDetail2 = pickupAddressDetails.Item1.AddressLine2;
								booking.FromSuburb = pickupAddressDetails.Item1.Suburb;
								booking.FromPostcode = pickupAddressDetails.Item1.Postcode;
								booking.StateId = pickupAddressDetails.Item2.ToString();
							}
							if (!string.IsNullOrEmpty(booking.StateId))
							{
								booking.ServiceCode = deliveryServiceCode;
								booking.DespatchDateTime = DateTime.Parse(GetAdvanceDateForBooking(booking.StateId, booking.ServiceCode));
								booking.AccountCode = GetAccountCode(booking.StateId);
								accountCode = booking.AccountCode;								
							}
							booking.Caller = lineData;
						}
						else if (lineType == "RD")
						{
							booking.Ref2 = lineData;
						}
						else if (lineType == "TN" || lineType == "CN" || lineType == "PN")
						{
							itemType = lineType;
							barcode = lineData;
						}
						else if (lineType == "FD")
						{
							if ((string.IsNullOrWhiteSpace(lineData) ? "N" : lineData) == "D")
							{
								numDrugDependencyItems += 1;
							}
							var itemCategory = GetItemCategory(itemType, (string.IsNullOrWhiteSpace(lineData) ? "N" : lineData));
							item = new Item() { Description = itemCategory, Barcode = barcode, Quantity = 1 };
							items.Add(item);
							itemType = string.Empty;
							barcode = string.Empty;
						}
						else if (lineType == "RA" && booking.Ref1.ToUpper() == bookingReference.ToUpper()) //Return booking
						{
							if (returnBooking == null)
							{
								returnBooking = new Booking();
							}
							var returnBookingItem = new Item() { Barcode = lineData, Quantity = 1 };
							returnBookingItems.Add(returnBookingItem);
							returnBooking.Ref1 = bookingReference;
							returnBooking.Ref2 = booking.Ref2;
							returnBooking.ServiceCode = returnServiceCode;
							returnBooking.AccountCode = accountCode;
							returnBooking.FromDetail1 = booking.ToDetail1;
							returnBooking.FromDetail2 = booking.ToDetail2;
							returnBooking.FromDetail3 = booking.ToDetail3;
							returnBooking.FromDetail4 = booking.ToDetail4;
							returnBooking.FromDetail5 = booking.ToDetail5;
							returnBooking.FromSuburb = booking.ToSuburb;
							returnBooking.FromPostcode = booking.ToPostcode;
							returnBooking.ToDetail1 = booking.FromDetail1;
							returnBooking.ToDetail2 = booking.FromDetail2;
							returnBooking.ToDetail3 = booking.FromDetail3;
							returnBooking.ToDetail4 = booking.FromDetail4;
							returnBooking.ToDetail5 = booking.FromDetail5;
							returnBooking.ToSuburb = booking.FromSuburb;
							returnBooking.ToPostcode = booking.FromPostcode;
							returnBooking.StateId = booking.StateId;
							returnBooking.Caller = booking.Caller;
							returnBooking.DespatchDateTime = DateTime.Parse(GetAdvanceDateForBooking(booking.StateId, returnBooking.ServiceCode));

							if (returnBooking != null && returnBooking.Ref1 == bookingReference)
							{
								returnBooking.lstItems = returnBookingItems;
							}
						}
					}

					if (booking is not null)
					{
						if (items.Count > 0)
						{
							booking.lstItems = items;
						}
						if (numDrugDependencyItems > 0)
							booking.ExtraDelInformation = numDrugDependencyItems + " Drug of dependency item(s)";
						if (booking.lstItems != null && booking.lstItems.Count > 0)
						{
							var bookingsWithBarcodes = GetBookingsWithinBarcodeLimit(booking);
							if (bookingsWithBarcodes != null && bookingsWithBarcodes.Count > 0)
							{
								bookings.AddRange(bookingsWithBarcodes);
							}
						}

						if (returnBooking != null && returnBooking.Ref1.Contains(booking.Ref1))
						{
							if (returnBooking.lstItems != null && returnBooking.lstItems.Count > 0)
							{
								var bookingsWithBarcodes = GetBookingsWithinBarcodeLimit(returnBooking);
								if (bookingsWithBarcodes != null && bookingsWithBarcodes.Count > 0)
								{
									bookings.AddRange(bookingsWithBarcodes);
								}
							}
							returnBooking = null;
						}
					}
				}
				catch (Exception e)
				{
					Logger.Log(
					$"Exception Occurred while reading file contents for Sigma, Exception: {e.Message}", "SigmaFileHelper");
				}
			}

			return bookings;
		}

		private static Tuple<AddressDetail, int> GetSigmaPickupAddressDetails(string depotCode)
		{
			var sigmaAddress = new AddressDetail();
			var stateId = 0;

			switch (depotCode.Trim())
			{
				case "201":
					sigmaAddress.AddressLine1 = "Sigma Kemps Creek";
					sigmaAddress.AddressLine2 = "2 Imperata Close";
					sigmaAddress.Suburb = "Kemps Creek";
					sigmaAddress.Postcode = "2178";
					stateId = 2;
					break;
				case "401":
					sigmaAddress.AddressLine1 = "Sigma Berrinba";
					sigmaAddress.AddressLine2 = "101 Wayne Goss Drive";
					sigmaAddress.Suburb = "Berrinba";
					sigmaAddress.Postcode = "4117";
					stateId = 3;
					break;
				case "501":
					sigmaAddress.AddressLine1 = "Sigma Pooraka";
					sigmaAddress.AddressLine2 = "35 Burma Road";
					sigmaAddress.Suburb = "Pooraka";
					sigmaAddress.Postcode = "5095";
					stateId = 4;
					break;
				case "601":
					sigmaAddress.AddressLine1 = "Sigma Canning Vale";
					sigmaAddress.AddressLine2 = "10 Craft Street";
					sigmaAddress.Suburb = "Canning Vale";
					sigmaAddress.Postcode = "6155";
					stateId = 5;
					break;
				case "801":
					sigmaAddress.AddressLine1 = "Sigma Winnellie";
					sigmaAddress.AddressLine2 = "115 Coonawarra Rd";
					sigmaAddress.Suburb = "Winnellie";
					sigmaAddress.Postcode = "820";
					stateId = 9;
					break;
				case "302":
					sigmaAddress.AddressLine1 = "Sigma Truganina";
					sigmaAddress.AddressLine2 = "580-610 Dohertys Rd";
					sigmaAddress.Suburb = "Truganina";
					sigmaAddress.Postcode = "3029";
					stateId = 1;
					break;
				default:
					throw new Exception("Unexpected Case");
			}

			return Tuple.Create(sigmaAddress, stateId);
		}

		private static string GetItemCategory(string itemType, string containerType)
		{
			var itemCategory = string.Empty;

			if (!string.IsNullOrWhiteSpace(itemType))
			{
				if (containerType == "N" && itemType == "TN")
				{
					itemCategory = "Tote";
				}
				else if (containerType == "N" && itemType == "CN")
				{
					itemCategory = "Case";
				}
				else if (containerType == "F" && itemType == "TN")
				{
					itemCategory = "Tote-Fridge";
				}
				else if (containerType == "F" && itemType == "CN")
				{
					itemCategory = "Case-Fridge";
				}
				else if (containerType == "D" && itemType == "TN")
				{
					itemCategory = "Tote-Drug of dependency";
				}
				else if (containerType == "D" && itemType == "CN")
				{
					itemCategory = "Case-Drug of dependency";
				}
				else if (containerType == "P" && itemType == "PN")
				{
					itemCategory = "Pallet";
				}
			}

			return itemCategory;
		}

		private static string GetAccountCode(string stateId)
		{
			var accountCode = string.Empty;
			switch (stateId)
			{
				case "1":
					accountCode = "KMSIGTRU";
					break;
				case "2":
					accountCode = "KSSIGM";
					break;
				case "3":
					accountCode = "KBSIGMA";
					break;
				case "4":
					accountCode = "KASIGPHA";
					break;
				case "5":
					accountCode = "KPSIGPRM";
					break;
				case "9":
					accountCode = "KDSIGMA";
					break;
				default:
					throw new Exception("Unexpected Case");
			}

			return accountCode;
		}

		public static string GetAdvanceDateForBooking(string state, string serviceCode)
		{
			var despatchDate = string.Empty;
			if (!string.IsNullOrEmpty(state))
			{
				var localDateTimeForState = Core.Helpers.DateTimeHelpers.GetLocalDateTimeFromUtc(Convert.ToInt32(state), DateTime.UtcNow);
				if (localDateTimeForState > DateTime.MinValue)
				{
					var localDate = DateOnly.FromDateTime(localDateTimeForState);
					var localTime = TimeSpan.Parse(localDateTimeForState.ToString("HH:mm:ss"));
					var cutOffTime = TimeSpan.Parse(bookingCutOffTime);
					var acceptTime = TimeSpan.Parse(bookingAcceptTime);
					var dayOfWeek = Core.Helpers.DateTimeHelpers.IsAWeekDay(localDate.ToString("dd/MM/yyyy"));
					var isASameDayDelivery = false;
					if (localTime < acceptTime && localTime < cutOffTime)
					{
						isASameDayDelivery = true;
					}

					if (serviceCode == deliveryServiceCode)
					{
						if (dayOfWeek != DayOfWeek.Sunday && isASameDayDelivery)
						{
							despatchDate = new CalculateDates().GetNextWorkingDayInclusiveSaturday(localDateTimeForState.AddDays(-1), 1, StateHelpers.GetStateAbbrev(state), true).ToString("dd/MM/yyyy");
						}
						else
						{
							despatchDate = new CalculateDates().GetNextWorkingDayInclusiveSaturday(localDateTimeForState, 1, StateHelpers.GetStateAbbrev(state), true).ToString("dd/MM/yyyy");
						}
					}
					else
					{

						if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday || isASameDayDelivery)
						{
							despatchDate = new CalculateDates().GetNextWorkingDay(localDateTimeForState, 0, StateHelpers.GetStateAbbrev(state)).ToString("dd/MM/yyyy");
						}
						else if (!isASameDayDelivery)
						{
							despatchDate = new CalculateDates().GetNextWorkingDay(localDateTimeForState, 1, StateHelpers.GetStateAbbrev(state)).ToString("dd/MM/yyyy");
						}
					}
				}
			}
			return despatchDate;
		}

		private static List<Booking> GetBookingsWithinBarcodeLimit(Booking booking)
		{
			if (booking.lstItems != null && booking.lstItems.Count > 0)
			{
				var bookings = new List<Booking>();
				var barcodeGroups = booking.lstItems.Select((x, i) => new { Index = i, Value = x })
									.GroupBy(x => x.Index / barcodeUpperLimit)
									.Select(x => x.Select(v => v.Value).ToList());
				if (barcodeGroups != null)
				{
					foreach (var barcodeGroup in barcodeGroups)
					{
						var splitBooking = new Booking
						{
							AccountCode = booking.AccountCode,
							FromDetail1 = booking.FromDetail1,
							FromDetail2 = booking.FromDetail2,
							FromDetail3 = booking.FromDetail3,
							FromSuburb = booking.FromSuburb,
							FromPostcode = booking.FromPostcode,
							Ref1 = booking.Ref1,
							Ref2 = booking.Ref2,
							ToDetail1 = booking.ToDetail1,
							ToDetail2 = booking.ToDetail2,
							ToDetail3 = booking.ToDetail3,
							ToSuburb = booking.ToSuburb,
							ToPostcode = booking.ToPostcode,
							StateId = booking.StateId,
							ServiceCode = booking.ServiceCode,
							ExtraDelInformation = booking.ExtraDelInformation,
							DespatchDateTime = booking.DespatchDateTime,
							lstContactDetail = booking.lstContactDetail,
							ATL = booking.ATL,
							OkToUpload = false,
							Caller = booking.Caller,
							lstItems = barcodeGroup
						};
						bookings.Add(splitBooking);
					}
				}
				return bookings;
			}
			return null;
		}
	}
}
