using r.io.shared.Services;
using System.Net.Sockets;

namespace r.io.shared.PackageProcessing
{
    public abstract class RequestHandler
    {
        public abstract GameServiceCollection GameServices { set; }
        public abstract char Type { get; }
        public abstract void Handle(UdpReceiveResult result, UdpPackage pack);
    }
}
