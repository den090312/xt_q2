using _61_62_USERS_AND_AWARDS.Entities;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IStorable
    {
        void CreateStorage();

        void PrintStoragePaths();

        void AddUser(User user);

        void RemoveUser(string name);

        void PrintAllUsers();

        void AddAward(Award award);
    }
}
