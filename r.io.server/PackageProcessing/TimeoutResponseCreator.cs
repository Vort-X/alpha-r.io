using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using System.Linq;
using System.Threading.Tasks;

namespace r.io.server.PackageProcessing
{
    internal class TimeoutResponseCreator : ResponseCreator
    {
        public override char Type => 't';

        public override int Priority => 0;

        public override Task[] Broadcast()
        {
            var bs = gameServices.Get<BroadcastService>();
            var cs = gameServices.Get<ConnectionService>();
            var s = gameServices.Get<Serializer<UdpPackage>>();
            var pack = new UdpPackage() { Type = Type };
            var data = s.Serialize(pack);
            var tasks = cs.Timeouted.Select(conn => bs.Send(conn.EndPoint, data)).ToArray();
            cs.RemoveTimeouted();
            return tasks;
        }
    }
}
