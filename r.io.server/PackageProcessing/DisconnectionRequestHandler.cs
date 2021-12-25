using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using System.Net.Sockets;

namespace r.io.server.PackageProcessing
{
    class DisconnectionRequestHandler : RequestHandler
    {
        private ConnectionService connectionService;

        public override char Type => 'd';

        public override GameServiceCollection GameServices
        {
            set
            {
                connectionService = value.Get<ConnectionService>();
            }
        }

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            connectionService.Remove(result.RemoteEndPoint);
        }
    }
}
