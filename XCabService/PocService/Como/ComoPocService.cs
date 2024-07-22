using Data.Model.Poc.V2;
using xcab.como.tracker;
using xcab.como.tracker.Service;

namespace XCabService.PocService.Como
{
    public class ComoPocService : IPocService
    {
        IImageExtractor imageExtractor;
        public ComoPocService(IImageExtractor imageExtractor)
        {
            this.imageExtractor = imageExtractor;
        }
        public async Task<ICollection<PocImageResponse>> GetImage(PocImageRequest pocImageRequest)
        {
            var PocImages = new List<PocImageResponse>();
            if (pocImageRequest != null)
            {

                var legNumber = ELegType.PICKUP;
                switch (pocImageRequest.LegNumber)
                {
                    case 1:
                        legNumber = ELegType.PICKUP;
                        break;
                    case 2:
                        legNumber = ELegType.DELIVERY;
                        break;
                }
                PocImages = (List<PocImageResponse>)await imageExtractor.GetPoc((int)pocImageRequest.ComoJobId, legNumber);

            }
            return PocImages;
        }
    }
}
