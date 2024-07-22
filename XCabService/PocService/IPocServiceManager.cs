using Data.Model.Poc.V2;

namespace XCabService.PocService
{
	public interface IPocServiceManager
	{
		 Task<ICollection<PocImageResponse>> GetPocImage(PocImageRequest pocImageRequest);
	}
}
