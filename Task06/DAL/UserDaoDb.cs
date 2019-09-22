using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDaoDb : IUserDao
    {
        public bool Add(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByGuid(Guid userGuid)
        {
            throw new NotImplementedException();
        }

        public void PrintInfo()
        {
            throw new NotImplementedException();
        }

        public bool RemoveByGuid(Guid userGuid)
        {
            throw new NotImplementedException();
        }
    }
}
