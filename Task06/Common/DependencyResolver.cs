using BLL;
using InterfacesBLL;
using DAL;

namespace Common
{
    public class DependencyResolver
    {
        public readonly IUserLogic UserBll = new UserLogic(new UserDaoFile());

        public readonly IAwardLogic AwardBll = new AwardLogic(new AwardDaoFile());

        public readonly IUserAwardLogic UserAwardBll = new UserAwardLogic(new UserAwardDaoFile(), new UserDaoFile(), new AwardDaoFile());
    }
}