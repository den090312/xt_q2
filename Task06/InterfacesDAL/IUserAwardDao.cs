﻿namespace InterfacesDAL
{
    public interface IUserAwardDao
    {
        void JoinAwardsToUsers(string[] userIdArray, string[] awardIdArray);

        void PrintUsersAwards(string[] userLines, string[] awardLines);
    }
}