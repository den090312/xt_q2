using System.Collections.Generic;
using Entities;

namespace InterfacesDAL
{
    public interface IManagerDao
    {
        bool Add(ref Manager manager);

        Manager GetByIdUser(int idUser);

        bool IsManager(int idUser);

        IEnumerable<Manager> GetAll();
    }
}
