using _61_62_USERS_AND_AWARDS.Entities;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IStorable
    {
        void Create();

        void PrintAllPaths();

        void AddUser(User user);
    }
}
