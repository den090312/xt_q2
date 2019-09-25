using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WEB_UI
{
    public static class Crud
    {
        public static bool UserCreate(string userName, string dateOfBirth)
        {
            NullCheck(userName);
            EmptyStringCheck(userName);

            NullCheck(dateOfBirth);
            EmptyStringCheck(dateOfBirth);

            if (!DateTime.TryParse(dateOfBirth, out DateTime result))
            {
                return false;
            }

            var userLogic = DependencyResolver.UserLogic;
            NullCheck(userLogic);

            var user = userLogic?.Create(userName, result);
            NullCheck(user);

            return userLogic.Add(user);
        }

        public static bool DeleteUserAwards(string userGuid)
        {
            NullCheck(userGuid);
            EmptyStringCheck(userGuid);

            if (!Guid.TryParse(userGuid, out Guid result))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.RemoveUserAwards(result);
        }

        public static bool AwardCreate(string awardTitle)
        {
            NullCheck(awardTitle);
            EmptyStringCheck(awardTitle);

            var awardLogic = DependencyResolver.AwardLogic;
            var award = awardLogic?.Create(awardTitle);

            return awardLogic.Add(award);
        }

        public static bool DeleteAwardUsers(string awardGuid)
        {
            NullCheck(awardGuid);
            EmptyStringCheck(awardGuid);

            if (!Guid.TryParse(awardGuid, out Guid result))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.RemoveAwardUsers(result);
        }

        public static bool JoinAwardToUser(string userGuid, string awardGuid)
        {
            NullCheck(userGuid);
            EmptyStringCheck(userGuid);

            NullCheck(awardGuid);
            EmptyStringCheck(awardGuid);

            if (userGuid == string.Empty || awardGuid == string.Empty)
            {
                return false;
            }

            if (!Guid.TryParse(userGuid, out Guid resultUserGuid) || !Guid.TryParse(awardGuid, out Guid resultAwardGuid))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.JoinAwardToUser(resultUserGuid, resultAwardGuid);
        }

        public static bool EditUsers(string[] guids, string[] names, string[] dates)
        {
            NullCheck(guids);
            NullCheck(names);
            NullCheck(dates);

            var usersAwards = DependencyResolver.UserAwardLogic.GetAll();

            NullCheck(usersAwards);

            if (!AllUsersDelete())
            {
                return false;
            }

            var i = 0;

            foreach (var name in names)
            {
                if (!UserAdded(guids[i], name, dates[i]))
                {
                    return false;
                }
                else
                {
                    i++;
                }
            }

            return ReJoinAwardToUser(usersAwards);
        }

        public static bool EditAwards(string[] guids, string[] titles)
        {
            NullCheck(guids);
            NullCheck(titles);

            var usersAwards = DependencyResolver.UserAwardLogic.GetAll();

            NullCheck(usersAwards);

            if (!AllAwardsDeleted())
            {
                return false;
            }

            var i = 0;

            foreach (var title in titles)
            {
                if (!AwardAdded(guids[i], title))
                {
                    return false;
                }
                else
                {
                    i++;
                }
            }

            return ReJoinAwardToUser(usersAwards);
        }

        private static bool ReJoinAwardToUser(IEnumerable<UserAward> usersAwards)
        {
            foreach (var userAward in usersAwards)
            {
                var awardGuid = userAward.AwardRef.Guid;
                var userGuid = userAward.UserRef.Guid;

                var userAwardLogic = DependencyResolver.UserAwardLogic;

                if (!userAwardLogic.JoinAwardToUser(userGuid, awardGuid))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool UserAdded(string guid, string name, string date)
        {
            if (guid == string.Empty || name == string.Empty || date == string.Empty)
            {
                return false;
            }

            if (!Guid.TryParse(guid, out Guid resultGuid) || !DateTime.TryParse(date, out DateTime resultDate))
            {
                return false;
            }

            var user = new User(resultGuid, name, resultDate);

            return DependencyResolver.UserLogic.Add(user);
        }

        private static bool AwardAdded(string guid, string title)
        {
            if (guid == string.Empty || title == string.Empty || !Guid.TryParse(guid, out Guid resultGuid))
            {
                return false;
            }

            var award = new Award(resultGuid, title);

            return DependencyResolver.AwardLogic.Add(award);
        }

        private static bool AllUsersDelete()
        {
            var userLogic = DependencyResolver.UserLogic;
            var allUsers = userLogic?.GetAll();

            foreach (var user in allUsers)
            {
                if (!userLogic.RemoveByGuid(user.Guid))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AllAwardsDeleted()
        {
            var awardLogic = DependencyResolver.AwardLogic;
            var allAwards = awardLogic?.GetAll();

            foreach (var award in allAwards)
            {
                if (!awardLogic.RemoveByGuid(award.Guid))
                {
                    return false;
                }
            }

            return true;
        }

        private static void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new Exception($"{nameof(inputString)} is empty!");
            }
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject == null)
            {
                throw new NullReferenceException($"{nameof(classObject)} is null!");
            }
        }
    }
}