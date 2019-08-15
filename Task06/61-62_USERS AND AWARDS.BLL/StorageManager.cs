using _61_62_USERS_AND_AWARDS.DAL;
using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Common;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public class StorageManager : ICreateble, IInfoble
    {
        public void Create() => Storage.Create();

        public void WriteInfo() => Storage.WriteInfo();
    }
}
