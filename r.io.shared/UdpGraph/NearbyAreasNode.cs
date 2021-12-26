using System;
using System.Collections.Generic;

namespace r.io.shared.UdpGraph
{
    [Serializable]
    public class NearbyAreasNode : Node
    {
        public string Username { get; set; }
        public List<CircleGameObjectNode> Food { get; set; }
        public Dictionary<string, CircleGameObjectNode> Players { get; set; }
    }
}
