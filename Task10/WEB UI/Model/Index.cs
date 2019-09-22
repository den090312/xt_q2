﻿using System.Collections.Specialized;
using System.Web;

namespace WEB_UI
{
    public static class Index
    {
        public static NameValueCollection Forms { private get; set; }

        public static HttpFileCollection Files { private get; set; }

        public static HttpServerUtilityBase MyServer { private get; set; }

        public static string Message { get; set; }

        static Index()
        {
            Forms = new NameValueCollection();
            Files = HttpContext.Current.Request.Files;
            Message = string.Empty;
        }

        public static void Run(out string alert)
        {
            UserCreation();
            UserAwardsDelete();
            AwardCreation();
            AwardUsersDelete();
            JoinAwardToUser();
            EditUsers();
            EditAwards();
            Account(out alert);
            SaveUserImage();
            SaveAwardImage();
        }

        private static void Account(out string alert)
        {
            TryLogIn();
            TryLogOut();
            TryRegister(out alert);
        }

        private static void TryRegister(out string alert)
        {
            var alertResponse = string.Empty;
            var regName = Forms["regName"];
            var regPass = Forms["regPass"];
            var roleName = Forms["roleName"];

            if (regName != null & regPass != null & roleName != null)
            {
                if (TryUserRegister(regName, regPass, roleName, out alertResponse))
                {
                    LogIn(regName, regPass);
                }
                else
                {
                    Webuser.LogOut();
                }
            }

            alert = alertResponse;
        }

        private static void TryLogOut()
        {
            var loggedOut = Forms["loggedOut"];

            if (loggedOut != null & loggedOut == "loggedOut")
            {
                Webuser.LogOut();
            }
        }

        private static string TryLogIn()
        {
            var errorResponse = string.Empty;
            var logName = Forms["logName"];
            var logPass = Forms["logPass"];

            if (logName != null & logPass != null)
            {
                if (!Webuser.NameExists(logName))
                {
                    errorResponse = "@<script>alert('User name is not exists!');</script>";

                }
                else if (!Webuser.PasswordIsOk(logName, logPass))
                {
                    errorResponse = "@<script>alert('Wrong password!');</script>";
                }
                else
                {
                    LogIn(logName, logPass);
                }
            }

            return errorResponse;
        }

        private static void LogIn(string logName, string logPass) => Webuser.LogIn(logName, logPass);

        private static bool TryUserRegister(string regName, string regPass, string roleName, out string errorResponse)
        {
            errorResponse = string.Empty;

            if (regName == string.Empty || regPass == string.Empty || roleName == string.Empty)
            {
                errorResponse = "<script>alert('Finded empty strings!');</script>";

                return false;
            }

            if (Webuser.NameExists(regName))
            {
                Message = "User name already exists";

                errorResponse = "<script>alert('User name already exists!');</script>";

                return false;
            }

            var user = Webuser.Create(regName, Role.Create(roleName), regPass);

            return Webuser.Register(user);
        }

        private static void UserCreation()
        {
            var userName = Forms["userName"];
            var dateOfBirth = Forms["dateOfBirth"];

            if (userName != null & dateOfBirth != null)
            {
                if (Crud.UserCreate(userName, dateOfBirth))
                {
                    Message = "User created";
                }
                else
                {
                    Message = "User NOT created";
                }
            }
        }

        private static void UserAwardsDelete()
        {
            var userGuid = Forms["userGuid"];

            if (userGuid != null)
            {
                if (Crud.DeleteUserAwards(userGuid))
                {
                    Message = "User with awards deleted";
                }
                else
                {
                    Message = "User with awards NOT deleted";
                }
            }
        }

        private static void AwardCreation()
        {
            var awardTitle = Forms["awardTitle"];

            if (awardTitle != null)
            {
                if (Crud.AwardCreate(awardTitle))
                {
                    Message = "Award created";
                }
                else
                {
                    Message = "Award NOT created";
                }
            }
        }

        private static void AwardUsersDelete()
        {
            var awardGuid = Forms["awardGuid"];

            if (awardGuid != null & awardGuid != "")
            {
                if (Crud.DeleteAwardUsers(awardGuid))
                {
                    Message = "Award with users deleted";
                }
                else
                {
                    Message = "Award with users NOT deleted";
                }
            }
        }

        private static void JoinAwardToUser()
        {
            var userGuid = Forms["userGuidJoin"];
            var awardGuid = Forms["awardGuidJoin"];

            if (userGuid != null & awardGuid != null)
            {
                if (Crud.JoinAwardToUser(userGuid, awardGuid))
                {
                    Message = "Award joined to user";
                }
                else
                {
                    Message = "Award was NOT joined to user";
                }
            }
        }

        private static void EditUsers()
        {
            var guids = Forms.GetValues("userGuids");
            var names = Forms.GetValues("userNames");
            var dates = Forms.GetValues("userDates");

            if (guids != null & names != null & dates != null)
            {
                if (Crud.UsersEdit(guids, names, dates))
                {
                    Message = "Users edited";
                }
                else
                {
                    Message = "Users was NOT edited";
                }
            }
        }

        private static void EditAwards()
        {
            var guids = Forms.GetValues("awardGuids");
            var titles = Forms.GetValues("awardTitles");

            if (guids != null & titles != null)
            {
                if (Crud.AwardsEdit(guids, titles))
                {
                    Message = "Awards edited";
                }
                else
                {
                    Message = "Awards was NOT edited";
                }
            }
        }

        private static void SaveUserImage()
        {
            var root = MyServer.MapPath("~");

            if (!Images.TryGetImage(Files, out var userImageFile, out var userImageFileName))
            {
                return;
            }

            var userImageGuid = Forms["userImageGuid"];

            if (userImageGuid != null)
            {
                var userImagePath = MyServer.MapPath(userImageFileName);

                if (Images.ImageSaved(userImagePath, root, userImageFile, userImageGuid))
                {
                    Message = "User image saved";
                }
                else
                {
                    Message = "User image was NOT saved";
                }
            }
        }

        private static void SaveAwardImage()
        {
            var root = MyServer.MapPath("~");

            if (!Images.TryGetImage(Files, out var awardImageFile, out var awardImageFileName))
            {
                return;
            }

            var awardImageGuid = Forms["awardImageGuid"];

            if (awardImageGuid != null)
            {
                var awardImagePath = MyServer.MapPath(awardImageFileName);

                if (Images.ImageSaved(awardImagePath, root, awardImageFile, awardImageGuid))
                {
                    Message = "Award image saved";
                }
                else
                {
                    Message = "Award image was NOT saved";
                }
            }
        }
    }
}