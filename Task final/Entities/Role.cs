using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Role
    {
        public string Name { get; set; }

        public bool NomenclatureRead { get; set; }

        public bool NomenclatureWrite { get; set; }

        public bool OrderRead { get; set; }

        public bool OrderWrite { get; set; }

        public bool RoleRead { get; set; }

        public bool RoleWrite { get; set; }

        public bool UserRead { get; set; }

        public bool UserWrite { get; set; }

        public Role(string name)
        {
            NullCheck(name);
            EmptyStringCheck(name);

            Name = name;
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
