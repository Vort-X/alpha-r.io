using r.io.server;
using System;

namespace r.io.clistarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Configurer.Create().Start();
        }
    }
}
