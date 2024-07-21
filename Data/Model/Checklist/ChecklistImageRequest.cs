using Core;

namespace Data.Model.Checklist;

public class ChecklistImageRequest
{
    public string JobNumber { get; set; }
    public EStates State { get; set; }
    public int LegNumber { get; set; }
    public DateTime JobDate { get; set; }
    public int? ComoJobId { get; set; } 
}
