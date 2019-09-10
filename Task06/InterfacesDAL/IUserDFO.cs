using Entities;

namespace InterfacesDAL
{
    public interface IUserDFO
    {
        void AddUser(User user);

        void RemoveUsers(string userName);

        void PrintUsers();

        void EraseUser(string userID);
    }
}
