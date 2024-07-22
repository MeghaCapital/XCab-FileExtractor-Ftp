using Data.Model.Checklist;

namespace XCabService.ChecklistService;

public interface IChecklistService
{
    Task<ICollection<ChecklistImageResponse>> GetChecklistImagesAsync(ChecklistImageRequest checklistImageRequest);
}
