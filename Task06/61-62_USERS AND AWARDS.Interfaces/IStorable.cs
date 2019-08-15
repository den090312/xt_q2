using _61_62_USERS_AND_AWARDS.Entities;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IStorable
    {
        void CreateStorage();

        void PrintStoragePaths();

        void CreateUser(User user);

        void DeleteUser(string name);
    }
}
