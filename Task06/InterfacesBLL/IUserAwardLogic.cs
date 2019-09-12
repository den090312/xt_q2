using System;

namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        bool JoinedAwardToUser(Guid userGuid, Guid awardGuid);
    }
}