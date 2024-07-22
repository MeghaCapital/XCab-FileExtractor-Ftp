using Core;
using Core.Helpers;
using Core.Models.Slack;
using Data.Api.TrackingEvents.Model;
using Data.Model;
using Data.Model.Images;
using Data.Model.Tracking;
using XCabService.ImageService;

namespace XCabService.FileService
{
    public class XCabFileContentServiceProvider
    {
        private const string CapitalSchemaName = "capital";

        public async static Task<List<XCabTrackingFileContentResponse>> GetFileContent(ICollection<XCabTrackingEvent> xCabTrackingEvents, IImageServiceManager imageServiceManager, string schemaName)
        {
            var xCabTrackingFileContentResponses = new List<XCabTrackingFileContentResponse>();

            if (xCabTrackingEvents != null && xCabTrackingEvents.Count > 0 && !string.IsNullOrWhiteSpace(schemaName))
            {
                try
                {
                    foreach (var trackingEvent in xCabTrackingEvents)
                    {
                        var trackingResponse = new TrackingResponse();
                        var xCabTrackingFileContentResponse = new XCabTrackingFileContentResponse();

                        switch (schemaName.ToLower())
                        {
                            case CapitalSchemaName:
                                // skip FTP(No file creation)
                                if (trackingEvent.SkipFtpAccess)
                                    continue;

                                trackingResponse.AccountCode = trackingEvent.AccountCode;
                                trackingResponse.Ref1 = trackingEvent.Ref1;
                                trackingResponse.Ref2 = trackingEvent.Ref2;
                                trackingResponse.ConsignmentNumber = trackingEvent.ConsignmentNumber;
                                trackingResponse.CapitalState = trackingEvent.StateId switch
                                {
                                    1 => CapBusiness.VIC,
                                    2 => CapBusiness.NSW,
                                    3 => CapBusiness.QLD,
                                    4 => CapBusiness.SA,
                                    5 => CapBusiness.WA,
                                    7 => CapBusiness.ACT,
                                    _ => throw new Exception("Unexpected Case"),
                                };
                                trackingResponse.JobNumber = Convert.ToString(trackingEvent.Tplus_JobNumber);
                                trackingResponse.JobDate = (DateTime)(trackingEvent.JobBookingDateTime == null ? DateTime.MinValue : trackingEvent.JobBookingDateTime);
                                
                                trackingResponse.SenderDetails = new AddressDetailsType()
                                {
                                    AddressLine1 = trackingEvent.FromDetail1,
                                    AddressLine2 = trackingEvent.FromDetail2,
                                    Postcode = Convert.ToString(trackingEvent.FromPostcode),
                                    Suburb = trackingEvent.FromSuburb
                                };

                                trackingResponse.ReceiverDetails = new AddressDetailsType()
                                {
                                    AddressLine1 = trackingEvent.ToDetail1,
                                    AddressLine2 = trackingEvent.ToDetail2,
                                    Postcode = Convert.ToString(trackingEvent.ToPostcode),
                                    Suburb = trackingEvent.ToSuburb
                                };

                                // TO DO: Add logic for job booked events and job modified event.
                                if (trackingEvent.PickupArriveDateTime != DateTime.MinValue)
                                {
                                    trackingResponse.EventDateTime = trackingEvent.PickupArriveDateTime;
                                    trackingResponse.TrackingType = TrackingResponseTrackingType.PickupArrive;
                                    trackingResponse.EventCoordinates = new TrackingResponseEventCoordinates
                                    {
                                        Latitude = trackingEvent.PickupArriveLatitude,
                                        Longitude = trackingEvent.PickupCompleteLatitude
                                    };
                                }
                                else if (trackingEvent.PickupCompleteDateTime != DateTime.MinValue)
                                {
                                    trackingResponse.EventDateTime = trackingEvent.PickupCompleteDateTime;
                                    trackingResponse.TrackingType = TrackingResponseTrackingType.PickupComplete;
                                    var pickupPOD = await GetPOD(imageServiceManager, trackingEvent, "pickup");
                                    trackingResponse.EventCoordinates = new TrackingResponseEventCoordinates
                                    {
                                        Latitude = trackingEvent.PickupCompleteLatitude,
                                        Longitude = trackingEvent.PickupCompleteLatitude
                                    };
                                    trackingResponse.PODUrl = new PodInformationType
                                    {
                                        PodJobNumber = trackingEvent.Tplus_JobNumber.ToString(),
                                        PodSubJobNumber = "01",
                                        Base64Image = Convert.ToBase64String(pickupPOD.Image),
                                        SignatoriesName = pickupPOD.Name
                                    };
                                }
                                else if (trackingEvent.DeliveryArriveDateTime != DateTime.MinValue)
                                {
                                    trackingResponse.EventDateTime = trackingEvent.DeliveryArriveDateTime;
                                    trackingResponse.TrackingType = TrackingResponseTrackingType.DeliveryArrive;
                                    trackingResponse.EventCoordinates = new TrackingResponseEventCoordinates
                                    {
                                        Latitude = trackingEvent.DeliveryArriveLatitude,
                                        Longitude = trackingEvent.DeliveryArriveLongitude
                                    };
                                }
                                else if (trackingEvent.DeliveryCompleteDateTime != DateTime.MinValue)
                                {
                                    trackingResponse.EventDateTime = trackingEvent.DeliveryCompleteDateTime;
                                    trackingResponse.TrackingType = TrackingResponseTrackingType.DeliveryComplete;
                                    var deliveryPOD = await GetPOD(imageServiceManager, trackingEvent, "delivery"); 
                                    trackingResponse.EventCoordinates = new TrackingResponseEventCoordinates
                                    {
                                        Latitude = trackingEvent.DeliveryCompleteLatitude,
                                        Longitude = trackingEvent.DeliveryCompleteLongitude
                                    };
                                    trackingResponse.PODUrl = new PodInformationType
                                    {
                                        PodJobNumber = trackingEvent.Tplus_JobNumber.ToString(),
                                        PodSubJobNumber = "02",
                                        Base64Image = Convert.ToBase64String(deliveryPOD.Image),
                                        SignatoriesName = deliveryPOD.Name
                                    };
                                }
                                else if (trackingEvent.Cancelled)
                                {
                                    trackingResponse.TrackingType = TrackingResponseTrackingType.Cancelled;
                                    trackingResponse.EventDateTime = trackingEvent.DeliveryArriveDateTime;
                                    trackingResponse.EventCoordinates = new TrackingResponseEventCoordinates
                                    {
                                        Latitude = trackingEvent.DeliveryArriveLatitude,
                                        Longitude = trackingEvent.DeliveryArriveLongitude
                                    };
                                }
                                break;
                        }
                        xCabTrackingFileContentResponse.LoginDetails = trackingEvent.LoginDetails;
                        xCabTrackingFileContentResponse.TrackingResponse = trackingResponse;
                        // Currently we have only xml files  
                        xCabTrackingFileContentResponse.FileExtension = FileExtension.xml;
                        xCabTrackingFileContentResponses.Add(xCabTrackingFileContentResponse);
                    }
                }
                catch (Exception ex)
                {
                    await Logger.LogSlackNotificationFromApp("XCAB",
                              $"Error while constucting file content in GetFileContent. Details are {ex.Message}",
                              Name(), SlackChannel.GeneralErrors);
                }
            }
            return xCabTrackingFileContentResponses;
        }

        private static async Task<ImageResponse> GetPOD(IImageServiceManager imageServiceManager, XCabTrackingEvent trackingEvent, string typeOfPOD)
        {
            var imageResponse = new ImageResponse();
            var LegNumber = 2;
            try
            {
                if (!string.IsNullOrWhiteSpace(typeOfPOD) && string.Equals(typeOfPOD, "pickup", StringComparison.InvariantCultureIgnoreCase))
                    LegNumber = 1;

                var pods = await imageServiceManager.GetImages(new ImageRequest
                {
                    JobNumber = trackingEvent.Tplus_JobNumber.ToString(),
                    LegNumber = LegNumber,
                    JobDate = (DateTime)trackingEvent.JobBookingDateTime,
                    State = StateHelpers.GetStateEnum(trackingEvent.StateId),
                    ImageTypeRequested = new List<ImageType>() { ImageType.Pod }
                });
                if (pods != null && pods.Count > 0)
                {
                    imageResponse = pods.First();
                }
            }
            catch(Exception ex)
            {
                Logger.LogSlackNotificationFromApp("XCAB",
                              $"Error while getting POD. Details are {ex.Message}",
                              Name(), SlackChannel.GeneralErrors);
            }
            return imageResponse;
        }

        private static string Name()
        {
            return nameof(XCabFileContentServiceProvider);
        }
    }
}
