using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using r.io.shared.UdpGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace r.io.server.PackageProcessing
{
    class MoveRequestHandler : RequestHandler
    {
        private ConnectionService connectionService;
        private GameLoopManager gameLoopManager;

        public override char Type => 'm';

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
            if (pack.Node is not MoveNode) return;
            var conn = connectionService.Get(result.RemoteEndPoint);
            var move = pack.Node as MoveNode;
            gameLoopManager.playerService.Move(conn.Player, move.X, move.Y);
            conn.UpdateLastPing();
        }
    }
}
