using Core;

namespace Data.Model.PodStreamer
{
    public class ImageRequest
    {
        public string JobNumber { get; set; } = string.Empty;
        public string SubJobNumber { get; set; } =  string.Empty ;
        public DateTime AllocationDateTime { get; set; }
        public EStates State { get; set; }
    }
}
