using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public int OrderId { get; }

        public int IdCustomer { get; }

        public int IdManager { get; }

        public DateTime Date { get; }

        public int Code { get; }

        public string Adress { get; }

        public List<int> IdNomenclatures { get; }

        public Status CurrentStatus { get; }

        public enum Status
        {
            None = 0,
            Opened = 1,
            Processed = 2,
            Completed = 3,
            Canceled = 4,
            Returned = 5,
            Closed = 6
        }
    }
}
