using Data.Entities.Ilogix;
using Data.Model.PodStreamer.V2;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCabService.PodService.ILogix
{
    public class ILogixPodService : IPodService
    {
        IIlogixImagesRepository iIlogixImagesRepository;

        public ILogixPodService(IIlogixImagesRepository iIlogixImagesRepository)
        {
            this.iIlogixImagesRepository = iIlogixImagesRepository;
        }
        public async Task<ICollection<PodImageResponse>> GetImage(PodImageRequest podImageRequest)
        {
            var PodImages = new List<PodImageResponse>();
            if (podImageRequest != null)
            {
                var subJobNumber = string.Concat("0", podImageRequest.LegNumber);
                var images = (List<PodImage>) iIlogixImagesRepository.GetPodImage(podImageRequest.JobNumber, subJobNumber, Core.Helpers.StateHelpers.GetStatePrefix(podImageRequest.StateId), podImageRequest.JobDate);
                if(images.Count > 0)
                {
                    foreach(var image in images)
                    {
                        var pod = new PodImageResponse()
                        {
                            Image = image.podImage,
                            PodName = image.PODName
                        };
                        PodImages.Add(pod);
					}
				}
            }
            return PodImages;
        }
    }
}
