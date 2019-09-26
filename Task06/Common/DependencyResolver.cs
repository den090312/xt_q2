using BLL;
using InterfacesBLL;
using InterfacesDAL;
using DAL;
using System.Configuration;

namespace Common
{
    public static class DependencyResolver
    {
        private static readonly IUserAwardDao userAwardDao;
        private static readonly IUserDao userDao;
        private static readonly IAwardDao awardDao;

        public static IUserLogic UserLogic { get; private set; }

        public static IAwardLogic AwardLogic { get; private set; }

        public static IUserAwardLogic UserAwardLogic { get; private set; }

        static DependencyResolver()
        {
            var userAwardDaoSet = ConfigurationManager.AppSettings["userAwardDao"];

            if (userAwardDaoSet == "1")
            {
                userAwardDao = new UserAwardDaoFile();
            }

            if (userAwardDaoSet == "2")
            {
                userAwardDao = new UserAwardDaoDb();
            }

            var userDaoSet = ConfigurationManager.AppSettings["userDao"];

            if (userDaoSet == "1")
            {
                userDao = new UserDaoFile();
            }

            if (userDaoSet == "2")
            {
                userDao = new UserDaoDb();
            }

            var awardDaoSet = ConfigurationManager.AppSettings["awardDao"];

            if (awardDaoSet == "1")
            {
                awardDao = new AwardDaoFile();
            }

            if (awardDaoSet == "2")
            {
                awardDao = new AwardDaoDb();
            }

            UserLogic = new UserLogic(userDao);
            AwardLogic = new AwardLogic(awardDao);
            UserAwardLogic = new UserAwardLogic(userAwardDao, userDao, awardDao);
        }
    }
}