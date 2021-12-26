using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using System;
using System.Threading.Tasks;

namespace r.io.server.PackageProcessing
{
    class TopPlayersResponseCreator : ResponseCreator
    {
        public override GameServiceCollection GameServices 
        { 
            set
            {

            }
        }

        public override int Priority => 3;

        public override char Type => 'e';

        public override Task[] Broadcast()
        {
            return Array.Empty<Task>();
        }
    }
}
