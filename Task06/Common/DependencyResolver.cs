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

        private static readonly IUserLogic _userLogic;
        private static readonly IAwardLogic _awardLogic;
        private static readonly IUserAwardLogic _userAwardLogic;

        public static IUserLogic UserLogic => _userLogic;

        public static IAwardLogic AwardLogic => _awardLogic;

        public static IUserAwardLogic UserAwardLogic => _userAwardLogic; 

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

            _userLogic = new UserLogic(userDao);
            _awardLogic = new AwardLogic(awardDao);
            _userAwardLogic = new UserAwardLogic(userAwardDao, userDao, awardDao);
        }
    }
}