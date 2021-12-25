using System;
using System.Collections.Generic;

namespace r.io.shared.UdpGraph
{
    [Serializable]
    public class NearbyAreasNode : Node
    {
        public List<CircleGameObjectNode> Foodes { get; set; }
        public Dictionary<string, CircleGameObjectNode> Players { get; set; }
    }
}
