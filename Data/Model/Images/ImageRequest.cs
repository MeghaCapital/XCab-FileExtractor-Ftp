using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Images
{
    public class ImageRequest
    {
        public string JobNumber { get; set; }
        public EStates State { get; set; }
        public int LegNumber { get; set; }
        public DateTime JobDate { get; set; }
        public int? ComoJobId { get; set; }
        public ICollection<ImageType>? ImageTypeRequested { get; set; }

    }
}
