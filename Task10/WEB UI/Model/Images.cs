using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Helpers;

namespace WEB_UI
{
    public static class Images
    {
        private static readonly string altImageName = "alt";

        public static bool TryGetImage(HttpFileCollection images, out HttpPostedFile image)
        {
            NullCheck(images);

            if (images.Count == 0)
            {
                image = null;

                return false;
            }

            image = images[0];

            NullCheck(image);

            return true;
        }

        public static string GetImgSrc(string root, Guid imageGuid)
        {
            NullCheck(root);

            if (root == string.Empty || imageGuid == Guid.Empty)
            {
                return string.Empty;
            }

            var imgSrc = string.Empty;
            var altSrc = string.Empty;

            imgSrc = GetImgSrcFromDB(imageGuid, imgSrc);
            altSrc = GetAltSrc(root, altSrc);

            if (imgSrc == string.Empty)
            {
                imgSrc = altSrc;
            }

            return imgSrc;
        }

        private static string GetImgSrcFromDB(Guid guid, string src)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetImageByGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParGuid(guid));
                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                while (sqlDr.Read())
                {
                    //var typeConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
                    //var bitmap = (Bitmap)typeConverter.ConvertFrom(bytes);
                    var bytes = (byte[])sqlDr["Bytes"];
                    var base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    src = "data:image;base64," + base64String;
                }
            }

            return src;
        }

        public static bool SaveUserImage(WebImage userImage, string userGuid)
        {
            NullCheck(userImage);
            NullCheck(userGuid);

            try
            {
                SaveImageToUser(userImage, Guid.Parse(userGuid));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveAwardImage(HttpPostedFile awardImage, string awardGuid)
        {
            NullCheck(awardImage);
            NullCheck(awardGuid);

            try
            {
                SaveImageToAward(awardImage, Guid.Parse(awardGuid));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Guid GetImageGuidByAwardGuid(Guid awardGuid)
        {
            var guidImage = new Guid();

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetImageGuidByAwardGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParAwardGuid(awardGuid));
                sqlCommand.Parameters.Add(SqlParImageGuid());
                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                while (sqlDr.Read())
                {
                    var sqlGuid = sqlDr.GetSqlGuid(0);

                    if (!sqlGuid.IsNull)
                    {
                        guidImage = (Guid)(sqlGuid);
                    }
                }
            }

            return guidImage;
        }

        public static Guid GetImageGuidByUserGuid(Guid userGuid)
        {
            var guidImage = new Guid();

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetImageGuidByUserGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(SqlParUserGuid(userGuid));
                sqlCommand.Parameters.Add(SqlParImageGuid());
                sqlConnection.Open();

                var sqlDr = sqlCommand.ExecuteReader();

                while (sqlDr.Read())
                {
                    var sqlGuid = sqlDr.GetSqlGuid(0);

                    if (!sqlGuid.IsNull)
                    {
                        guidImage = (Guid)(sqlGuid);
                    }
                }
            }

            return guidImage;
        }

        private static void SaveImageToUser(WebImage userImage, Guid userGuid)
        {
            var userImageBytes = userImage.GetBytes();
            var imageGuid = GetAddedImageGuid(userImageBytes);

            if (imageGuid != Guid.Empty)
            {
                AddImageToUser(userGuid, imageGuid);
            }
        }

        private static void SaveImageToAward(HttpPostedFile awardImage, Guid awardGuid)
        {
            //var awardImageBytes = GetBytes(awardImage);
            //var imageGuid = GetAddedImageGuid(awardImageBytes);

            //if (imageGuid != Guid.Empty)
            //{
            //    AddImageToAward(awardGuid, imageGuid);
            //}
        }

        private static void AddImageToUser(Guid userGuid, Guid imageGuid)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddImageToUser";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParGuid(userGuid));
                sqlCommand.Parameters.Add(SqlParImageGuid(imageGuid));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static void AddImageToAward(Guid awardGuid, Guid imageGuid)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddImageToAward";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParGuid(awardGuid));
                sqlCommand.Parameters.Add(SqlParImageGuid(imageGuid));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        private static Guid GetAddedImageGuid(byte[] imageBytes)
        {
            var imageGuid = Guid.NewGuid();

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddImage";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParGuid(imageGuid));
                sqlCommand.Parameters.Add(SqlParBytes(imageBytes));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }

            return imageGuid;
        }

        private static Guid GetAddedImageGuid(HttpPostedFile imageFile)
        {
            var imageGuid = Guid.NewGuid();

            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "AddImage";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParGuid(imageGuid));
                sqlCommand.Parameters.Add(SqlParFile(imageFile));

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }

            return imageGuid;
        }

        private static SqlParameter SqlParFile(HttpPostedFile imageFile)
        {
            return new SqlParameter
            {
                ParameterName = "@Bytes",
                Value = imageFile,
                SqlDbType = SqlDbType.Image,
                Direction = ParameterDirection.Input
            };
        }

        private static byte[] GetBytes(HttpPostedFile image)
        {
            byte[] imageBytes = null;

            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                imageBytes = binaryReader.ReadBytes(image.ContentLength);
            }

            return imageBytes;
        }

        private static SqlParameter SqlParUserGuid(Guid userGuid)
        {
            return new SqlParameter
            {
                ParameterName = "@UserGuid",
                Value = userGuid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParAwardGuid(Guid awardGuid)
        {
            return new SqlParameter
            {
                ParameterName = "@AwardGuid",
                Value = awardGuid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParBytes(byte[] imageBytes)
        {
            return new SqlParameter
            {
                ParameterName = "@Bytes",
                Value = imageBytes,
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParGuid(Guid guid)
        {
            return new SqlParameter
            {
                ParameterName = "@Guid",
                Value = guid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParGuid()
        {
            return new SqlParameter
            {
                ParameterName = "@Guid",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Output
            };
        }

        private static SqlParameter SqlParImageGuid(Guid imageGuid)
        {
            return new SqlParameter
            {
                ParameterName = "@ImageGuid",
                Value = imageGuid,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input
            };
        }

        private static SqlParameter SqlParImageGuid()
        {
            return new SqlParameter
            {
                ParameterName = "@ImageGuid",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Output
            };
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