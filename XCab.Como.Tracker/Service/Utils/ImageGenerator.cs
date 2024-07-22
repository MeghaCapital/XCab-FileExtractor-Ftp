using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCab.Como.Tracker.Service.Utils
{
	public class ImageGenerator
	{
        public async static Task<System.Drawing.Bitmap> GenerateBitmap(List<byte[]> Images)
        {
            //read all images into memory
            var images = new List<System.Drawing.Bitmap>();
            System.Drawing.Bitmap finalImage = null;

            try
            {
                var width = 0;
                var height = 0;

                foreach (var image in Images)
                {
                    //create a Bitmap from the file and add it to the list
                    using (var ms = new MemoryStream(image))
                    {
                        var bitmap = new System.Drawing.Bitmap(ms);

                        //update the size of the final bitmap
                        //update the height of the combined bitmap that have so far
                        height += bitmap.Height;
                        width = bitmap.Width > width ? bitmap.Width : width;

                        images.Add(bitmap);
                    }

                }

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (var g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(System.Drawing.Color.White);

                    //go through each image and draw it on the final image
                    var offset = 0;
                    foreach (var image in images)
                    {
                        g.DrawImage(image,
                            new System.Drawing.Rectangle(0, offset, image.Width, image.Height));
                        offset += image.Height;
                    }
                }

                return finalImage;
            }
            catch (Exception)
            {
                if (finalImage != null)
                    finalImage.Dispose();
                //throw ex;
                throw;
            }
            finally
            {
                //clean up memory
                foreach (var image in images)
                {
                    image.Dispose();
                }
            }
        }

        public async static Task<byte[]> GetByteArrayFromImage(Image sourceBmp)
        {
            using (var stream = new MemoryStream())
            {
                sourceBmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
