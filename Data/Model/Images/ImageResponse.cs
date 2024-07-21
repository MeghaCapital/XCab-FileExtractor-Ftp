using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Images
{
    public class ImageResponse
    {
        public byte[] Image { get; set; }
        public ImageType Type { get; set; }
        public string? Name { get; set; }
    }
}
