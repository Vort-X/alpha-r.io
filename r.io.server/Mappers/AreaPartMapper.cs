using r.io.model.GameEntities;
using r.io.shared.UdpGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io.server.Mappers
{
    internal static class AreaPartMapper
    {
        public static AreaPartNode ToNode(this AreaPart entity)
        {
            var node = new AreaPartNode
            {
                XLeft = entity.xLeft,
                YTop = entity.yTop,
                KillableObjects = entity.killableObjects.Select(o => o.ToNode()).ToList(),
            };
            return node;
        }
    }
}
