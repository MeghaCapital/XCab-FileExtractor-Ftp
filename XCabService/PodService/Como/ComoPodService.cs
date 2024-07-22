using Data.Model.PodStreamer.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xcab.como.tracker;
using xcab.como.tracker.Service;

namespace XCabService.PodService.Como
{
    public class ComoPodService : IPodService
    {
        IImageExtractor imageExtractor;
        public ComoPodService(IImageExtractor imageExtractor)
        {
            this.imageExtractor = imageExtractor;
        }
        public async Task<ICollection<PodImageResponse>> GetImage(PodImageRequest podImageRequest)
        {
            var PodImages = new List<PodImageResponse>();
            if (podImageRequest != null)
            {

                var legNumber = ELegType.PICKUP;
                switch (podImageRequest.LegNumber)
                {
                    case 1:
                        legNumber = ELegType.PICKUP;
                        break;
                    case 2:
                        legNumber = ELegType.DELIVERY;
                        break;
                }
                PodImages = (List<PodImageResponse>)await imageExtractor.GetPod((int)podImageRequest.ComoJobId, legNumber);

            }
            return PodImages;
        }
    }
}
