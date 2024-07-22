using Data.Model.PodStreamer.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCabService.PodService
{
    public interface IPodService
    {
        Task<ICollection<PodImageResponse>> GetImage(PodImageRequest podImageRequest);
    }
}
