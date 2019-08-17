using System;
using BllInterfaces;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALInterafces;

namespace UserLogic
{
    public class CrudLogic : IUserLogic
    {
        private IUserDal userDal;

        public CrudLogic(IUserDal userDal)
        {
            this.userDal = userDal ?? throw new ArgumentNullException("Can not find data access layer");
        }

        public void Add(User user)
        {
           throw new NotImplementedException();
        }

        public void ChangeUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers(int count = 50)
        {
            throw new NotImplementedException();
        }

        public Entities.User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public void DoSomething()
        {

        }
    }
}
