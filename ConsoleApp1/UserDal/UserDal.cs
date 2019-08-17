using DALInterafces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDalFileSystem
{
    public class UserDal : IUserDal
    {
        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void ChangeUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<global::Entities.User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        internal string GetStoragePath()
        {
            throw new NotImplementedException();
        }

        private string MapUserToSring(User user)
        {
            throw new NotImplementedException();
        }

        private User MapUserFromString(string user)
        {
            throw new NotImplementedException();

        }
    }
}
