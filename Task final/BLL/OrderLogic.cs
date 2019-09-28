using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderLogic
    {
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
