using Entities;
using InterfacesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AwardDaoDb : IAwardDao
    {
        public bool Add(Award award)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAll()
        {
            throw new NotImplementedException();
        }

        public Award GetByGuid(Guid awardGuid)
        {
            throw new NotImplementedException();
        }

        public void PrintInfo()
        {
            throw new NotImplementedException();
        }

        public bool RemoveByGuid(Guid awardGuid)
        {
            throw new NotImplementedException();
        }
    }
}
