using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesBLL
{
    public interface IManagerLogic
    {
        bool Add(ref Manager manager);

        Manager GetByUserId(int idUser);

        bool IsManager(int idUser);
    }
}
