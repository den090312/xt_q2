using System.IO;
using System.Web;

namespace WEB_UI
{
    public static class Images
    {
        public static bool ImageExists(HttpFileCollection images, out HttpPostedFile imageFile, out string imageFileName)
        {
            imageFile = images[0];

            if (imageFile == null)
            {
                imageFileName = string.Empty;

                return false;
            }

            if (images == null || images.Count == 0)
            {
                imageFileName = string.Empty;
                imageFile = null;

                return false;
            }

            imageFileName = Path.GetFileName(imageFile.FileName);

            if (imageFileName == string.Empty)
            {
                return false;
            }

            return true;
        }
    }
}