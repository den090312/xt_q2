using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _51_BACKUP_SYSTEM
{
    class StorageFile : Storage
    {
        public override string FullName { get; protected set; }
        private byte[] byteArray = new byte[0];
    }
}
