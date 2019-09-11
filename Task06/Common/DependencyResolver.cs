using BLL;
using InterfacesBLL;
using DAL;
using InterfacesDAL;

namespace Common
{
    public class DependencyResolver
    {
        public readonly IUserLogic UserBLL = new UserLogic(new UserDaoFile());

        public readonly IAwardLogic AwardBLL = new AwardLogic(new AwardDaoFile());

        public readonly IUserAwardLogic UserAwardBLL = new UserAwardLogic(new UserAwardDaoFile());

        public readonly IUserDao UserDAL = new UserDaoFile();

        public readonly IAwardDao AwardDAL = new AwardDaoFile();

        public readonly IUserAwardDao UserAwardDAL = new UserAwardDaoFile();
    }
}