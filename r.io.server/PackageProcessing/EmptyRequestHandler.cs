using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using System.Net.Sockets;

namespace r.io.server.PackageProcessing
{
    class EmptyRequestHandler : RequestHandler
    {
        public override char Type => 'e';

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            gameServices.Get<ConnectionService>().Get(result.RemoteEndPoint)?.UpdateLastPing();
        }
    }
}
