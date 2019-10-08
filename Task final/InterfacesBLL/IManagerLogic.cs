using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IManagerLogic
    {
        bool Add(ref Manager manager);

        Manager GetByUserId(int userId);

        IEnumerable<Manager> GetAll();
    }
}