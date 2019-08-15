using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Common;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class StorageManager
    {
        public static IStorable CurrentStorage { get; } = Dependencies.CurrentStorage;

        public static void Create() => CurrentStorage.Create();

        public static void PrintObjects() => CurrentStorage.PrintObjects();
    }
}
