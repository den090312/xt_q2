using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Common;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class StorageManager : IStorable
    {
        private static IStorable Implementation { get; } = Dependencies.StorageImplementation;

        public void CreateStorage() => Implementation.CreateStorage();

        public void PrintStorageInfo() => Implementation.PrintStorageInfo();
    }
}
