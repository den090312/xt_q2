using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        private readonly string password;

        public Role Role { get; }

        public string Name { get; }

        public string PasswordHash { get; }
    }
}