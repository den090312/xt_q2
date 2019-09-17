using Common;
using System;

namespace WEB_UI
{
    public static class Crud
    {
        public static bool UserCreated(string userName, string dateOfBirth)
        {
            NullCheck(userName);

            if (userName == string.Empty || dateOfBirth == string.Empty)
            {
                return false;
            }

            if (!DateTime.TryParse(dateOfBirth, out DateTime result))
            {
                return false;
            }

            var userLogic = DependencyResolver.UserLogic;
            var user = userLogic?.CreateUser(userName, result);

            return userLogic.UserAdded(user);
        }

        public static bool UserAwardsDeleted(string userGuid)
        {
            NullCheck(userGuid);

            if (userGuid == string.Empty)
            {
                return false;
            }

            if (!Guid.TryParse(userGuid, out Guid result))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.UserAwardsRemoved(result);
        }

        public static bool AwardCreated(string awardTitle)
        {
            NullCheck(awardTitle);

            if (awardTitle == string.Empty)
            {
                return false;
            }

            var awardLogic = DependencyResolver.AwardLogic;
            var award = awardLogic?.CreateAward(awardTitle);

            return awardLogic.AwardAdded(award);
        }

        public static bool AwardUsersDeleted(string awardGuid)
        {
            NullCheck(awardGuid);

            if (awardGuid == string.Empty)
            {
                return false;
            }

            if (!Guid.TryParse(awardGuid, out Guid result))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.AwardUsersRemoved(result);
        }

        public static bool JoinedAwardToUser(string userGuid, string awardGuid)
        {
            NullCheck(userGuid);
            NullCheck(awardGuid);

            if (userGuid == string.Empty || awardGuid == string.Empty)
            {
                return false;
            }

            if (!Guid.TryParse(userGuid, out Guid resultUserGuid) || !Guid.TryParse(awardGuid, out Guid resultAwardGuid))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.JoinedAwardToUser(resultUserGuid, resultAwardGuid);
        }

        public static bool UsersEdited(string[] guids, string[] names, string[] dates)
        {
            NullCheck(guids);
            NullCheck(names);
            NullCheck(dates);

            if (!AllUsersDeleted())
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

            return true;
        }

        public static bool AwardsEdited(string[] guids, string[] titles)
        {
            NullCheck(guids);
            NullCheck(titles);

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

            return true;
        }

        private static bool UserAdded(string guid, string name, string date)
        {
            NullCheck(guid);
            NullCheck(name);
            NullCheck(date);

            if (guid == string.Empty || name == string.Empty || date == string.Empty)
            {
                return false;
            }

            if (!Guid.TryParse(guid, out Guid resultGuid) || !DateTime.TryParse(date, out DateTime resultDate))
            {
                return false;
            }

            var user = new Entities.User(resultGuid, name, resultDate);

            return DependencyResolver.UserLogic.UserAdded(user);
        }

        private static bool AwardAdded(string guid, string title)
        {
            NullCheck(guid);
            NullCheck(title);

            if (guid == string.Empty || title == string.Empty || !Guid.TryParse(guid, out Guid resultGuid))
            {
                return false;
            }

            var award = new Entities.Award(resultGuid, title);

            return DependencyResolver.AwardLogic.AwardAdded(award);
        }

        private static bool AllUsersDeleted()
        {
            var userLogic = DependencyResolver.UserLogic;
            var allUsers = userLogic?.GetAll();

            foreach (var user in allUsers)
            {
                if (!userLogic.UserRemoved(user.UserGuid))
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
                if (!awardLogic.AwardRemoved(award.AwardGuid))
                {
                    return false;
                }
            }

            return true;
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