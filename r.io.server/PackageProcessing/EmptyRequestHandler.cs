using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using System.Net.Sockets;

namespace r.io.server.PackageProcessing
{
    class EmptyRequestHandler : RequestHandler
    {
        private ConnectionService connectionService;
        private GameLoopManager gameLoopManager;

        public override char Type => 'e';

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
            var conn = connectionService.Get(result.RemoteEndPoint);
            gameLoopManager.playerService.TryEat(conn.Player);
            connectionService.Get(result.RemoteEndPoint)?.UpdateLastPing();
        }
    }
}
