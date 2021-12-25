using r.io.model.GameEntities;
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
    internal class NearbyAreasResponseCreator : ResponseCreator
    {
        private BroadcastService broadcastService;
        private ConnectionService connectionService;
        private GameLoopManager gameLoopManager;
        private Serializer<UdpPackage> serializer;

        public override int Priority => 1;
        public override char Type => 'n';
        
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

        public override Task[] Broadcast()
        {
            return connectionService.Connected.Select(conn =>
            {
                var pack = new UdpPackage() { Type = Type };
                var areas = gameLoopManager.playerService.getGameAreasAround(conn.Player.x, conn.Player.y);
                pack.Node = new NearbyAreasNode()
                {
                    Foodes = areas.SelectMany(a => a.killableObjects).Where(k => k is FoodCircle).Select(f => f.ToNode()).ToList(),
                    Players = new(areas.SelectMany(a => a.killableObjects)
                        .Where(k => k is PlayerCircle)
                        .Cast<PlayerCircle>()
                        .Select(p => KeyValuePair.Create(p.name, p.ToNode()))),
                };
                var data = serializer.Serialize(pack);
                return broadcastService.Send(conn.EndPoint, data);
            }
            ).ToArray();
        }
    }
}
