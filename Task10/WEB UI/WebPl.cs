using Common;
using System;

namespace WEB_UI
{
    public static class WebPl
    {
        public static bool UserCreated(string userName, string dateOfBirth)
        {
            NullCheck(userName);

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

            if (!Guid.TryParse(userGuid, out Guid result))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.UserAwardsRemoved(result);
        }

        public static bool AwardCreated(string awardTitle)
        {
            NullCheck(awardTitle);

            var awardLogic = DependencyResolver.AwardLogic;
            var award = awardLogic?.CreateAward(awardTitle);

            return awardLogic.AwardAdded(award);
        }

        public static bool AwardUsersDeleted(string awardGuid)
        {
            NullCheck(awardGuid);

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

            if (!Guid.TryParse(userGuid, out Guid resultUserGuid) || !Guid.TryParse(awardGuid, out Guid resultAwardGuid))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.JoinedAwardToUser(resultUserGuid, resultAwardGuid);
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