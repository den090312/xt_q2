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

            switch (userAwardDaoSet)
            {
                case "1":
                    userAwardDao = new UserAwardDaoFile();
                    break;
                case "2":
                    userAwardDao = new UserAwardDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(userAwardDaoSet)}!");
            }

            var userDaoSet = ConfigurationManager.AppSettings["userDao"];

            switch (userDaoSet)
            {
                case "1":
                    userDao = new UserDaoFile();
                    break;
                case "2":
                    userDao = new UserDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(userAwardDaoSet)}!");
            }

            var awardDaoSet = ConfigurationManager.AppSettings["awardDao"];

            switch (awardDaoSet)
            {
                case "1":
                    awardDao = new AwardDaoFile();
                    break;
                case "2":
                    awardDao = new AwardDaoDb();
                    break;
                default:
                    throw new ConfigurationErrorsException($"Can't find settings for {nameof(userAwardDaoSet)}!");
            }

            UserLogic = new UserLogic(userDao);
            AwardLogic = new AwardLogic(awardDao);
            UserAwardLogic = new UserAwardLogic(userAwardDao, userDao, awardDao);
        }
    }
}