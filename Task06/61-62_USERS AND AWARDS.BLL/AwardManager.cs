using _61_62_USERS_AND_AWARDS.Entities;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class AwardManager 
    {
        public static void CreateAward(string name)
        {
            StorageManager.NullCheck(name);
            StorageManager.AddAward(new Award(name));
        }

        public static void DeleteAward(string name)
        {
            StorageManager.NullCheck(name);
            StorageManager.RemoveAward(name);
        }

        public static void PrintAllAwards() => StorageManager.PrintAllAwards();
    }
}
