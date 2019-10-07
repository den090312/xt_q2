using Entities;
using System.Collections.Generic;

namespace InterfacesBLL
{
    public interface IFeedbackLogic
    {
        bool Add(string name, string text);

        IEnumerable<Feedback> GetAll();
    }
}