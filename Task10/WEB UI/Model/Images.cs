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
        private static readonly string altImageName = "alt.jpg";

        public static string GetImgSrc(Guid imageGuid)
        {
            var imgSrc = GetSrc(imageGuid);

            if (imgSrc == string.Empty)
            {
                imgSrc = altImageName;
            }

            return imgSrc;
        }

        private static string GetSrc(Guid guid)
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
                    var bytes = (byte[])sqlDr["Bytes"];

                    if (bytes == null)
                    {
                        return string.Empty;
                    }

                    return "data:image;base64," + Convert.ToBase64String(bytes);
                }
            }

            return string.Empty;
        }

        public static bool SaveUserImage(WebImage userImage, Guid userGuid)
        {
            NullCheck(userImage);

            try
            {
                SaveImageToUser(userImage, userGuid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveAwardImage(WebImage awardImage, Guid awardGuid)
        {
            NullCheck(awardImage);

            try
            {
                SaveImageToAward(awardImage, awardGuid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Guid GetImageGuidByAwardGuid(Guid awardGuid)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetImageGuidByAwardGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParAwardGuid(awardGuid));
                sqlCommand.Parameters.Add(SqlParImageGuid());

                sqlConnection.Open();

                return GetGuidImage(sqlCommand.ExecuteReader());
            }
        }

        private static Guid GetGuidImage(SqlDataReader sqlDr)
        {
            while (sqlDr.Read())
            {
                var sqlGuid = sqlDr.GetSqlGuid(0);

                if (!sqlGuid.IsNull)
                {
                    return (Guid)(sqlGuid);
                }
            }

            return new Guid();
        }

        public static Guid GetImageGuidByUserGuid(Guid userGuid)
        {
            using (var sqlConnection = new SqlConnection(Database.WebUiConnectionString))
            {
                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "GetImageGuidByUserGuid";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(SqlParUserGuid(userGuid));
                sqlCommand.Parameters.Add(SqlParImageGuid());

                sqlConnection.Open();

                return GetGuidImage(sqlCommand.ExecuteReader());
            }
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

        private static void SaveImageToAward(WebImage awardImage, Guid awardGuid)
        {
            var awardImageBytes = awardImage.GetBytes();
            var imageGuid = GetAddedImageGuid(awardImageBytes);

            if (imageGuid != Guid.Empty)
            {
                AddImageToAward(awardGuid, imageGuid);
            }
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
            return new SqlParameter("@Bytes", SqlDbType.VarBinary, -1)
            {
                Value = imageBytes,
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

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject == null)
            {
                throw new NullReferenceException($"{nameof(classObject)} is null!");
            }
        }
    }
}