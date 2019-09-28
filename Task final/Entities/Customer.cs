using System;
using System.Collections.Generic;

namespace Entities
{
    public class Customer
    {
        public string Name { get; }

        public User User { get; }

        public List<Order> Orders { get; set; }

        public Customer(User user, string name)
        {
            NullCheck(user);
            NullCheck(name);
            EmptyStringCheck(name);

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
