using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.DAL;

namespace _61_62_USERS_AND_AWARDS.Common
{
    public static class Dependencies 
    {
        public static IStorable StorageImplementation { get; }

        public static IUserable UserImplementation { get; }

        public static IAwardable AwardImplementation { get; }

        static Dependencies()
        {
            StorageImplementation = new FileStorage();
            UserImplementation = new UserFileStorage();
        }
    }
}
