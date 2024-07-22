using Data.Model.Futile;
using Data.Repository.V2;

namespace XCabService.FutileService
{
	public class FutileServiceManager : IFutileServiceManager
	{
		IILogixFutileJobRepository ilogixFutileJobRepository;
		public FutileServiceManager(IILogixFutileJobRepository ilogixFutileJobRepository)
		{
			this.ilogixFutileJobRepository = ilogixFutileJobRepository;
		}


		public async Task<FutileJobResponse> GetFutileDetails(string consignmentNumber)
		{
			var futileJobResponse = new FutileJobResponse();
			var preLegs = new List<FutileLeg>();
			var postLegs = new List<FutileLeg>();
			var currentLeg = new FutileLeg();
			var currentLegFound = false;
			var originalJob = false;

			try
			{
				consignmentNumber = consignmentNumber.Trim();
				var originalConsignmentNumber = consignmentNumber;

				if (consignmentNumber.Contains(".F") && consignmentNumber.Split('.').Count() == 3)
					originalConsignmentNumber = consignmentNumber.Split('.')[0].ToString();
				else
					originalJob = true;

				var futileJobDetails = await ilogixFutileJobRepository.GetFutileJobDetails(originalConsignmentNumber);
				if (futileJobDetails != null && futileJobDetails.Count > 0)
				{
					futileJobResponse.AccountCode = futileJobDetails.FirstOrDefault().AccountCode;
					futileJobResponse.StateId = futileJobDetails.FirstOrDefault().StateId;
					futileJobResponse.OriginalJobNumber = futileJobDetails.FirstOrDefault().JobNumber;
					futileJobResponse.OriginalConNumber = originalConsignmentNumber;
					futileJobResponse.SubJobNumber = futileJobDetails.FirstOrDefault().SubJobNumber;
					futileJobResponse.Ref1 = futileJobDetails.FirstOrDefault().Ref1;
					futileJobResponse.Ref2 = futileJobDetails.FirstOrDefault().Ref2;

					if (originalJob)
					{
						currentLeg = new FutileLeg()
						{
							ConsignmentNumber = originalConsignmentNumber,
							IsUltimateOfBatch = true,
							JobNumber = futileJobDetails.FirstOrDefault().JobNumber,
							IsUltimateOfJob = false
						};

						currentLegFound = true;
					}

					var idOfUltimateLeg = futileJobDetails.Where(x => x.IsUltimateOfBatch == true).ToList().Any() ?
											futileJobDetails.Where(x => x.IsUltimateOfBatch == true).ToList().Max(y => y.Id)
											: futileJobDetails.ToList().Max(y => y.Id);

					foreach (var jobDetail in futileJobDetails)
					{
						if (jobDetail.ConsignmentNumber.Trim() == consignmentNumber)
						{
							currentLeg = new FutileLeg()
							{
								ConsignmentNumber = jobDetail.ConsignmentNumber,
								IsUltimateOfBatch = jobDetail.IsUltimateOfBatch,
								FutileType = (futileType)jobDetail.FutileType,
								JobNumber = jobDetail.GeneratedFutileJobNumber,
								IsUltimateOfJob = (idOfUltimateLeg == jobDetail.Id ? true : false)
							};

							currentLegFound = true;
						}
						else if (currentLegFound)
						{
							postLegs.Add(new FutileLeg()
							{
								ConsignmentNumber = jobDetail.ConsignmentNumber,
								IsUltimateOfBatch = jobDetail.IsUltimateOfBatch,
								FutileType = (futileType)jobDetail.FutileType,
								DriverNumber = jobDetail.DriverNumber,
								JobNumber = jobDetail.GeneratedFutileJobNumber,
								IsUltimateOfJob = (idOfUltimateLeg == jobDetail.Id ? true : false)
							});
						}
						else
						{
							preLegs.Add(new FutileLeg()
							{
								ConsignmentNumber = jobDetail.ConsignmentNumber,
								IsUltimateOfBatch = jobDetail.IsUltimateOfBatch,
								FutileType = (futileType)jobDetail.FutileType,
								DriverNumber = jobDetail.DriverNumber,
								JobNumber = jobDetail.GeneratedFutileJobNumber,
								IsUltimateOfJob = (idOfUltimateLeg == jobDetail.Id ? true : false)
							});

						}
					}

					futileJobResponse.CurrentLeg = currentLeg;

					if (preLegs.Count > 0)
						futileJobResponse.PreLegs = preLegs;

					if (postLegs.Count > 0)
						futileJobResponse.PostLegs = postLegs;
				}
				else
                {
					futileJobResponse = null;
				}
			}
			catch (Exception)
			{
				//log
			}

			return futileJobResponse;
		}		
	}
}
