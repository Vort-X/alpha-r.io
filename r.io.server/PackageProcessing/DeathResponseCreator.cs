using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using r.io.shared.UdpGraph;
using System.Linq;
using System.Threading.Tasks;

namespace r.io.server.PackageProcessing
{
    class DeathResponseCreator : ResponseCreator
    {
        private BroadcastService broadcastService;
        private ConnectionService connectionService;
        private Serializer<UdpPackage> serializer;

        public override GameServiceCollection GameServices 
        { 
            set
            {
                broadcastService = value.Get<BroadcastService>();
                connectionService = value.Get<ConnectionService>();
                serializer = value.Get<Serializer<UdpPackage>>();
            }
        }

        public override int Priority => 2;

        public override char Type => 'x';

        public override Task[] Broadcast()
        {
            var dead = connectionService.Connected.Where(conn => !conn.Player.isAlive).ToList();
            Task[] tasks = dead.Select(conn =>
                           {
                               var pack = new UdpPackage() { Type = Type };
                               pack.Node = new PlayerDeathNode();
                               var data = serializer.Serialize(pack);
                               return broadcastService.Send(conn.EndPoint, data);
                           }
                        ).ToArray();
            foreach (var conn in dead) connectionService.Remove(conn.EndPoint);
            return tasks;
        }
    }
}
