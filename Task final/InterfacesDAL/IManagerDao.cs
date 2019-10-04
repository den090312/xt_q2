using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace InterfacesDAL
{
    public interface IManagerDao
    {
        bool Add(ref Manager manager);

        Manager GetByIdUser(int idUser);

        bool IsManager(int idUser);
    }
}
