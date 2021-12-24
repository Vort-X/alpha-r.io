using r.io.model.GameEntities;
using r.io.model.Services.Abstract;
using r.io.server.Mappers;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.UdpGraph;
using System.Linq;

namespace r.io.server.PackageProcessing
{
    internal class NearbyAreasResponseCreator : ResponseCreator
    {
        public override char Type => 'n';

        protected override void Configure(UdpPackage pack, params object[] @params)
        {
            var player = @params[0] as PlayerCircle;
            var areas = gameServices.Get<GameLoopManager>().playerService.getGameAreasAround(player.x, player.y);
            pack["areas"] = new NearbyAreasNode()
            {
                AreaParts = areas.Select(a => a.ToNode()).ToList(),
            };
        }
    }
}
