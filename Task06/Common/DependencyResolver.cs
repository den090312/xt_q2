using BLL;
using InterfacesBLL;
using DAL;
using InterfacesDAL;

namespace Common
{
    public class DependencyResolver
    {
        public readonly IUserLogic UserBLL = new UserLogic(new UserDAO());

        public readonly IAwardLogic AwardBLL = new AwardLogic();

        public readonly IUserAwardLogic UserAwardBLL = new UserAwardLogic();

        public readonly IUserDAO UserDAL = new UserDAO();

        public readonly IAwardDAO AwardDAL = new AwardDAO();

        public readonly IUserAwardDAO UserAwardDAL = new UserAwardDAO();
    }
}