using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.DAL;

namespace _61_62_USERS_AND_AWARDS.Common
{
    public static class Dependencies 
    {
        public static IUserable UserImplementation { get; }

        public static IAwardable AwardImplementation { get; }

        static Dependencies()
        {
            AwardImplementation = new AwardFileStrorage();
            UserImplementation = new UserFileStorage();
        }
    }
}
