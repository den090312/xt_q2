using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Helpers;

namespace WEB_UI
{
    public static class Index
    {
        public static NameValueCollection Forms { private get; set; } 

        public static WebImage ImageFromRequest { private get; set; }

        public static HttpFileCollection Files { private get; set; }

        public static HttpServerUtilityBase MyServer { private get; set; }

        public static string Message { get; set; }

        static Index()
        {
            Forms = new NameValueCollection();
            Files = HttpContext.Current.Request.Files;
            Message = string.Empty;
        }

        public static void Run()
        {
            CreateUser();
            CreateAward();

            DeleteUserAwards();
            DeleteAwardUsers();

            JoinAwardToUser();

            EditUsers();
            EditAwards();

            Account();

            SaveUserImage();
            SaveAwardImage();

            EditUserRoles();
        }

        private static void Account()
        {
            TryLogIn();
            TryLogOut();
            TryRegister();
        }

        private static void TryRegister()
        {
            var regName = Forms["regName"];
            var regPass = Forms["regPass"];
            var roleName = Forms["roleName"];

            if (regName == null || regPass == null || roleName == null)
            {
                return;
            }

            if (TryUserRegister(regName, regPass, roleName))
            {
                LogIn(regName, regPass);
            }
            else
            {
                Webuser.LogOut();
            }
        }

        private static void TryLogOut()
        {
            var loggedOut = Forms["loggedOut"];

            if (loggedOut != null & loggedOut == "loggedOut")
            {
                Webuser.LogOut();
            }
        }

        private static void TryLogIn()
        {
            var logName = Forms["logName"];
            var logPass = Forms["logPass"];

            if (logName == null || logPass == null)
            {
                return;
            }

            if (!Webuser.NameExists(logName))
            {
                Message = "User name is not exists!";

            }
            else if (!Webuser.PasswordIsOk(logName, logPass))
            {
                Message = "Wrong password!";
            }
            else
            {
                LogIn(logName, logPass);

                Message = string.Empty;
            }
        }

        private static void LogIn(string logName, string logPass) => Webuser.LogIn(logName, logPass);

        private static bool TryUserRegister(string regName, string regPass, string roleName)
        {
            if (regName == string.Empty || regPass == string.Empty || roleName == string.Empty)
            {
                Message = "Finded empty strings!";

                return false;
            }

            if (Webuser.NameExists(regName))
            {
                Message = "User name already exists";

                return false;
            }

            var user = Webuser.Create(regName, Role.Create(roleName), regPass);

            Message = string.Empty;

            return Webuser.Register(user);
        }

        private static void CreateUser()
        {
            var userName = Forms["userName"];
            var dateOfBirth = Forms["dateOfBirth"];

            if (userName == null || dateOfBirth == null)
            {
                return;
            }

            if (userName == string.Empty || dateOfBirth == string.Empty)
            {
                return;
            }

            if (Crud.UserCreate(userName, dateOfBirth))
            {
                Message = "User created";
            }
            else
            {
                Message = "User NOT created";
            }
        }

        private static void DeleteUserAwards()
        {
            var userGuid = Forms["userGuid"];

            if (userGuid == null || userGuid == string.Empty)
            {
                return;
            }

            if (Crud.DeleteUserAwards(userGuid))
            {
                Message = "User with awards deleted";
            }
            else
            {
                Message = "User with awards NOT deleted";
            }
        }

        private static void CreateAward()
        {
            var awardTitle = Forms["awardTitle"];

            if (awardTitle == null || awardTitle == string.Empty)
            {
                return;
            }

            if (Crud.AwardCreate(awardTitle))
            {
                Message = "Award created";
            }
            else
            {
                Message = "Award NOT created";
            }
        }

        private static void DeleteAwardUsers()
        {
            var awardGuid = Forms["awardGuid"];

            if (awardGuid == null || awardGuid == string.Empty)
            {
                return;
            }

            if (Crud.DeleteAwardUsers(awardGuid))
            {
                Message = "Award with users deleted";
            }
            else
            {
                Message = "Award with users NOT deleted";
            }
        }

        private static void JoinAwardToUser()
        {
            var userGuid = Forms["userGuidJoin"];
            var awardGuid = Forms["awardGuidJoin"];

            if (userGuid == null || awardGuid == null)
            {
                return;
            }

            if (userGuid == string.Empty || awardGuid == string.Empty)
            {
                return;
            }

            if (Crud.JoinAwardToUser(userGuid, awardGuid))
            {
                Message = "Award joined to user";
            }
            else
            {
                Message = "Award was NOT joined to user";
            }
        }

        private static void EditUsers()
        {
            var guids = Forms.GetValues("userGuids");
            var names = Forms.GetValues("userNames");
            var dates = Forms.GetValues("userDates");

            if (guids != null & names != null & dates != null)
            {
                if (Crud.EditUsers(guids, names, dates))
                {
                    Message = "Users edited";
                }
                else
                {
                    Message = "Users was NOT edited";
                }
            }
        }

        private static void EditUserRoles()
        {
            var checkedRoles = Forms.GetValues("checkedRole");

            if (checkedRoles == null)
            {
                return;
            }

            if (checkedRoles.Length == 0)
            {
                return;
            }

            if (Webuser.RolesEdit(checkedRoles))
            {
                Message = "Roles edited";
            }
            else
            {
                Message = "Roles was NOT edited";
            }
        }

        private static void EditAwards()
        {
            var guids = Forms.GetValues("awardGuids");
            var titles = Forms.GetValues("awardTitles");

            if (guids != null & titles != null)
            {
                if (Crud.EditAwards(guids, titles))
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
            var userImage = ImageFromRequest;

            if (userImage == null)
            {
                return;
            }

            var userGuidData = Forms["userImageGuid"];

            if (userGuidData == string.Empty || userGuidData == string.Empty)
            {
                return;
            }

            if (!Guid.TryParse(userGuidData, out Guid awardGuid))
            {
                Message = "User guid error reading!";
            }

            if (Images.SaveUserImage(userImage, awardGuid))
            {
                Message = "User image saved";
            }
            else
            {
                Message = "User image was NOT saved";
            }
        }

        private static void SaveAwardImage()
        {
            var userImage = ImageFromRequest;

            if (userImage == null)
            {
                return;
            }

            var awardGuidData = Forms["awardImageGuid"];

            if (awardGuidData == null || awardGuidData == string.Empty)
            {
                return;
            }

            if (!Guid.TryParse(awardGuidData, out Guid userGuid))
            {
                Message = "Award guid error reading!";
            }

            if (Images.SaveAwardImage(userImage, userGuid))
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