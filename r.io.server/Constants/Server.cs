using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io.server.Constants
{
    //TODO: Move to config file
    static class Server
    {
        public static readonly int Port = 4096;
        public static readonly int TicksPerSecond = 60;
        public static readonly int SessionTimeout = 5 * 1000;
    }
}
