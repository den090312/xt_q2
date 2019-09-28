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
            NullCheck(customer);
            DateCheck(date);
            NullCheck(adress);
            EmptyStringCheck(adress);

            Customer = customer;
            Date = date;
            Adress = adress;
            CurrentStatus = Status.Opened;
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }

        private void DateCheck(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                throw new ArgumentException($"{nameof(date)} is empty!");
            }

            if (date < DateTime.Now)
            {
                throw new ArgumentException($"{nameof(date)} must be grater than current date!");
            }
        }
    }
}
