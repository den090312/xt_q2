using Task06.Interfaces;
using Task06.DAL;

namespace Task06.Common
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
