using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.UdpGraph;
using System.Net.Sockets;

namespace r.io.server.PackageProcessing
{
    class ConnectionRequestHandler : RequestHandler
    {
        public override char Type => 'c';

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            if (pack.Node is not UsernameNode) return;
            var cs = gameServices.Get<ConnectionService>();
            var conn = cs.Add(result.RemoteEndPoint);
            conn.Player = gameServices.Get<GameLoopManager>()
                .gameService
                .RegisterPlayer((pack.Node as UsernameNode).Username);
            conn.UpdateLastPing();
        }
    }
}
