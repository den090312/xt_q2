using System.Web;

namespace WEB_UI
{
    public static class Index
    {
        public static HttpRequestBase Request { get; set; }

        public static string Message { get; set; }

        static Index() => Message = string.Empty;

        public static void Start(out string errorMessage)
        {
            UserCreation();
            UserAwardsDelete();
            AwardCreation();
            AwardUsersDelete();
            JoinAwardToUser();
            EditUsers();
            EditAwards();
            Account(out errorMessage);
        }

        public static void Account(out string errorMessage)
        {
            TryLogIn();
            TryLogOut();
            TryRegister(out errorMessage);
        }

        private static void TryRegister(out string errorMessage)
        {
            var errorResponse = string.Empty;
            var regName = Request.Form["regName"];
            var regPass = Request.Form["regPass"];
            var roleName = Request.Form["roleName"];

            if (regName != null & regPass != null & roleName != null)
            {
                if (TryUserRegister(regName, regPass, roleName, out errorResponse))
                {
                    LogIn(regName, regPass);
                }
                else
                {
                    Webuser.LogOut();
                }
            }

            errorMessage = errorResponse;
        }

        private static void TryLogOut()
        {
            var loggedOut = Request.Form["loggedOut"];

            if (loggedOut != null & loggedOut == "loggedOut")
            {
                Webuser.LogOut();
            }
        }

        private static string TryLogIn()
        {
            var errorResponse = string.Empty;
            var logName = Request.Form["logName"];
            var logPass = Request.Form["logPass"];

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
            var userName = Request.Form["userName"];
            var dateOfBirth = Request.Form["dateOfBirth"];

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
            var userGuid = Request.Form["userGuid"];

            if (userGuid != null)
            {
                if (Crud.UserAwardsDelete(userGuid))
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
            var awardTitle = Request.Form["awardTitle"];

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
            var awardGuid = Request.Form["awardGuid"];

            if (awardGuid != null & awardGuid != "")
            {
                if (Crud.AwardUsersDelete(awardGuid))
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
            var userGuid = Request.Form["userGuidJoin"];
            var awardGuid = Request.Form["awardGuidJoin"];

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
            var guids = Request.Form.GetValues("userGuids");
            var names = Request.Form.GetValues("userNames");
            var dates = Request.Form.GetValues("userDates");

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
            var guids = Request.Form.GetValues("awardGuids");
            var titles = Request.Form.GetValues("awardTitles");

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
    }
}