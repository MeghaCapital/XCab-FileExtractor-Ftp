using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.tracker.Data.Models
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        //public string CaptureType { get; set; }
        public int MobileId { get; set; }
        public int ImageTypeId { get; set; }
        public string Name { get; set; }
        public DateTime? Received { get; set; }
    }
}
