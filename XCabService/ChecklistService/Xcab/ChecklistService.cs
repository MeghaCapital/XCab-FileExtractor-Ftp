using Core;
using Data.Model.Checklist;
using Data.Repository.V2;

namespace XCabService.ChecklistService.Xcab;

public class ChecklistService : IChecklistService
{
    private IXcabChecklistRepository xcabChecklistRepository;

    public ChecklistService(IXcabChecklistRepository xcabChecklistRepository)
    {
        this.xcabChecklistRepository = xcabChecklistRepository;
    }

    public ChecklistService()
    {
        xcabChecklistRepository = new XcabChecklistRepository();
    }

    public async Task<ICollection<ChecklistImageResponse>> GetChecklistImagesAsync(ChecklistImageRequest checklistImageRequest)
    {
        var checklistImages = new List<ChecklistImageResponse>();

        try
        {
            checklistImages = (await xcabChecklistRepository.ExtractChecklistImagesAsync(
                checklistImageRequest.JobNumber,
                checklistImageRequest.LegNumber,
                checklistImageRequest.JobDate,
                checklistImageRequest.State
                )).ToList();
        } catch (Exception ex)
        {
            _ = Logger.Log(
             "Exception Occurred in GetChecklistImage : Failed extracting Checklist image from repository. Message: " +
             ex.Message, nameof(ChecklistService));
        }

        return checklistImages;
    }
}
