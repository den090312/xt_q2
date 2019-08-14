using _61_62_USERS_AND_AWARDS.DAL;
using _61_62_USERS_AND_AWARDS.Interfaces;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    class Buffer : IInfoble
    {
        public void WriteInfo()
        {
            Storage.WriteInfo();
        }
    }
}
