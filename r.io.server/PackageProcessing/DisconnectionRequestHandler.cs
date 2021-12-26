using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using System.Linq;
using System.Net.Sockets;

namespace r.io.server.PackageProcessing
{
    class DisconnectionRequestHandler : RequestHandler
    {
        private ConnectionService connectionService;
        private GameLoopManager gameLoopManager;

        public override char Type => 'd';

        public override GameServiceCollection GameServices
        {
            set
            {
                connectionService = value.Get<ConnectionService>();
                gameLoopManager = value.Get<GameLoopManager>();
            }
        }

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            var player = connectionService.Connected.First(conn => conn.EndPoint == result.RemoteEndPoint).Player;
            gameLoopManager.playerService.Kill(player);
            connectionService.Remove(result.RemoteEndPoint);
        }
    }
}
