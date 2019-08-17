using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Common;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class StorageManager : IStorable
    {
        private static readonly IStorable implementation;

        public void CreateStorage() => implementation.CreateStorage();

        public void PrintStorageInfo() => implementation.PrintStorageInfo();

        static StorageManager() => implementation = Dependencies.StorageImplementation;
    }
}
