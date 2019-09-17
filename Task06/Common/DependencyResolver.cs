using BLL;
using InterfacesBLL;
using InterfacesDAL;
using DAL;

namespace Common
{
    public static class DependencyResolver
    {
        private static readonly IUserLogic _userLogic;
        private static readonly IUserDao _userDao;
        private static readonly IAwardLogic _awardLogic;
        private static readonly IAwardDao _awardDao;
        private static readonly IUserAwardLogic _userAwardLogic;
        private static readonly IUserAwardDao _userAwardDao;

        public static IUserLogic UserLogic => _userLogic;

        public static IAwardLogic AwardLogic => _awardLogic;

        public static IUserAwardLogic UserAwardLogic => _userAwardLogic;

        static DependencyResolver()
        {
            _userDao = new UserDaoFile();
            _userLogic = new UserLogic(_userDao);
            _awardDao = new AwardDaoFile();
            _awardLogic = new AwardLogic(_awardDao);
            _userAwardDao = new UserAwardDaoFile();
            _userAwardLogic = new UserAwardLogic(_userAwardDao, _userDao, _awardDao);
        }
    }
}