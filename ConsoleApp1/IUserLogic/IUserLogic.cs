using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllInterfaces
{
    public interface IUserLogic
    {
        void Add(User user);

        void Remove(int Id);

        User GetUserByName(string name);

        IEnumerable<User> GetUsers(int count = 50);

        void ChangeUser(User user);
    }
}
