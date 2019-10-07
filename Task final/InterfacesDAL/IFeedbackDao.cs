using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDAL
{
    public interface IFeedbackDao
    {
        bool Add(string name, string text);
    }
}
