using System;

namespace Data.Model
{
    public class TplusWebApi
    {
        public string JobNumber { get; set; } //
        public DateTime JobDate { get; set; } //DateTimeSelected
        //public string EvType { get; set; } //ilpu (subjob = 1), ildel (subjob != 1)

        //public string ImageSrc { get; set; } //poclink, sequence number part of image source

        public string State { get; set; }

        public string SubJobNumber { get; set; }

        public byte[] Image { private get; set; }

        //public string SequenceNumber { get; set; }

        //public string ILogixJobNum { get; set; } //Not required

        public byte[] Get()
        {
            return Image;
        }
    }
}
