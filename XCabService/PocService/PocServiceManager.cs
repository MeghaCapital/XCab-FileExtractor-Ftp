using Core;
using Data.Model.Poc.V2;
using Data.Repository.V2;
using Newtonsoft.Json;
using xcab.como.tracker.Service;
using XCabService.PocService.Como;
using XCabService.PocService.ILogix;

namespace XCabService.PocService;

public class PocServiceManager : IPocServiceManager
{
    IPocService pocService;
    public PocServiceManager(IPocService pocService)
    {
        this.pocService = pocService;
    }        

    public PocServiceManager()
    {

    }

    public async Task<ICollection<PocImageResponse>> GetPocImage(PocImageRequest pocImageRequest)
    {
        var pocImages = new List<PocImageResponse>();
        try
        {
            if (pocImageRequest != null)
            {
                if (pocImageRequest.ComoJobId != 0 && pocImageRequest.ComoJobId != null)
                {
                    pocService = new ComoPocService(new ImageExtractor());
                }
                else
                {
                    pocService = new ILogixPocService(new ILogixPocRepository());
                }
                pocImages = (List<PocImageResponse>)await pocService.GetImage(pocImageRequest);
            }
        }
        catch (Exception ex)
        {
            await Logger.Log(
                 "Exception Occurred in GetPocImage : Failed extracting POC image for request - " + JsonConvert.SerializeObject(pocImageRequest) + ". Message: " +
                 ex.Message, nameof(PocServiceManager));
        }
        return pocImages;
    }
}
