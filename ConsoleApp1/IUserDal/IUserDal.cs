using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALInterafces
{
    public interface IUserDal
    {
        void Add(User user);

        void Remove(int Id);

        User GetUserByName(string name);

        IEnumerable<User> GetAllUsers();

        void ChangeUser(User user);
    }
}
