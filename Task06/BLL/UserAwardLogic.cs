using InterfacesBLL;
using System;

namespace BLL
{
    public class UserAwardLogic : IUserAwardLogic
    {
        public void JoinAwardToUser(string awardID, string userID)
        {
            NullCheck(awardID);
            NullCheck(userID);

            throw new NotImplementedException();
        }

        public void PrintUserAwards()
        {
            throw new NotImplementedException();
        }

        public bool RecordExists(string awardID, string userID)
        {
            NullCheck(awardID);
            NullCheck(userID);

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