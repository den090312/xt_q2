using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserAwardDaoDb : IUserAwardDao
    {
        public IEnumerable<Award> GetAwardsByUser(User user, IEnumerable<Award> awards)
        {
            throw new NotImplementedException();
        }

        public bool JoinAwardToUser(User user, Award award)
        {
            throw new NotImplementedException();
        }

        public string GetInfo() => "123";

        public bool RemoveAwardUsers(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUserAwards(Guid userGuid, IEnumerable<User> users, IEnumerable<Award> awards)
        {
            throw new NotImplementedException();
        }
    }
}
