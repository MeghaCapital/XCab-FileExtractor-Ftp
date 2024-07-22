using Core;
using Data.Model.Images;
using Data.Model.Poc.V2;
using Data.Repository.SecondaryRepositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCabService.ChecklistService;
using XCabService.PocService;
using XCabService.PodService;

namespace XCabService.ImageService
{
    public class ImageServiceManager : IImageServiceManager
    {
        IPocServiceManager pocServiceManager;
        IPodServiceManager podServiceManager;
        IChecklistServiceManager checklistServiceManager;

        public ImageServiceManager(
            IPodServiceManager podServiceManager, 
            IPocServiceManager pocServiceManager,
            IChecklistServiceManager checklistServiceManager
            )
        {
            this.pocServiceManager = pocServiceManager;
            this.podServiceManager = podServiceManager;
            this.checklistServiceManager = checklistServiceManager;
        }
        public async Task<ICollection<ImageResponse>> GetImages(ImageRequest imageRequest)
        {
            var images = new List<ImageResponse>();
            try
            {
                if (imageRequest.ImageTypeRequested != null)
                {
                    if (imageRequest.ImageTypeRequested.Contains(ImageType.Pod))
                    {
                        var podImage = await podServiceManager.GetPodImage(new Data.Model.PodStreamer.V2.PodImageRequest
                        {
                            ComoJobId = imageRequest.ComoJobId,
                            JobDate = imageRequest.JobDate,
                            JobNumber = imageRequest.JobNumber,
                            LegNumber = imageRequest.LegNumber,
                            StateId = imageRequest.State
                        }
                       );
                        if (podImage != null && podImage.Count > 0)
                        {
                            images.Add(new ImageResponse
                            {
                                Image = podImage.ToList()[0].Image,
                                Name = podImage.ToList()[0].PodName,
                                Type = ImageType.Pod
                            });
                        }
                    }
                    if (imageRequest.ImageTypeRequested.Contains(ImageType.Poc))
                    {
                        var pocImages = await pocServiceManager.GetPocImage(new PocImageRequest
                        {
                            ComoJobId = imageRequest.ComoJobId,
                            JobDate = imageRequest.JobDate,
                            JobNumber = imageRequest.JobNumber,
                            LegNumber = imageRequest.LegNumber,
                            State = imageRequest.State

                        }
                        );
                        if (pocImages != null && pocImages.Count > 0)
                        {
                            foreach (var pocImage in pocImages)
                            {
                                images.Add(new ImageResponse
                                {
                                    Image = pocImage.Image,
                                    Name = "POC",
                                    Type = ImageType.Poc
                                });
                            }
                        }

                    }
                    if (imageRequest.ImageTypeRequested.Contains(ImageType.Checklist))
                    {
                        var checklistImages = await checklistServiceManager.GetChecklistImagesAsync(new Data.Model.Checklist.ChecklistImageRequest
                        {
                            ComoJobId = imageRequest.ComoJobId,
                            JobDate = imageRequest.JobDate,
                            JobNumber = imageRequest.JobNumber,
                            LegNumber = imageRequest.LegNumber,
                            State = imageRequest.State
                        }
                        );
                        if (checklistImages != null && checklistImages.Count > 0)
                        {
                            foreach (var img in checklistImages)
                            {
                                images.Add(new ImageResponse
                                {
                                    Image = img.Image,
                                    Name = "CHECKLIST",
                                    Type = ImageType.Checklist
                                });
                            }
                        }
                    }
                    if (imageRequest.ImageTypeRequested.Contains(ImageType.GenericClientSpecificChecklist))
                    {

                    }
                    if (imageRequest.ImageTypeRequested.Contains(ImageType.VehicleLoadingCraneChecklist))
                    {

                    }
                }
                else
                {
                    var podImage = await podServiceManager.GetPodImage(new Data.Model.PodStreamer.V2.PodImageRequest
                    {
                        ComoJobId = imageRequest.ComoJobId,
                        JobDate = imageRequest.JobDate,
                        JobNumber = imageRequest.JobNumber,
                        LegNumber = imageRequest.LegNumber,
                        StateId = imageRequest.State
                    }
                    );
                    var pocImages = await pocServiceManager.GetPocImage(new PocImageRequest
                    {
                        ComoJobId = imageRequest.ComoJobId,
                        JobDate = imageRequest.JobDate,
                        JobNumber = imageRequest.JobNumber,
                        LegNumber = imageRequest.LegNumber,
                        State = imageRequest.State

                    }
                    );
                    if (podImage != null && podImage.Count > 0)
                    {
                        images.Add(new ImageResponse
                        {
                            Image = podImage.ToList()[0].Image,
                            Name = podImage.ToList()[0].PodName,
                            Type = ImageType.Pod
                        });
                    }
                    if (pocImages != null && pocImages.Count > 0)
                    {
                        foreach (var pocImage in pocImages)
                        {
                            images.Add(new ImageResponse
                            {
                                Image = pocImage.Image,
                                Name = "POC",
                                Type = ImageType.Poc
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                     "Exception Occurred in GetImages : Failed extracting images for request - " + JsonConvert.SerializeObject(imageRequest) + ". Message: " +
                     ex.Message, nameof(ImageServiceManager));
            }
            return images;
        }
    }
}

