using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesBLL
{
    public interface IOrderLogic
    {
        IEnumerable<Order> GetByCustomerId(int customerId);

        IEnumerable<Order> GetByManagerId(int managerId);
    }
}
