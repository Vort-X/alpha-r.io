using System;
using System.Collections.Generic;

namespace r.io.shared.UdpGraph
{
    [Serializable]
    public class TopPlayersNode: Node
    {
        public Dictionary<string, CircleGameObjectNode> Players { get; set; }
    }
}