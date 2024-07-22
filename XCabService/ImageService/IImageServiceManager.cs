using Data.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCabService.ImageService
{
    public interface IImageServiceManager
    {
        Task<ICollection<ImageResponse>> GetImages(ImageRequest imageRequest);
    }
}
