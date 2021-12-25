using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io.shared.UdpGraph
{
    [Serializable]
    public class MoveNode : Node
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
