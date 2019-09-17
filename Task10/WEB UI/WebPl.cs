using Common;
using System;

namespace WEB_UI
{
    public static class WebPl
    {
        public static bool UserCreated(string userName, string dateOfBirth)
        {
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
            if (!Guid.TryParse(userGuid, out Guid result))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.UserAwardsRemoved(result);
        }

        public static bool AwardCreated(string awardTitle)
        {
            var awardLogic = DependencyResolver.AwardLogic;
            var award = awardLogic?.CreateAward(awardTitle);

            return awardLogic.AwardAdded(award);
        }

        public static bool AwardUsersDeleted(string awardGuid)
        {
            if (!Guid.TryParse(awardGuid, out Guid result))
            {
                return false;
            }

            return DependencyResolver.UserAwardLogic.AwardUsersRemoved(result);
        }
    }
}