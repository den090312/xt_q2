using System.Collections.Specialized;

namespace WEB_UI
{
    public static class Index
    {
        public static NameValueCollection RequestForm { get; set; }

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

        private static void Account(out string errorMessage)
        {
            TryLogIn();
            TryLogOut();
            TryRegister(out errorMessage);
        }

        private static void TryRegister(out string errorMessage)
        {
            var errorResponse = string.Empty;
            var regName = RequestForm["regName"];
            var regPass = RequestForm["regPass"];
            var roleName = RequestForm["roleName"];

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
            var loggedOut = RequestForm["loggedOut"];

            if (loggedOut != null & loggedOut == "loggedOut")
            {
                Webuser.LogOut();
            }
        }

        private static string TryLogIn()
        {
            var errorResponse = string.Empty;
            var logName = RequestForm["logName"];
            var logPass = RequestForm["logPass"];

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
            var userName = RequestForm["userName"];
            var dateOfBirth = RequestForm["dateOfBirth"];

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
            var userGuid = RequestForm["userGuid"];

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
            var awardTitle = RequestForm["awardTitle"];

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
            var awardGuid = RequestForm["awardGuid"];

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
            var userGuid = RequestForm["userGuidJoin"];
            var awardGuid = RequestForm["awardGuidJoin"];

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
            var guids = RequestForm.GetValues("userGuids");
            var names = RequestForm.GetValues("userNames");
            var dates = RequestForm.GetValues("userDates");

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
            var guids = RequestForm.GetValues("awardGuids");
            var titles = RequestForm.GetValues("awardTitles");

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