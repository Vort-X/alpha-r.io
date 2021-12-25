using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using System.Net.Sockets;

namespace r.io.server.PackageProcessing
{
    class DisconnectionRequestHandler : RequestHandler
    {
        public override char Type => 'd';

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            var cs = gameServices.Get<ConnectionService>();
            cs.Remove(result.RemoteEndPoint);
        }
    }
}
