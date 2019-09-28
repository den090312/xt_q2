using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public Customer Customer { get; }

        public Manager Manager { get; set; }

        public DateTime Date { get; }

        public string Adress { get; }

        public List<Nomenclature> ListNomenclature { get; set; }

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

        public Order(Customer customer, DateTime date, string adress)
        {
            Customer = customer;
            Date = date;
            Adress = adress;
            CurrentStatus = Status.Opened;
        }
    }
}
