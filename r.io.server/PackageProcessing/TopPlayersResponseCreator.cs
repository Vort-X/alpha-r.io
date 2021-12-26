using r.io.model.Services.Abstract;
using r.io.server.Mappers;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using r.io.shared.UdpGraph;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace r.io.server.PackageProcessing
{
    class TopPlayersResponseCreator : ResponseCreator
    {
        private BroadcastService broadcastService;
        private ConnectionService connectionService;
        private GameLoopManager gameLoopManager;
        private Serializer<UdpPackage> serializer;

        public override GameServiceCollection GameServices 
        { 
            set
            {
                broadcastService = value.Get<BroadcastService>();
                connectionService = value.Get<ConnectionService>();
                gameLoopManager = value.Get<GameLoopManager>();
                serializer = value.Get<Serializer<UdpPackage>>();
            }
        }

        public override int Priority => 3;

        public override char Type => 'e';

        public override Task[] Broadcast()
        {
            return connectionService.Connected.Select(conn =>
            {
                var pack = new UdpPackage() { Type = Type };
                var top = gameLoopManager.gameService.getTopPlayers(10);
                pack.Node = new TopPlayersNode()
                {
                    Players = new(top.Select(p => KeyValuePair.Create(p.name, p.ToNode()))),
                };
                var data = serializer.Serialize(pack);
                return broadcastService.Send(conn.EndPoint, data);
            }
            ).ToArray();
        }
    }
}
