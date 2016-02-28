using System.Drawing;
using System.Drawing.Imaging;

namespace Mendeo.Common.Core
{
    public class ImageHelper
    {
        public static string RetrieveFileFormatExtension(ImageFormat imageFormat)
        {
            if (imageFormat.Guid == ImageFormat.Jpeg.Guid)
            {
                return "jpg";
            }

            if (imageFormat.Guid == ImageFormat.Png.Guid)
            {
                return "png";
            }

            if (imageFormat.Guid == ImageFormat.Bmp.Guid)
            {
                return "bmp";
            }

            if (imageFormat.Guid == ImageFormat.Gif.Guid)
            {
                return "gif";
            }

            if (imageFormat.Guid == ImageFormat.Icon.Guid)
            {
                return "ico";
            }

            return "jpg";
        }

        public static string RetrieveFileFormatExtension(byte[] inputImage)
        {
            var imgInput = ImageSaver.ByteArrayToImage(inputImage);
            var thisFormat = imgInput.RawFormat;
            return RetrieveFileFormatExtension(thisFormat);
        }
    }
}
