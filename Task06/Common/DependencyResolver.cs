using BLL;
using InterfacesBLL;
using DAL;

namespace Common
{
    public class DependencyResolver
    {
        public readonly IUserLogic UserBLL = new UserLogic(new UserDaoFile());

        public readonly IAwardLogic AwardBLL = new AwardLogic(new AwardDaoFile());

        public readonly IUserAwardLogic UserAwardBLL = new UserAwardLogic(new UserAwardDaoFile());
    }
}