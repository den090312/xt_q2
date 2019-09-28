using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Manager
    {
        public string Name { get; }

        public User User { get; }

        public List<Order> Orders { get; set; }

        public enum Rank
        {
            None = 0,
            Junior = 1,
            Middle = 2,
            Top = 3,
            General = 4
        }

        public Manager(string name, User user)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            NullCheck(user);

            Name = name;
            User = user;
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
