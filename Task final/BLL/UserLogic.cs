using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UserLogic : IUserLogic, ILoggerLogic
    {
        private readonly IUserDao userDao;

        private readonly ILoggerDao loggerDao;

        private readonly static char hashSeparator = '|';

        public ILog Log => loggerDao.Log;

        public UserLogic(IUserDao iUserDao, ILoggerDao iLoggerDao)
        {
            NullCheck(iUserDao);
            NullCheck(iLoggerDao);

            userDao = iUserDao;
            loggerDao = iLoggerDao;
        }

        public void InitLogger() => loggerDao.StartLogger();

        public bool Add(int roleId, string name, string password)
        {
            IdCheck(roleId);

            NullCheck(name);
            EmptyStringCheck(name);

            NullCheck(password);
            EmptyStringCheck(password);

            var user = new User(roleId, name, GetHash(password));

            return userDao.Add(ref user);
        }

        public bool Add(ref User user, int roleId, string password)
        {
            NullCheck(user);
            EmptyStringCheck(user.Name);
            IdCheck(roleId);

            NullCheck(password);
            EmptyStringCheck(password);

            user.IdRole = roleId;
            user.PasswordHash = GetHash(password);

            return userDao.Add(ref user);
        }

        public bool ChangeName(User user, string newName)
        {
            NullCheck(user);
            IdCheck(user.Id);

            NullCheck(newName);
            EmptyStringCheck(newName);

            return userDao.UpdateName(user);
        }

        public IEnumerable<User> GetAll() => userDao.GetAll();

        public bool Remove(int UserId)
        {
            IdCheck(UserId);

            return userDao.Remove(UserId);
        }

        public User GetByName(string name)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            return userDao.GetByName(name);
        }

        public bool PasswordIsOk(string password, string hashcode)
        {
            NullCheck(password);
            EmptyStringCheck(password);

            NullCheck(hashcode);
            EmptyStringCheck(hashcode);

            return GetHash(password) == hashcode;
        }

        public bool ChangePassword(User user, string password)
        {
            NullCheck(user);

            NullCheck(password);
            EmptyStringCheck(password);

            return userDao.UpdatePasswordHash(user, GetHash(password));
        }

        private string GetHash(string password)
        {
            var listPass = new List<string>();
            var step = 4;

            for (var i = 0; i < password.Length; i += step)
            {
                listPass.Add(password.Substring(i, Math.Min(step, password.Length - i)));
            }

            return GetHashFromListPass(listPass, step);
        }

        private string GetHashFromListPass(List<string> listPass, int step)
        {
            var hashSb = new StringBuilder();

            for (var i = 0; i < listPass.Count; i++)
            {
                var bytes = Encoding.ASCII.GetBytes(AppendSpacesToString(listPass[i], step));

                hashSb.Append(BitConverter.ToInt32(bytes, 0));

                if (i == listPass.Count - 1)
                {
                    continue;
                }

                hashSb.Append(hashSeparator);
            }

            return hashSb.ToString();
        }

        private string AppendSpacesToString(string subString, int spaceCount)
        {
            if (subString.Length < spaceCount)
            {
                var sb = new StringBuilder(subString);

                while (sb.Length != spaceCount)
                {
                    sb.Append(" ");
                }

                subString = sb.ToString();
            }

            return subString;
        }

        private void IdCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} is incorrect!");
            }
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
