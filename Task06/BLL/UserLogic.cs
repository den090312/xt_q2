using InterfacesBLL;
using InterfacesDAL;
using Entities;
using System;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDFO iUserDAO;

        public UserLogic(IUserDFO iUserDAO)
        {
            this.iUserDAO = iUserDAO;
        }

        public User CreateUser(string name, DateTime dateBirth)
        {
            NullCheck(name);
            CheckDateOfBirth(dateBirth);

            return new User(name, dateBirth);
        }

        public void AddUser(User user)
        {
            NullCheck(user);

            iUserDAO.AddUser(user);
        }

        public void EraseUser(string userID)
        {
            throw new NotImplementedException();
        }

        public string[] GetUserIDArray(string userName)
        {
            NullCheck(userName);

            throw new NotImplementedException();
        }

        public void PrintUsers()
        {
            throw new NotImplementedException();
        }

        public void RemoveUsers(string userName)
        {
            NullCheck(userName);

            throw new NotImplementedException();
        }

        public bool UsersExists(string userName)
        {
            NullCheck(userName);

            throw new NotImplementedException();
        }

        private static void CheckDateOfBirth(DateTime birthDate)
        {
            DateTime currentDateTime = DateTime.Now.Date;

            if (birthDate > currentDateTime)
            {
                throw new ArgumentException("Date of birth can't be more than current date!");
            }

            if (birthDate == currentDateTime)
            {
                throw new ArgumentException("Welcome to our world!");
            }
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
