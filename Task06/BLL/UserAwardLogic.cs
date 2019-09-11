using InterfacesBLL;
using System;

namespace BLL
{
    public class UserAwardLogic : IUserAwardLogic
    {
        public void JoinAwardsToUsers(string[] awardIdArray, string[] userIdArray)
        {
            NullCheck(awardIdArray);
            NullCheck(userIdArray);

            throw new NotImplementedException();
        }

        public void PrintUserAwards()
        {
            throw new NotImplementedException();
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}