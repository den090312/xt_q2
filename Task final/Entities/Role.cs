using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Role
    {
        public int RoleId { get; }

        public string Name { get; }

        public bool NomenclatureRead { get; }

        public bool NomenclatureWrite { get; }

        public bool OrderRead { get; }

        public bool OrderWrite { get; }

        public bool RoleRead { get; }

        public bool RoleWrite { get; }

        public bool UserRead { get; }

        public bool UserWrite { get; }
    }
}
