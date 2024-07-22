using Core;
using Data.Model.PodStreamer.V2;
using Data.Repository.EntityRepositories;
using Newtonsoft.Json;
using xcab.como.tracker.Service;
using XCabService.PodService.Como;
using XCabService.PodService.ILogix;

namespace XCabService.PodService;

public class PodServiceManager : IPodServiceManager
{
    IPodService podService;
    public PodServiceManager(IPodService podService)
    {
        this.podService = podService;
    }

    public PodServiceManager() { }

    public async Task<ICollection<PodImageResponse>> GetPodImage(PodImageRequest podImageRequest)
    {
        var podImages = new List<PodImageResponse>();
        try
        {
            if (podImageRequest != null)
            {
                if (podImageRequest.ComoJobId != 0 && podImageRequest.ComoJobId != null)
                {
                    podService = new ComoPodService(new ImageExtractor());
                }
                else
                {
                    podService = new ILogixPodService(new IlogixImagesRepository());
                }
                podImages = (List<PodImageResponse>)await podService.GetImage(podImageRequest);
            }
        }
        catch (Exception ex)
        {
            await Logger.Log(
                 "Exception Occurred in GetPodImage : Failed extracting POD image for request - " + JsonConvert.SerializeObject(podImageRequest) + ". Message: " +
                 ex.Message, nameof(PodServiceManager));
        }
        return podImages;
    }
}
