using System;
using System.IO;
using System.Web;

namespace WEB_UI
{
    public static class Images
    {
        private static readonly string altImageName = "alt";

        public static bool TryGetImage(HttpFileCollection images, out HttpPostedFile imageFile, out string imageFileName)
        {
            if (images == null || images.Count == 0)
            {
                imageFileName = string.Empty;
                imageFile = null;

                return false;
            }

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

        public static string GetImgSrc(string root, Guid guid)
        {
            NullCheck(root);

            if (root == string.Empty || guid.ToString() == string.Empty)
            {
                return string.Empty;
            }

            var imgSrc = string.Empty;
            var altSrc = string.Empty;

            //imgSrc = GetImgSrcFromFile(root, guid.ToString(), imgSrc);
            imgSrc = GetImgSrcFromDB(guid, imgSrc);
            altSrc = GetAltSrc(root, altSrc);

            if (imgSrc == string.Empty)
            {
                imgSrc = altSrc;
            }

            return imgSrc;
        }

        private static string GetImgSrcFromDB(Guid guid, string imgSrc)
        {
            return imgSrc;
        }

        public static bool SaveImage(string imagePath, string root, HttpPostedFile image, string guid)
        {
            NullCheck(imagePath);
            NullCheck(root);
            NullCheck(guid);

            if (imagePath == string.Empty || root == string.Empty || guid == string.Empty)
            {
                return false;
            }

            try
            {
                SaveImageToFile(imagePath, root, image, guid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GetAltSrc(string root, string altSrc)
        {
            var altPath = Path.Combine(root, altImageName);

            if (File.Exists(altPath))
            {
                var alt64 = Convert.ToBase64String(File.ReadAllBytes(altPath));
                altSrc = string.Format("data:image/gif;base64,{0}", alt64);
            }

            return altSrc;
        }

        private static string GetImgSrcFromFile(string root, string guid, string imgSrc)
        {
            var path = Path.Combine(root, guid);

            if (File.Exists(path))
            {
                var base64 = Convert.ToBase64String(File.ReadAllBytes(path));
                imgSrc = string.Format("data:image/gif;base64,{0}", base64);
            }

            return imgSrc;
        }

        private static void SaveImageToFile(string imagePath, string root, HttpPostedFile image, string guid)
        {
            image.SaveAs(imagePath);

            var imageBytes = File.ReadAllBytes(imagePath);
            File.Delete(imagePath);

            var finalPath = Path.Combine(root, guid);
            File.WriteAllBytes(finalPath, imageBytes);
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