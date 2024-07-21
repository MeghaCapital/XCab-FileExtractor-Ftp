using System.ComponentModel;
using System.Drawing;

namespace Core.Helpers
{
    public static class ImageUtils
    {
        public static Bitmap GetBitmapForByte(byte[] imageByte)
        {
            var tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap? bitmap = null;

            if (imageByte != null)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                bitmap = (Bitmap)tc.ConvertFrom(imageByte);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
#pragma warning disable CS8603 // Possible null reference return.
            return bitmap;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}