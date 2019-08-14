using _61_62_USERS_AND_AWARDS.DAL;
using _61_62_USERS_AND_AWARDS.Interfaces;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class StorageManager : IInfoble
    {
        public void WriteInfo()
        {
            Storage.WriteInfo();
        }
    }
}
