using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.DAL;

namespace _61_62_USERS_AND_AWARDS.Common
{
    public static class Dependencies 
    {
        public static IStorable CurrentStorage { get; }

        static Dependencies() => CurrentStorage = new Storage();
    }
}
