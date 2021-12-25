using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
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
        public override char Type => 'm';

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            if (pack.Node is not MoveNode) return;
            var cs = gameServices.Get<ConnectionService>();
            var conn = cs.Get(result.RemoteEndPoint);
            var move = pack.Node as MoveNode;
            gameServices.Get<PlayerService>().Move(conn.Player, move.X, move.Y);
            conn.UpdateLastPing();
        }
    }
}
