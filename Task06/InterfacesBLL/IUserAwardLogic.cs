﻿using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IUserAwardLogic
    {
        bool JoinedAwardToUser(Guid userGuid, Guid awardGuid);

        IEnumerable<UserAward> GetAll();
    }
}