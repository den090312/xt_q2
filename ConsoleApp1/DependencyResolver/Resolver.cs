using BllInterfaces;
using DALInterafces;
using UserLogic;
using UserDalFileSystem;

namespace DependencyResolver
{
    public static class Resolver
    {
        public static IUserDal userDal => new UserDal();

        public static IUserLogic GetUserLogic => new CrudLogic(userDal);
    }
}
