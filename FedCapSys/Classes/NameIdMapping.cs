using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FedCapSys.Classes
{
    class NameIdMapping
    {
        public long ID = 0;
        public string Name = null;

        public override string ToString()
        {
            return Name;
        }
    }
}
