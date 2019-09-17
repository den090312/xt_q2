using System;
using System.IO;
using System.Web;

namespace WEB_UI
{
    public static class Images
    {
        private static readonly string altImageName = "alt";

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

        public static string GetImgSrc(string root, string guid)
        {
            NullCheck(root);
            NullCheck(guid);

            var imgSrc = string.Empty;
            var altSrc = string.Empty;

            var path = Path.Combine(root, guid);

            if (File.Exists(path))
            {
                var base64 = Convert.ToBase64String(File.ReadAllBytes(path));
                imgSrc = string.Format("data:image/gif;base64,{0}", base64);
            }

            var altPath = Path.Combine(root, altImageName);

            if (File.Exists(altPath))
            {
                var alt64 = Convert.ToBase64String(File.ReadAllBytes(altPath));
                altSrc = string.Format("data:image/gif;base64,{0}", alt64);
            }

            if (imgSrc == string.Empty)
            {
                imgSrc = altSrc;
            }

            return imgSrc;
        }

        public static bool ImageSaved(string imagePath, string root, HttpPostedFile image, string guid)
        {
            try
            {
                image.SaveAs(imagePath);

                var imageBytes = File.ReadAllBytes(imagePath);
                File.Delete(imagePath);

                var finalPath = Path.Combine(root, guid);
                File.WriteAllBytes(finalPath, imageBytes);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject == null)
            {
                throw new NullReferenceException($"{nameof(classObject)} is null!");
            }
        }
    }
}