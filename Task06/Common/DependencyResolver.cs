using BLL;
using InterfacesBLL;
using DAL;
using InterfacesDAL;

namespace Common
{
    public class DependencyResolver
    {
        public readonly IUserLogic UserBLL = new UserLogic();
        public readonly IAwardLogic AwardBLL = new AwardLogic();
    }
}