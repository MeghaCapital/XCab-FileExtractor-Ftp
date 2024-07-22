using Data.Model.Poc.V2;
using Data.Repository.V2;

namespace XCabService.PocService.ILogix
{
	public class ILogixPocService: IPocService
	{
		IILogixPocRepository iLogixPocRepository;
		public ILogixPocService(IILogixPocRepository iLogixPocRepository)
		{
			this.iLogixPocRepository = iLogixPocRepository;
		}
		public async Task<ICollection<PocImageResponse>> GetImage(PocImageRequest pocImageRequest)
		{
			var PocImages = new List<PocImageResponse>();
			if (pocImageRequest != null)
			{
				PocImages = (List<PocImageResponse>)await iLogixPocRepository.ExtractPocImages(pocImageRequest.JobNumber, pocImageRequest.LegNumber, pocImageRequest.JobDate, pocImageRequest.State);
			}
			return PocImages;
		}
	}
}
