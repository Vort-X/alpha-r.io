using System;
using System.Collections.Generic;

namespace r.io.shared.UdpGraph
{
    [Serializable]
    public class NearbyAreasNode : Node
    {
        public List<AreaPartNode> AreaParts { get; set; }
    }
}
