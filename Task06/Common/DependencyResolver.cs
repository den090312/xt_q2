using BLL;
using InterfacesBLL;
using InterfacesDAL;
using DAL;

namespace Common
{
    public static class DependencyResolver
    {
        private static readonly IUserLogic _userLogic;
        private static readonly IUserDao userDao;
        private static readonly IAwardLogic _awardLogic;
        private static readonly IAwardDao awardDao;
        private static readonly IUserAwardLogic _userAwardLogic;
        private static readonly IUserAwardDao userAwardDao;

        public static IUserLogic UserLogic => _userLogic;

        public static IAwardLogic AwardLogic => _awardLogic;

        public static IUserAwardLogic UserAwardLogic => _userAwardLogic;

        static DependencyResolver()
        {
            //userDao = new UserDaoFile();
            userDao = new UserDaoDb();
            _userLogic = new UserLogic(userDao);
            //awardDao = new AwardDaoFile();
            awardDao = new AwardDaoDb();
            _awardLogic = new AwardLogic(awardDao);
            //userAwardDao = new UserAwardDaoFile();
            userAwardDao = new UserAwardDaoDb();
            _userAwardLogic = new UserAwardLogic(userAwardDao, userDao, awardDao);
        }
    }
}