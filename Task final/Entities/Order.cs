using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int IdCustomer { get; }

        public int IdManager { get; set; }

        public DateTime Date { get; }

        public string Adress { get; }

        public List<int> ListIdProduct { get; set; }

        public Status CurrentStatus { get; set; }

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

        public Order(int idCustomer, DateTime date, string adress)
        {
            IdCustomer = idCustomer;
            Date = date;
            Adress = adress;
            CurrentStatus = Status.Opened;
        }
    }
}
