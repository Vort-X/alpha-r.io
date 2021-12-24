using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io.shared.UdpGraph
{
    [Serializable]
    public class AreaPartNode : Node
    {
        public int XLeft { get; set; }
        public int YTop { get; set; }
        public List<CircleGameObjectNode> KillableObjects { get; set; }
    }
}
