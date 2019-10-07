using System.Collections.Generic;
using Entities;

namespace InterfacesDAL
{
    public interface IFeedbackDao
    {
        bool Add(string name, string text);

        IEnumerable<Feedback> GetAll();
    }
}
