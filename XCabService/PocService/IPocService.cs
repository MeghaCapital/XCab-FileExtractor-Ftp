using Data.Model.Poc.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCabService.PocService
{
    public interface IPocService
    {
        Task<ICollection<PocImageResponse>> GetImage(PocImageRequest pocImageRequest);
    }
}
