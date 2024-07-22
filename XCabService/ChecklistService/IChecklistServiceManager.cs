using Data.Model;
using Data.Model.Checklist;

namespace XCabService.ChecklistService;

public interface IChecklistServiceManager
{
    Task<ICollection<ChecklistImageResponse>> GetChecklistImagesAsync(ChecklistImageRequest checklistImageRequest);
}
