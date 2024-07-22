using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using xcab.como.booker.Data;
using xcab.como.booker.Data.Response;
using xcab.como.booker.Data.Variable;
using xcab.como.common;
using xcab.como.common.Service;
using xcab.como.common.Struct;
using XCab.Como.Booker.Data;
using XCab.Como.Booker.Data.Response;
using XCab.Como.Booker.Data.Variable;

namespace xcab.como.booker.Service
{
	public class ComoJobCreator : ComoDefaultProcessor, IComoJobCreator
	{
		public ComoJobCreator() : base()
		{

		}

		public async Task<XcabJobResponse> Create(ComoBookingRequest bookingRequest, EBookingPhaseRequest bpRequest)
		{
			if (!string.IsNullOrEmpty(base.ApiToken))
			{
				Job job = null;

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Payload received. Json: " + JsonConvert.SerializeObject(payload), Constants.ErrorList.Information);

				//IdentityCollectionManager.Initialise(base.ApiToken);

				string city = string.Equals(bookingRequest.State, "vic", StringComparison.InvariantCultureIgnoreCase) ? "Melbourne"
					: string.Equals(bookingRequest.State, "nsw", StringComparison.InvariantCultureIgnoreCase) ? "Sydney"
					: string.Equals(bookingRequest.State, "qld", StringComparison.InvariantCultureIgnoreCase) ? "Brisbane"
					: string.Equals(bookingRequest.State, "sa", StringComparison.InvariantCultureIgnoreCase) ? "Adelaide"
					: string.Equals(bookingRequest.State, "wa", StringComparison.InvariantCultureIgnoreCase) ? "Perth"
					: string.Equals(bookingRequest.State, "act", StringComparison.InvariantCultureIgnoreCase) ? "Australian Capital Territory"
					: string.Equals(bookingRequest.State, "nat", StringComparison.InvariantCultureIgnoreCase) ? "National"
					: string.Equals(bookingRequest.State, "all", StringComparison.InvariantCultureIgnoreCase) ? "All"
					: "All";

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Start retrieving account id.", Constants.ErrorList.Information);

				int account = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Accounts,
					"{businessUnit:{name: {_eq: \"" + city + "\"}}, client:{clientCode:{ code: {_eq: \"" + bookingRequest.AccountCode + "\"}}}}"
				)).Result;

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Complete retrieving account id.", Constants.ErrorList.Information);
				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Start retrieving service id.", Constants.ErrorList.Information);

				int service = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Services,
					"{code: {_eq: \"" + bookingRequest.ServiceCode + "\"}}"
				)).Result;

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Complete retrieving service id.", Constants.ErrorList.Information);
				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Start retrieving job booking phases id.", Constants.ErrorList.Information);

				int jobBookingPhase = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.JobBookingPhases,
					"{name: {_eq: \"" + bpRequest.ToString() + "\"}}"
				)).Result;

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Complete retrieving job booking phases id.", Constants.ErrorList.Information);
				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Start retrieving pickup suburb id.", Constants.ErrorList.Information);

				int pickupSuburb = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Suburbs,
					"{ name: {_eq : \"" + bookingRequest.FromSuburb.ToUpperInvariant() + "\"}, postcode : {postcodeValue : {_eq : \"" + bookingRequest.FromPostcode + "\"}}, isPostOfficeBox : {_eq: false}, state : { abbreviation : {_eq : \"" + bookingRequest.State.ToUpperInvariant() + "\" } } } "
				)).Result;

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Complete retrieving pickup suburb id.", Constants.ErrorList.Information);
				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Start retrieving delivery suburb id.", Constants.ErrorList.Information);

				int deliverySuburb = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Suburbs,
					"{ name: {_eq : \"" + bookingRequest.ToSuburb.ToUpperInvariant() + "\"}, postcode : {postcodeValue : {_eq : \"" + bookingRequest.ToPostcode + "\"}}, isPostOfficeBox : {_eq: false}, state : { abbreviation : {_eq : \"" + bookingRequest.State.ToUpperInvariant() + "\" } } } "
				)).Result;

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Complete retrieving delivery suburb id.", Constants.ErrorList.Information);
				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Start retrieving sub job creation method id.", Constants.ErrorList.Information);

				int subJobCreationMethod = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.SubJobCreationMethods,
					"{name: {_eq: \"XCab\"}}"
				)).Result;

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Complete retrieving sub job creation method id.", Constants.ErrorList.Information);

				int barcodeScanTypeId = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.BarcodeScanTypes,
					"{ name: { _eq: \"Scanned\"}}"
				)).Result;

				if ((account > 0) && (jobBookingPhase > 0) && (service > 0) && (pickupSuburb > 0) && (deliverySuburb > 0) && (subJobCreationMethod > 0))
				{
					//Log.Write(GetType().Name + " - svc/xcab-como - ", "Create ordered references.", Constants.ErrorList.Information);

					List<Reference> orderedReferences = new List<Reference>();
					orderedReferences.Add(new Reference() { order = 1, value = bookingRequest.Ref1 });
					orderedReferences.Add(new Reference() { order = 2, value = bookingRequest.Ref2 });

					//Log.Write(GetType().Name + " - svc/xcab-como - ", "Create sub job legs.", Constants.ErrorList.Information);

					serviceChargingMechanismPricingItem cubic = new serviceChargingMechanismPricingItem();

					if (bookingRequest.TotalVolume != null)
					{
						cubic.enteredQuantity = double.Parse(bookingRequest.TotalVolume);
					}
					else
					{
						var totalCubic = bookingRequest.lstItems != null ? bookingRequest.lstItems.Sum(x => x.Cubic).ToString() : null;
						if (totalCubic != null)
							cubic.enteredQuantity = double.Parse(totalCubic);
					}

					List<SubJobLeg> subJobLegs = new List<SubJobLeg>();

					subJobLegs.Add(new SubJobLeg(
							orderedReferences[0].order,
							orderedReferences,
							bookingRequest.ToDetail2,
							deliverySuburb,
							bookingRequest.ToDetail1,
							//Need to determine how to retrieve eta
							//DateTime.Now,
							//payload.AdvanceDateTime,
							authorityToLeaveSafe: bookingRequest.ATL,
							extraInformation: bookingRequest.ExtraDelInformation + ":" + bookingRequest.ToDetail3 + ":" + bookingRequest.ToDetail4 + ":" + bookingRequest.ToDetail5,
							clientPricing: cubic.enteredQuantity
						));

					//Log.Write(GetType().Name + " - svc/xcab-como - ", "Create sub jobs.", Constants.ErrorList.Information);

					DateTime despatchDateTime = default(DateTime);
					bool isAdvanced = default(bool);

					if (bookingRequest.AdvanceDateTime != default(DateTime))
					{
						despatchDateTime = bookingRequest.AdvanceDateTime;
						isAdvanced = true;
					}
					else if (bookingRequest.DespatchDateTime != default(DateTime))
					{
						despatchDateTime = bookingRequest.DespatchDateTime;
						isAdvanced = false;
					}

					List<string> itemBarcodes = bookingRequest.lstItems.Select(x => x.Barcode).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

					List<Barcode> barcodes = new List<Barcode>();

					for (int x = 0; x < itemBarcodes.Count(); x++)
					{
						Barcode barcode = new Barcode()
						{
							barcodeString = itemBarcodes[x],
							barcodeScanType = new BarcodeScanType()
							{
								id = barcodeScanTypeId
							}
						};
						barcodes.Add(barcode);
					};

					List<Remarks> remarks = new List<Remarks>();

					for (int x = 0; x < bookingRequest.Remarks.Count(); x++)
					{
						Remarks remark = new Remarks()
						{
							remarkValue = bookingRequest.Remarks[x]
						};
						remarks.Add(remark);
					};

					List<SubJob> subJobs = new List<SubJob>();
					subJobs.Add(new SubJob(
						1,
						despatchDateTime.ToUniversalTime(),
						"Found " + ((bookingRequest.lstItems != null) && (bookingRequest.lstItems.All(i => i.Quantity > 0)) ? bookingRequest.lstItems.Sum(i => i.Quantity).ToString() : bookingRequest.lstItems.Count.ToString()) + " items",
						subJobLegs,
						service,
						bookingRequest.FromDetail2,
						pickupSuburb,
						bookingRequest.FromDetail1,
						extraInformation: bookingRequest.ExtraPuInformation + ":" + bookingRequest.FromDetail3 + ":" + bookingRequest.FromDetail4 + ":" + bookingRequest.FromDetail5,
						totalWeight: (bookingRequest.lstItems != null) && (bookingRequest.lstItems.All(i => i.Weight > 0)) && (bookingRequest.lstItems.All(i => i.Quantity > 0)) ? bookingRequest.lstItems.Sum(i => i.Weight * i.Quantity) : int.TryParse(bookingRequest.TotalWeight, out int j) ? j : 0,
						totalPieces: (bookingRequest.lstItems != null) && (bookingRequest.lstItems.All(i => i.Quantity > 0)) ? bookingRequest.lstItems.Sum(i => i.Quantity) : bookingRequest.lstItems.Count(),
						externalBookingReference: bookingRequest.ConsignmentNumber,
						isAdvancedBooking: isAdvanced,
						unsPhone: bookingRequest.TrackAndTraceSmsNumber,
						unsEmail: bookingRequest.TrackAndTraceEmailAddress,
						barcodes: barcodes,
						remarks: remarks
					));

					//Log.Write(GetType().Name + " - svc/xcab-como - ", "Create job.", Constants.ErrorList.Information);

					job = new Job(
						account,
						jobBookingPhase,
						subJobCreationMethod,
						bookingRequest.Caller,
						subJobs
					);
				}
				return Task.Run(async () => await Generate(job)).Result;
			}

			//Log.Write(GetType().Name + " - svc/xcab-como - ", "Api token not specified.", Constants.ErrorList.Information);

			return null;
		}

		private async Task<XcabJobResponse> Generate(Job job)
		{
			if (!string.IsNullOrEmpty(base.ApiToken))
			{
				Client.Initialise(base.ApiToken);

				Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);

				//ComoJobCreator.jobClient.UpdateTransactionMode(ETransaction.Modify);

				//Log.Write(GetType().Name + " - svc/xcab-como - ", "Book job.", Constants.ErrorList.Information);

				string operation = "bookJob";

				// Need to add job variable to query
				var bookJobResponse = Task.Run(async () => await Client.SetAsync(
					operation,
					"mutation " + operation + "($job: CreateBookJob!) { " + operation + "(job: $job) }",
					new
					{
						job = job
					}
				)).Result;

				if (bookJobResponse != null)
				{
					int jobId = int.Parse(bookJobResponse[operation].ToString());
					//int jobId = 13200110;

					//Log.Write(GetType().Name + " - svc/xcab-como - ", "Job " + Convert.ToString(jobId) + " created.", Constants.ErrorList.Information);

					//ComoJobCreator.jobClient.UpdateTransactionMode(ETransaction.Get);

					operation = "Job";

					var existingJobResponse = Task.Run(async () => await Client.GetAsync<JobResponse>(
						operation,
						"query " + operation + " { job(id: " + jobId.ToString() + ") { displayName subJobs {subJobLegs { clientPricingItem { chargingMechanismPricingItems { itemPrice { price } } } calculatedETA } } } }"
					)).Result;

					if (existingJobResponse != null)
					{
						double totalPrice = 0;
						double totalPriceExGst = 0;

						if (existingJobResponse.job != null)
						{
							if (existingJobResponse.job.subJobs != null)
							{
								foreach (var subjob in existingJobResponse.job.subJobs)
								{
									if (subjob.subJobLegs != null)
									{
										foreach (var subjobleg in subjob.subJobLegs)
										{
											if (subjobleg.clientPricingItem.chargingMechanismPricingItems != null)
											{
												foreach (var item in subjobleg.clientPricingItem.chargingMechanismPricingItems)
												{
													totalPrice += item.itemPrice.price;
													//Need to determine calculation
													totalPriceExGst += (item.itemPrice.price * 0.1);
												}
											}
										}
									}
								}
							}
						}

						XcabJobResponse response = new XcabJobResponse()
						{
							JobNumber = JobEndec.Decode(existingJobResponse.job.displayName, EEncoding.BASE26),
							Gst = totalPrice - totalPriceExGst,
							JobTotalPrice = totalPrice,
							JobPriceExGst = totalPriceExGst,
							JobId = jobId
						};

						//Log.Write(GetType().Name + " - svc/xcab-como - ", "Response created. Json: " + JsonConvert.SerializeObject(response), Constants.ErrorList.Information);

						return response;
					}
				}
			}

			return null;
		}

		public async Task<XcabJobResponse> GetQuote(ComoQuoteRequest quoteRequest, EBookingPhaseRequest bpRequest)
		{
			var xcabJobResponse = new XcabJobResponse();
			if (quoteRequest != null)
			{
				string city = string.Equals(quoteRequest.State, "vic", StringComparison.InvariantCultureIgnoreCase) ? "Melbourne"
						: string.Equals(quoteRequest.State, "nsw", StringComparison.InvariantCultureIgnoreCase) ? "Sydney"
						: string.Equals(quoteRequest.State, "qld", StringComparison.InvariantCultureIgnoreCase) ? "Brisbane"
						: string.Equals(quoteRequest.State, "sa", StringComparison.InvariantCultureIgnoreCase) ? "Adelaide"
						: string.Equals(quoteRequest.State, "wa", StringComparison.InvariantCultureIgnoreCase) ? "Perth"
						: string.Equals(quoteRequest.State, "act", StringComparison.InvariantCultureIgnoreCase) ? "Australian Capital Territory"
						: string.Equals(quoteRequest.State, "nat", StringComparison.InvariantCultureIgnoreCase) ? "National"
						: string.Equals(quoteRequest.State, "all", StringComparison.InvariantCultureIgnoreCase) ? "All"
						: "All";

				int accountId = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Accounts,
					"{businessUnit:{name: {_eq: \"" + city + "\"}}, client:{clientCode:{ code: {_eq: \"" + quoteRequest.AccountCode + "\"}}}}"
				)).Result;


				int serviceId = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Services,
					"{code: {_eq: \"" + quoteRequest.ServiceCode + "\"}}"
				)).Result;

				int jobBookingPhaseId = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.JobBookingPhases,
					"{name: {_eq: \"" + bpRequest.ToString() + "\"}}"
				)).Result;

				int fromSuburbId = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Suburbs,
					"{ name: {_eq : \"" + quoteRequest.FromSuburb.ToUpperInvariant() + "\"}, postcode : {postcodeValue : {_eq : \"" + quoteRequest.FromPostcode + "\"}}, isPostOfficeBox : {_eq: false}, state : { abbreviation : {_eq : \"" + quoteRequest.State.ToUpperInvariant() + "\" } } } "
				)).Result;

				int toSuburbId = Task.Run(async () => await RetrieveEntityAsync(
					EEntities.Suburbs,
					"{ name: {_eq : \"" + quoteRequest.ToSuburb.ToUpperInvariant() + "\"}, postcode : {postcodeValue : {_eq : \"" + quoteRequest.ToPostcode + "\"}}, isPostOfficeBox : {_eq: false}, state : { abbreviation : {_eq : \"" + quoteRequest.State.ToUpperInvariant() + "\" } } } "
				)).Result;

				if (!string.IsNullOrWhiteSpace(city) && accountId > 0 && serviceId > 0 && jobBookingPhaseId > 0 && fromSuburbId > 0 && toSuburbId > 0)
				{
					Client.Initialise(base.ApiToken);
					Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);
					var routeRequests = new List<RouteRequest>();
					var legs = new List<Leg>();
					var leg = new Leg
					{
						Order = 1,
						ToSuburbId = toSuburbId
					};
					legs.Add(leg);
					var requestId = "";
					var routeRequest = new RouteRequest
					{
						RequestId = requestId,
						DespatchDateUtc = quoteRequest.DespatchDateTime,
						AccountId = accountId,
						ServiceId = serviceId,
						FromSuburbId = fromSuburbId,
						Legs = legs,
					};
					routeRequests.Add(routeRequest);
					string operation = "priceRoutes";
					var quoteResponse = Task.Run(async () => await Client.SendQueryAsync(
							operation,
							@$"query {operation}($routeRequests: [RouteRequest]!) 
								{{ {operation}(routeRequests: $routeRequests) 
									{{ clientPrice 
										{{ price 
											{{ 
												fuel job 
											}} 
										}} 
									}} 
								}}",
							new
							{
								routeRequests = routeRequests
							}
						)).Result;

					if(!string.IsNullOrWhiteSpace(quoteResponse))
					{
						var response = JsonConvert.DeserializeObject<QuoteResponse>(quoteResponse);
						var jobPriceExGst = response.priceRoutes.First().clientPrice.price.job;
						var gst = 0.0;
						if (jobPriceExGst > 0)
						{
							gst = jobPriceExGst * 0.1;
						}
						var jobToalPrice = jobPriceExGst + gst;
						xcabJobResponse.JobTotalPrice = jobToalPrice > 0 ? jobToalPrice : 0;
						xcabJobResponse.JobPriceExGst = jobPriceExGst > 0 ? jobPriceExGst : 0;
						xcabJobResponse.Gst = gst;
					}
				}
			}
			return xcabJobResponse;
		}
	}
}
