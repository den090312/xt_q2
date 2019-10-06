using Common;
using Entities;
using System;
using System.Collections.Specialized;

namespace WebPL.Models
{
    public class Manager
    {
        public static NameValueCollection Forms { get; set; }

        public static string Message { get; private set; }

        static Manager() => Message = string.Empty;

        public static void Run(NameValueCollection forms)
        {
            Forms = forms;

            if (AddManager())
            {
                return;
            }
        }

        private static bool AddManager()
        {
            var roleIdString = Forms["selectRoleIdChosen"];

            var userName = Forms["newManagerLogin"];
            var password = Forms["newManagerPass"];

            var managerName = Forms["newManagerName"];
            var rank = Forms["selectRankChosen"];

            if (string.IsNullOrEmpty(roleIdString) || string.IsNullOrEmpty(rank) || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(managerName))
            {
                return false;
            }

            if (!int.TryParse(roleIdString, out int roleId))
            {
                return false;
            }

            var manager = GetManager(managerName, rank, GetAddedUser(userName, password, roleId));

            return ManagerAdded(ref manager);
        }

        private static bool ManagerAdded(ref Entities.Manager manager)
        {
            if (Dependencies.ManagerLogic.Add(ref manager))
            {
                Message = "Менеджер добавлен";

                return true;
            }
            else
            {
                Message = "Ошибка добавления менеджера!";

                return false;
            }
        }

        private static Entities.Manager GetManager(string name, string rankString, User user)
        {
            var rank = (Entities.Manager.Rank)Enum.Parse(typeof(Entities.Manager.Rank), rankString);

            return new Entities.Manager(user.Id, name, rank);
        }

        private static User GetAddedUser(string name, string password, int roleId)
        {
            var user = new User(roleId, name);
            Dependencies.UserLogic.Add(ref user, user.IdRole, password);

            return user;
        }
    }
}