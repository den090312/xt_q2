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

        public bool Remove(int UserId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateName(User user)
        {
            throw new NotImplementedException();
        }
    }
}
