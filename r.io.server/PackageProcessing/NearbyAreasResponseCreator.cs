using r.io.model.GameEntities;
using r.io.model.Services.Abstract;
using r.io.server.Mappers;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.UdpGraph;
using System.Linq;
using System.Threading.Tasks;

namespace r.io.server.PackageProcessing
{
    internal class NearbyAreasResponseCreator : ResponseCreator
    {
        public override char Type => 'n';

        public override int Priority => 1;

        public override Task[] Broadcast()
        {
            var bs = gameServices.Get<BroadcastService>();
            var cs = gameServices.Get<ConnectionService>();
            var s = gameServices.Get<Serializer<UdpPackage>>();
            var ps = gameServices.Get<GameLoopManager>().playerService;
            return cs.Connected.Select(conn =>
            {
                var pack = new UdpPackage() { Type = Type };
                var areas = ps.getGameAreasAround(conn.Player.x, conn.Player.y);
                pack.Node = new NearbyAreasNode()
                {
                    AreaParts = areas.Select(a => a.ToNode()).ToList(),
                };
                var data = s.Serialize(pack);
                return bs.Send(conn.EndPoint, data);
            }).ToArray();
        }
    }
}
