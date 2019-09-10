using BLL;
using InterfacesBLL;
using DAL;
using InterfacesDAL;

namespace Common
{
    public class DependencyResolver
    {
        public readonly IUserLogic UserBLL = new UserLogic(new UserDFO());

        public readonly IAwardLogic AwardBLL = new AwardLogic();

        public readonly IUserAwardLogic UserAwardBLL = new UserAwardLogic();

        public readonly IUserDFO UserDAL = new UserDFO();

        public readonly IAwardDFO AwardDAL = new AwardDFO();

        public readonly IUserAwardDFO UserAwardDAL = new UserAwardDFO();
    }
}