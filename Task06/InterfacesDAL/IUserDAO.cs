using Entities;

namespace InterfacesDAL
{
    public interface IUserDAO
    {
        void AddUser(User user);

        void RemoveUsers(string userName);

        bool UsersExists(string userName);

        string[] GetUserIDArray(string userName);

        void PrintUsers();

        void EraseUser(string userID);
    }
}
