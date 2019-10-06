using Entities;

namespace InterfacesDAL
{
    public interface ICustomerDao
    {
        bool Add(ref Customer customer);

        Customer GetByIdUser(int idUser);
    }
}