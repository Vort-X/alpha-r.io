using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using r.io.shared.UdpGraph;
using System.Net.Sockets;

namespace r.io.server.PackageProcessing
{
    class ConnectionRequestHandler : RequestHandler
    {
        private ConnectionService connectionService;
        private GameService gameService;

        public override char Type => 'c';

        public override GameServiceCollection GameServices 
        {
            set 
            {
                connectionService = value.Get<ConnectionService>();
                gameService = value.Get<GameLoopManager>().gameService;
            }
        }

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            if (pack.Node is not UsernameNode) return;
            var conn = connectionService.Add(result.RemoteEndPoint);
            conn.Player = gameService.RegisterPlayer((pack.Node as UsernameNode).Username);
            conn.UpdateLastPing();
        }
    }
}
