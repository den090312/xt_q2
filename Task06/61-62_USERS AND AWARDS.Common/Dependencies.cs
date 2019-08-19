using Task06.Interfaces;
using Task06.DAL;

namespace Task06.Common
{
    public static class Dependencies 
    {
        public static IUserable UserImplement { get; }

        public static IAwardable AwardImplement { get; }

        static Dependencies()
        {
            AwardImplement = new AwardFileStrorage();
            UserImplement = new UserFileStorage();
        }
    }
}
