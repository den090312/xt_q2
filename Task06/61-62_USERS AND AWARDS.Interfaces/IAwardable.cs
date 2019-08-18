﻿using _61_62_USERS_AND_AWARDS.Entities;

namespace _61_62_USERS_AND_AWARDS.Interfaces
{
    public interface IAwardable
    {
        void CreateStorage();

        void PrintStorageInfo();

        void AddAward(Award award);

        void RemoveAward(string awardName);

        bool AwardExists(string awardName);

        void PrintAwards();

        void AddUserToAward(string user, string awardName);

        string GetID(string awardName);
    }
}
