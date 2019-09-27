using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Manager
    {
        public int ManagerId { get; }

        public int IdUser { get; }

        public List<int> IdOrders { get;  }

        public string Name { get; }

        public enum Rank
        {
            None = 0,
            Junior = 1,
            Middle = 2,
            Top = 3,
            General = 4
        }
    }
}
