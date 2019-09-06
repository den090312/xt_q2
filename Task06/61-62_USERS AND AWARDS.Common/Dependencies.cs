using Task06.Interfaces;
using Task06.BLL;

namespace Task06.Common
{
    public static class Dependencies 
    {
        public static IUserable UserManager { get; }

        public static IAwardable AwardManager { get; }

        static Dependencies()
        {
            UserManager = new UserManager();
            AwardManager = new AwardManager();
        }
    }
}
