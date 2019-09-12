using System;

namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        bool JoinedAwardToUser(Guid userGuid, Guid awardGuid);
    }
}