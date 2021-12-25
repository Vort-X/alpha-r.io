using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using System.Linq;
using System.Threading.Tasks;

namespace r.io.server.PackageProcessing
{
    internal class TimeoutResponseCreator : ResponseCreator
    {
        private BroadcastService broadcastService;
        private ConnectionService connectionService;
        private Serializer<UdpPackage> serializer;

        public override int Priority => 0;
        public override char Type => 't';

        public override GameServiceCollection GameServices
        {
            set
            {
                broadcastService = value.Get<BroadcastService>();
                connectionService = value.Get<ConnectionService>();
                serializer = value.Get<Serializer<UdpPackage>>();
            }
        }

        public override Task[] Broadcast()
        {
            var pack = new UdpPackage() { Type = Type };
            var data = serializer.Serialize(pack);
            var tasks = connectionService.Timeouted
                .Select(conn => broadcastService.Send(conn.EndPoint, data))
                .ToArray();
            connectionService.RemoveTimeouted();
            return tasks;
        }
    }
}
