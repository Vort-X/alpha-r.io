using r.io.shared;
using r.io.shared.PackageProcessing;

namespace r.io.server.PackageProcessing
{
    internal class TimeoutResponseCreator : ResponseCreator
    {
        public override char Type => 't';

        protected override void Configure(UdpPackage pack, params object[] @params)
        {
            
        }
    }
}
