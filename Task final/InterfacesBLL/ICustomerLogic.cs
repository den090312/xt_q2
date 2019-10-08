using Entities;

namespace InterfacesBLL
{
    public interface ICustomerLogic
    {
        bool Add(ref Customer customer);

        Customer GetByUserId(int userId);
    }
}