using _61_62_USERS_AND_AWARDS.Entities;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IUserable
    {
        void CreateStorage();

        void PrintStorageInfo();

        void AddUser(User user);

        void RemoveUser(string userName);

        bool UserExists(string userName);

        void PrintUsers();

        void AddAwardToUser(string awardName, string userName);
    }
}
