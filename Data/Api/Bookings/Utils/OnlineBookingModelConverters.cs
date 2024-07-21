using Core;
using Data.Api.Bookings.Tms;
using System.Text;
using System.Text.RegularExpressions;

namespace Data.Api.Bookings.Utils
{
	public abstract class OnlineBookingModelConverters
	{
		private const string KeyOnHold = "ONHOLD";
		private const string ConsolidatedReferencesPair = "REFERENCE1||REFERENCE2";
		private const string DgDefaultLinePackagingInstr = "Packaged in accordance with the latest DG code. Make sure you have DG docs placed in your door holder.";
		private const string DgDefaultLineGoodsInstr = "Goods to be secured inside gates or sides with no more than 30% of the highest layer above top of sides.";
		private const string RegextForReplace = @"[a-zA-Z]+";
		private const string RegextForReplaceWith = "";
		private const int characterLimit = 15;

		public static async Task<TmsBookingRequest> GetTmsBookingRequestFromOnlineBooking(BookingModel.OnlineBooking onlineBooking)
		{
			var tmsBookingRequest = new TmsBookingRequest();
			try
			{
				tmsBookingRequest.AccountCode = onlineBooking.AccountCode;
				tmsBookingRequest.AdvanceDateTime = onlineBooking.AdvanceDateTime != null ? onlineBooking.AdvanceDateTime.Value : DateTime.MinValue;
				tmsBookingRequest.Ref1 = onlineBooking.Reference1;
				tmsBookingRequest.Ref2 = onlineBooking.Reference2;
				tmsBookingRequest.ConsignmentNumber = onlineBooking.ConsignmentNumber;
				tmsBookingRequest.DespatchDateTime = onlineBooking.AdvanceDateTime.HasValue && onlineBooking.AdvanceDateTime != DateTime.MinValue ? onlineBooking.AdvanceDateTime.Value : DateTime.Now;
				tmsBookingRequest.FromAddressDetail = new Data.AddressDetail();
				tmsBookingRequest.ServiceCode = onlineBooking.ServiceCode.Trim();
				tmsBookingRequest.TotalVolume = onlineBooking.TotalVolume;
				if (onlineBooking.State == Data.Api.Bookings.BookingModel.State.Nt)
                {
					tmsBookingRequest.IsNtJob = true;					
                }
                else
                {
					tmsBookingRequest.IsNtJob = false;
                }
				tmsBookingRequest.StateId = Core.Helpers.StateHelpers.GetTplusStateId(onlineBooking.State.ToString());
				tmsBookingRequest.FromAddressDetail.AddressLine1 = onlineBooking.FromDetail1;
				tmsBookingRequest.FromAddressDetail.AddressLine2 = onlineBooking.FromDetail2;
				tmsBookingRequest.FromAddressDetail.AddressLine3 = onlineBooking.FromDetail3;
				tmsBookingRequest.FromAddressDetail.AddressLine4 = onlineBooking.FromDetail4;
				tmsBookingRequest.FromAddressDetail.AddressLine5 = onlineBooking.FromDetail5;
				tmsBookingRequest.FromAddressDetail.Suburb = onlineBooking.FromSuburb;
				tmsBookingRequest.FromAddressDetail.Postcode = string.IsNullOrWhiteSpace(onlineBooking.FromPostcode)? string.Empty:onlineBooking.FromPostcode;
				tmsBookingRequest.Caller = onlineBooking.Runcode != null ? onlineBooking.Runcode.Trim() : onlineBooking.Caller;
				tmsBookingRequest.PreAllocatedDriverNumber = onlineBooking.DriverNumber > 0 ? onlineBooking.DriverNumber.ToString() : string.Empty;
				tmsBookingRequest.Username = onlineBooking.UserCredentials.Username;

				tmsBookingRequest.ToAddressDetail = new Data.AddressDetail
				{
					AddressLine1 = onlineBooking.ToDetail1,
					AddressLine2 = onlineBooking.ToDetail2,
					AddressLine3 = onlineBooking.ToDetail3,
					AddressLine4 = onlineBooking.ToDetail4,
					AddressLine5 = onlineBooking.ToDetail5,
					Suburb = onlineBooking.ToSuburb,
					Postcode = string.IsNullOrWhiteSpace(onlineBooking.ToPostcode) ? string.Empty : onlineBooking.ToPostcode
				};

				try
				{
					if (string.IsNullOrEmpty(onlineBooking.TotalWeight) || double.Parse(onlineBooking.TotalWeight) == 0)
					{
						if (onlineBooking.BookingItems != null && onlineBooking.BookingItems.Count > 0)
						{
							tmsBookingRequest.TotalWeight = onlineBooking.BookingItems.Sum(x => x.Weight).ToString();
						}
					}
					else
					{
						tmsBookingRequest.TotalWeight = onlineBooking?.TotalWeight;
					}
				}
				catch (Exception e)
				{
					await Logger.Log($"Exception occurred while extracting total weight of booking for account:{onlineBooking.AccountCode}, Ref1:{onlineBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
				}

				tmsBookingRequest.BookingExtras = new TmsBookingExtras();
				if (!string.IsNullOrEmpty(onlineBooking.ExtraPuInformation) || !string.IsNullOrEmpty(onlineBooking.ExtraDelInformation))
				{
					if (!string.IsNullOrEmpty(onlineBooking.ExtraPuInformation))
					{
						tmsBookingRequest.BookingExtras.PickupInstructions = onlineBooking.ExtraPuInformation;
					}
					if (!string.IsNullOrEmpty(onlineBooking.ExtraDelInformation))
					{
						tmsBookingRequest.BookingExtras.DeliveryInstructions = onlineBooking.ExtraDelInformation;
					}
					if (onlineBooking.ATLInstructions != null)
					{
						tmsBookingRequest.BookingExtras.DeliveryInstructions = tmsBookingRequest.BookingExtras.DeliveryInstructions != null ? tmsBookingRequest.BookingExtras.DeliveryInstructions + ". " + onlineBooking.ATLInstructions : onlineBooking.ATLInstructions;
					}
				}

				if (!string.IsNullOrEmpty(onlineBooking.TrackAndTraceEmailAddress) || !string.IsNullOrEmpty(onlineBooking.TrackAndTraceSmsNumber))
				{
					if (!string.IsNullOrEmpty(onlineBooking.TrackAndTraceEmailAddress))
					{
						tmsBookingRequest.BookingExtras.TrackAndTraceEmailAddress = onlineBooking.TrackAndTraceEmailAddress;
					}
					if (!string.IsNullOrEmpty(onlineBooking.TrackAndTraceSmsNumber))
					{
						tmsBookingRequest.BookingExtras.TrackAndTraceSmsNumber = onlineBooking.TrackAndTraceSmsNumber.Trim().Replace(" ", "");
					}
				}

				if (!string.IsNullOrWhiteSpace(onlineBooking.ExtraDelInformation))
				{
					if (onlineBooking.ExtraDelInformation.ToUpper().Contains("NO ATL"))
					{
						tmsBookingRequest.BookingExtras.Atl = false;
					}
					//check if it contains ATL
					else if (onlineBooking.ExtraDelInformation.ToUpper().Contains("ATL"))
					{
						tmsBookingRequest.BookingExtras.Atl = true;
					}
					else if (onlineBooking.ExtraDelInformation.ToUpper().Contains("NSR"))
					{
						tmsBookingRequest.BookingExtras.Atl = true;
					}
					else if (onlineBooking.ExtraDelInformation.ToUpper().Contains("ATL/NSR"))
					{
						tmsBookingRequest.BookingExtras.Atl = true;
					}
				}

				if (onlineBooking.ATL != null && (bool)onlineBooking.ATL)
				{
					tmsBookingRequest.BookingExtras.Atl = true;
				}
				else
				{
					tmsBookingRequest.BookingExtras.Atl = false;
				}

				var totalItemsQuantity = 0;
				if (onlineBooking.BookingItems != null && onlineBooking.BookingItems.Count > 0)
				{
					tmsBookingRequest.Items = new List<TmsRequestBookingItem>();
					if (onlineBooking.BookingItems != null)
					{
						foreach (var item in onlineBooking.BookingItems)
						{
							var tmsBookingItem = new TmsRequestBookingItem
							{
								Length = !string.IsNullOrEmpty(item.Length.ToString()) ? item.Length : item.Length.GetValueOrDefault(),
								Width = !string.IsNullOrEmpty(item.Width.ToString()) ? item.Width : item.Width.GetValueOrDefault(),
								Height = !string.IsNullOrEmpty(item.Height.ToString()) ? item.Height : item.Height.GetValueOrDefault(),
								Quantity = item.Quantity > 0 ? item.Quantity : item.Quantity.GetValueOrDefault(),
								Cubic = !string.IsNullOrEmpty(item.Volume.ToString()) ? item.Volume : item.Volume.GetValueOrDefault(),
								Weight = !string.IsNullOrEmpty(item.Weight.ToString()) ? item.Weight : item.Weight.GetValueOrDefault(),
								Barcode = item.Barcode == null ? "" : item.Barcode,
								Description = item.Description
							};
							tmsBookingRequest.Items.Add(tmsBookingItem);
							totalItemsQuantity += (int)tmsBookingItem.Quantity;
						}
					}

					if (onlineBooking.BookingItems.Any())
					{
						try
						{
							foreach (var item in onlineBooking.BookingItems)
							{
								if (item.Length > 0 || item.Width > 0 || item.Height > 0 || !string.IsNullOrWhiteSpace(item.Description) || item.Quantity > 0 || item.Volume > 0)
								{
									var itemDimension = $"{item.Description} [L={item.Length},W={item.Width},H={item.Height},Kg={item.Weight},m3={item.Volume},Qty={item.Quantity}]";
									if (tmsBookingRequest.Remarks == null)
									{
										tmsBookingRequest.Remarks = new List<TmsBookingRemarks>();
									}
									tmsBookingRequest.Remarks.Add(new TmsBookingRemarks { RemarkText = (itemDimension) });
								}
							}
						}
						catch (Exception e)
						{
							await Logger.Log($"Exception occurred while mapping item details to remarks of booking for account:{onlineBooking.AccountCode}, Ref1:{onlineBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
						}
					}
				}

				if (!string.IsNullOrWhiteSpace(onlineBooking.TotalItems))
				{
					if (onlineBooking.UserCredentials.Username.ToUpper() == "ALLFASTENERS")
					{
						tmsBookingRequest.TotalItems = Regex.Replace(onlineBooking.TotalItems, RegextForReplace, RegextForReplaceWith);
					}
					else
					{
						tmsBookingRequest.TotalItems = Convert.ToInt32(Math.Floor(Convert.ToDouble(onlineBooking.TotalItems))).ToString();
					}
				}
				else if (totalItemsQuantity > 0)
				{
					tmsBookingRequest.TotalItems = totalItemsQuantity.ToString();
				} 
				else 
				{
					tmsBookingRequest.TotalItems = tmsBookingRequest.Items != null ? tmsBookingRequest.Items.Count().ToString() : "0";
				}

				if (onlineBooking.BookingContactInformation != null && (onlineBooking.BookingContactInformation.Name != null || onlineBooking.BookingContactInformation.PhoneNumbers != null))
				{
					try
					{
						tmsBookingRequest.BookingContactInformation = new TmsBookingContactInformation();
						if (onlineBooking.BookingContactInformation.PhoneNumbers != null)
						{							
							tmsBookingRequest.BookingContactInformation.PhoneNumbers = new List<string>();
							foreach (var phoneNumber in onlineBooking.BookingContactInformation.PhoneNumbers)
							{
								tmsBookingRequest.BookingContactInformation.PhoneNumbers.Add(phoneNumber);
							}
						}

						var contactDetail = string.Empty;
						var contactName = string.Empty;
						if (!string.IsNullOrWhiteSpace(onlineBooking.BookingContactInformation.Name))
						{
							tmsBookingRequest.BookingContactInformation.Name = onlineBooking.BookingContactInformation.Name;
							contactName = onlineBooking.BookingContactInformation.Name.Split(' ').Length > 1 ? onlineBooking.BookingContactInformation.Name.Split(' ')[0] : onlineBooking.BookingContactInformation.Name;

							if(contactName.Length > characterLimit)
							{
								contactName = contactName.Substring(0, 15);
							}													
						}

						if (tmsBookingRequest.BookingContactInformation.PhoneNumbers != null && tmsBookingRequest.BookingContactInformation.PhoneNumbers.Any())
						{
							contactDetail = (contactName + " " + tmsBookingRequest.BookingContactInformation.PhoneNumbers[0]).Trim();

							if (string.IsNullOrEmpty(tmsBookingRequest.ToAddressDetail.AddressLine3))
							{
								tmsBookingRequest.ToAddressDetail.AddressLine3 = contactDetail;
							}
							else if (string.IsNullOrEmpty(tmsBookingRequest.ToAddressDetail.AddressLine4))
							{
								tmsBookingRequest.ToAddressDetail.AddressLine4 = contactDetail;
							}
							else if (string.IsNullOrEmpty(tmsBookingRequest.BookingExtras.DeliveryInstructions))
							{
								tmsBookingRequest.BookingExtras.DeliveryInstructions = contactDetail;
							}
							else
							{
								//TODO: Should we concatenate phone details into extra Del information
							}
						}
					}
					catch (Exception e)
					{
						await Logger.Log($"Exception occurred while extracting contact details of booking for account:{onlineBooking.AccountCode}, Ref1:{onlineBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
					}
				}

				//check for any custom fields
				try
				{
					if (onlineBooking.CustomFields != null && onlineBooking.CustomFields.Count > 0)
					{
						foreach (CustomFields cf in onlineBooking.CustomFields)
						{
							if (onlineBooking.Reference1.ToUpper().Contains("INVOICES") && cf.Key.ToUpper().Equals(ConsolidatedReferencesPair))
							{
								if (tmsBookingRequest.CustomFields == null)
								{
									tmsBookingRequest.CustomFields = new List<CustomFields>();
								}
								tmsBookingRequest.CustomFields.Add(cf);

								if (tmsBookingRequest.Remarks == null)
								{
									tmsBookingRequest.Remarks = new List<TmsBookingRemarks>();
								}
								tmsBookingRequest.Remarks.Add(new TmsBookingRemarks { RemarkText = (cf.Value).Replace("||", ",") });
							}
							else if (cf.Key.ToUpper().Equals(KeyOnHold))
							{
								if (cf.Value.Equals("1"))
									tmsBookingRequest.OnHoldBooking = true;
							}
						}								
					}
				}
				catch (Exception e)
				{
					await Logger.Log($"Exception occurred when reading custom fileds of booking for account:{onlineBooking.AccountCode}, Ref1:{onlineBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
				}

				return tmsBookingRequest;
			}
			catch (Exception e)
			{
				await Logger.Log($"Exception occurred in GetTmsBookingRequest method. Username:{onlineBooking.UserCredentials.Username}, Ref1:{onlineBooking.Reference1}, Ref2:{onlineBooking.Reference2}. Error Message:{e.Message}", nameof(OnlineBookingModelConverters));
			}
			return tmsBookingRequest;
		}

		public static async Task<TmsBookingRequest> GetTmsBookingRequestFromDGBooking(DgBooking dgBooking)
		{
			var tmsBookingRequest = new TmsBookingRequest();
			try
			{
				tmsBookingRequest.AccountCode = dgBooking.AccountCode;
				tmsBookingRequest.AdvanceDateTime = dgBooking.AdvanceDateTime != null ? dgBooking.AdvanceDateTime.Value : DateTime.MinValue;
				tmsBookingRequest.Ref1 = dgBooking.Reference1;
				tmsBookingRequest.Ref2 = dgBooking.Reference2;
				tmsBookingRequest.ConsignmentNumber = dgBooking.ConsignmentNumber;
				tmsBookingRequest.DespatchDateTime = dgBooking.AdvanceDateTime.HasValue && dgBooking.AdvanceDateTime != DateTime.MinValue ? dgBooking.AdvanceDateTime.Value : DateTime.Now;
				tmsBookingRequest.FromAddressDetail = new Data.AddressDetail();
				tmsBookingRequest.ServiceCode = dgBooking.ServiceCode.Trim();
				tmsBookingRequest.TotalVolume = dgBooking.TotalVolume;
				tmsBookingRequest.StateId = Core.Helpers.StateHelpers.GetStateId(dgBooking.State.ToString());
				tmsBookingRequest.FromAddressDetail.AddressLine1 = dgBooking.FromDetail1;
				tmsBookingRequest.FromAddressDetail.AddressLine2 = dgBooking.FromDetail2;
				tmsBookingRequest.FromAddressDetail.AddressLine3 = dgBooking.FromDetail3;
				tmsBookingRequest.FromAddressDetail.AddressLine4 = dgBooking.FromDetail4;
				tmsBookingRequest.FromAddressDetail.AddressLine5 = dgBooking.FromDetail5;
				tmsBookingRequest.FromAddressDetail.Suburb = dgBooking.FromSuburb;
				tmsBookingRequest.FromAddressDetail.Postcode = dgBooking.FromPostcode;
				tmsBookingRequest.Caller = dgBooking.Runcode != null ? dgBooking.Runcode.Trim() : null;
				tmsBookingRequest.PreAllocatedDriverNumber = dgBooking.DriverNumber > 0 ? dgBooking.DriverNumber.ToString() : string.Empty;
				tmsBookingRequest.Username = dgBooking.UserCredentials.Username;
				tmsBookingRequest.OnHoldBooking = true;

				tmsBookingRequest.ToAddressDetail = new Data.AddressDetail
				{
					AddressLine1 = dgBooking.ToDetail1,
					AddressLine2 = dgBooking.ToDetail2,
					AddressLine3 = dgBooking.ToDetail3,
					AddressLine4 = dgBooking.ToDetail4,
					AddressLine5 = dgBooking.ToDetail5,
					Suburb = dgBooking.ToSuburb,
					Postcode = dgBooking.ToPostcode,
				};

				try
				{
					if (string.IsNullOrEmpty(dgBooking.TotalWeight) || double.Parse(dgBooking.TotalWeight) == 0)
					{
						if (dgBooking.BookingItems != null && dgBooking.BookingItems.Count > 0)
						{
							tmsBookingRequest.TotalWeight = dgBooking.BookingItems.Sum(x => x.Weight).ToString();
						}
					}
					else
					{
						tmsBookingRequest.TotalWeight = dgBooking?.TotalWeight;
					}
				}
				catch (Exception e)
				{
					await Logger.Log($"Exception occurred while extracting total weightof booking for account:{dgBooking.AccountCode}, Ref1:{dgBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
				}

				tmsBookingRequest.BookingExtras = new TmsBookingExtras();
				if (dgBooking.ExtraPuInformation != null || dgBooking.ExtraDelInformation != null ||
					dgBooking.SpecialInstructions != null ||
					!string.IsNullOrWhiteSpace(dgBooking.TrackAndTraceSmsNumber))
				{
					if (!string.IsNullOrWhiteSpace(dgBooking.ExtraPuInformation))
					{
						tmsBookingRequest.BookingExtras.PickupInstructions = dgBooking.ExtraPuInformation;
					}
					if (!string.IsNullOrWhiteSpace(dgBooking.ExtraDelInformation))
					{
						tmsBookingRequest.BookingExtras.DeliveryInstructions = dgBooking.ExtraDelInformation;
					}
					if (!string.IsNullOrWhiteSpace(dgBooking.SpecialInstructions))
					{
						tmsBookingRequest.BookingExtras.SpecialInstructions = dgBooking.SpecialInstructions;
					}
					//added SMS On PUP field to be populated in TMS so that it can also be used by UNS
					if (!string.IsNullOrWhiteSpace(dgBooking.TrackAndTraceSmsNumber))
					{
						tmsBookingRequest.BookingExtras.PickupSms = dgBooking.TrackAndTraceSmsNumber.Trim().Replace(" ", "");
					}
				}

				if (dgBooking.BookingItems != null)
				{
					//check if there is only one item provided
					if (dgBooking.BookingItems.Count == 1)
					{
						try
						{
							if (dgBooking.BookingItems[0].Length > 0 && dgBooking.BookingItems[0].Width > 0
								&& dgBooking.BookingItems[0].Height > 0)
							{
								var itemDimension = "L=" + dgBooking.BookingItems[0].Length + " W=" + dgBooking.BookingItems[0].Width + " H=" + dgBooking.BookingItems[0].Height;
								if (string.IsNullOrEmpty(dgBooking.ToDetail3))
								{
									tmsBookingRequest.FromAddressDetail.AddressLine3 = itemDimension;
								}
								else if (string.IsNullOrEmpty(dgBooking.ToDetail4))
								{
									tmsBookingRequest.FromAddressDetail.AddressLine4 = itemDimension;
								}
								else
								{
									if (tmsBookingRequest.BookingExtras != null)
									{
										if (!string.IsNullOrEmpty(tmsBookingRequest.BookingExtras.PickupInstructions))
										{
											tmsBookingRequest.BookingExtras.PickupInstructions += " " + itemDimension;
										}
										else
										{
											tmsBookingRequest.BookingExtras.PickupInstructions = itemDimension;
										}
									}
									else
									{
										tmsBookingRequest.BookingExtras = new TmsBookingExtras
										{
											PickupInstructions = itemDimension
										};
									}
								}
							}
						}
						catch (Exception e)
						{
							await Logger.Log($"Exception Occurred while mapping DG item details for 1 item of booking for account:{dgBooking.AccountCode}, Ref1:{dgBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
						}
					}
					else
					{
						try
						{
							//get the maximum length, width & height
							var MaxLength = dgBooking.BookingItems.Max(x => x.Length);
							var MaxWidth = dgBooking.BookingItems.Max(x => x.Width);
							var MaxHeight = dgBooking.BookingItems.Max(x => x.Height);
							var itemDimension = "L=" + MaxLength + " W=" + MaxWidth + " H=" + MaxHeight;
							if (string.IsNullOrEmpty(dgBooking.ToDetail3))
							{
								tmsBookingRequest.FromAddressDetail.AddressLine3 = itemDimension;
							}
							else if (string.IsNullOrEmpty(dgBooking.ToDetail4))
							{
								tmsBookingRequest.FromAddressDetail.AddressLine4 = itemDimension;
							}
							else
							{

								if (tmsBookingRequest.BookingExtras != null)
								{
									if (!string.IsNullOrEmpty(tmsBookingRequest.BookingExtras.PickupInstructions))
									{
										tmsBookingRequest.BookingExtras.PickupInstructions += " " + itemDimension;
									}
									else
									{
										tmsBookingRequest.BookingExtras.PickupInstructions = itemDimension;
									}
								}
								else
								{
									tmsBookingRequest.BookingExtras = new TmsBookingExtras
									{
										PickupInstructions = itemDimension
									};
								}

							}
						}
						catch (Exception e)
						{
							await Logger.Log($"Exception Occurred while mapping DG item details for multiple items of booking for account:{dgBooking.AccountCode}, Ref1:{dgBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
						}

						var lstBarcodes = new List<string>();
						foreach (var bookingItem in dgBooking.BookingItems)
						{
							if (bookingItem.Quantity > 1)
							{
								for (var i = 0; i < bookingItem.Quantity; i++)
								{
									lstBarcodes.Add(bookingItem.Barcode);
								}
							}
							else
							{
								lstBarcodes.Add(bookingItem.Barcode);
							}
						}

						tmsBookingRequest.Barcodes = lstBarcodes;
					}

					tmsBookingRequest.Items = new List<TmsRequestBookingItem>();
					foreach (var bookingItem in dgBooking.BookingItems)
					{						
						if (bookingItem.Quantity > 1)
						{
							for (var i = 0; i < bookingItem.Quantity; i++)
							{
								var item = new TmsRequestBookingItem { Barcode = bookingItem.Barcode };
								tmsBookingRequest.Items.Add(item);
							}
						}
						else
						{
							var item = new TmsRequestBookingItem { Barcode = bookingItem.Barcode };
							tmsBookingRequest.Items.Add(item);
						}
					}
				}
				
				if (!string.IsNullOrWhiteSpace(dgBooking.TotalItems) &&	dgBooking.TotalItems.All(char.IsDigit))
				{
					tmsBookingRequest.TotalItems = dgBooking.TotalItems;
				}
				else
				{
					if (tmsBookingRequest.Items != null && tmsBookingRequest.Items.Count > 0)
					{
						dgBooking.TotalItems = Convert.ToString(tmsBookingRequest.Items.Count);
						if (!string.IsNullOrWhiteSpace(dgBooking.TotalItems))
						{
							tmsBookingRequest.TotalItems = dgBooking.TotalItems;
						}
					}
				}

				if (dgBooking.ATL != null && (bool)dgBooking.ATL)
				{
					tmsBookingRequest.BookingExtras.Atl = true;
				}
				else
				{
					tmsBookingRequest.BookingExtras.Atl = false;
				}

				//extract contact numbers
				if (dgBooking.BookingContactInformation != null)
				{
					var contactDetail = "";
					if (!string.IsNullOrEmpty(dgBooking.BookingContactInformation.Name))
					{
						contactDetail +=
							dgBooking.BookingContactInformation.Name + " ";
					}
					//we use ToAddressLine3 to insert any phone numbers when its empty
					if (dgBooking.BookingContactInformation.PhoneNumbers != null &&
						dgBooking.BookingContactInformation.PhoneNumbers.Count > 0)
					{
						contactDetail = dgBooking.BookingContactInformation.PhoneNumbers.Aggregate(
							contactDetail,
							(current, contactNumber) => current + contactNumber + " ");
					}
					else if (!string.IsNullOrEmpty(dgBooking.BookingContactInformation.Name))
					{
						contactDetail = dgBooking.BookingContactInformation.Name;
					}
					else
					{
						contactDetail = "";
					}

					if (string.IsNullOrEmpty(tmsBookingRequest.ToAddressDetail.AddressLine3))
					{
						tmsBookingRequest.ToAddressDetail.AddressLine3 = contactDetail;
					}
					else if (string.IsNullOrEmpty(tmsBookingRequest.ToAddressDetail.AddressLine4))
					{
						tmsBookingRequest.ToAddressDetail.AddressLine4 = contactDetail;
					}
				}

				try
				{
					//extract DG Booking information
					if (dgBooking.DgBookingItems != null && dgBooking.DgBookingItems.Count > 0)
					{
						var dgRemark = new StringBuilder();
						var space = " ";
						foreach (var dgBookingItem in dgBooking.DgBookingItems)
						{

							if (dgBookingItem.NumberOfItems != null && dgBookingItem.NumberOfItems != 0 && dgBookingItem.UnitWtOrVol != null && dgBookingItem.UnitWtOrVol != 0)
							{
								dgRemark.Append(dgBookingItem.NumberOfItems.ToString());
								dgRemark.Append(space).Append("X").Append(space).Append(dgBookingItem.UnitWtOrVol);
								dgRemark.Append(space).Append(dgBookingItem.UnitType.ToString());
								var dgClassName = DgHelper.GetDgClassName(dgBookingItem.DgClass);
								if (dgClassName.Length > 0)
								{
									dgRemark.Append(space).Append(dgClassName);
								}
								if (dgBookingItem.SubsidiaryRiskClass != null)
								{
									var dgSubClassName = DgHelper.GetDgClassName(dgBookingItem.SubsidiaryRiskClass, true);
									if (dgSubClassName.Length > 0)
										dgRemark.Append(space).Append(dgSubClassName);
								}
								if (dgBookingItem.PackagingGroup != null)
								{
									var packagingGroupName = DgHelper.GetPackagingGroupName(dgBookingItem.PackagingGroup);
									if (packagingGroupName.Length > 0)
										dgRemark.Append(space).Append(packagingGroupName);
								}
								dgRemark.Append('|');
							}

						}
						if (dgRemark.Length > 0)
							dgRemark.Remove(dgRemark.Length - 1, 1);
						var dgRemarkText = dgRemark.ToString();
						if (dgRemarkText.Length > 0)
						{
							var dgLines = dgRemarkText.Split('|');
							if (dgLines.Length > 0)
							{
								var remarks = new TmsBookingRemarks[dgLines.Length + 2];
								var counter = 0;
								foreach (var dgLine in dgLines)
								{
									var remark = new TmsBookingRemarks
									{
										RemarkText = dgLine
									};
									remarks[counter++] = remark;
								}
								//also add the default remarks line for DG
								var remarkDefaultDgLinePackInst = new TmsBookingRemarks
								{
									RemarkText = DgDefaultLinePackagingInstr
								};
								remarks[counter++] = remarkDefaultDgLinePackInst;
								var remarkDefaultDgLineGoodInst = new TmsBookingRemarks
								{
									RemarkText = DgDefaultLineGoodsInstr
								};
								remarks[counter++] = remarkDefaultDgLineGoodInst;
								tmsBookingRequest.Remarks = remarks;
							}

						}
					}

				}
				catch (Exception e)
				{
					await Logger.Log($"Exception Occurred while extracting DG Item details to map to remarks of booking for account:{dgBooking.AccountCode}, Ref1:{dgBooking.Reference1}. Message: {e.Message}", nameof(OnlineBookingModelConverters));
				}
			}
			catch (Exception e)
			{
				await Logger.Log($"Exception occurred in GetTmsBookingRequest method. Username:{dgBooking.UserCredentials.Username}, Ref1:{dgBooking.Reference1}, Ref2:{dgBooking.Reference2}. Error Message:{e.Message}", nameof(OnlineBookingModelConverters));
			}
			return tmsBookingRequest;
		}
	}
}
