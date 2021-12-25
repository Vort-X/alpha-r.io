using r.io.shared.UdpGraph;
using System;
using System.Collections.Generic;

namespace r.io.shared
{
    [Serializable]
    public class UdpPackage
    {
        public Node Node { get; set; }
        [System.Diagnostics.CodeAnalysis.NotNull]
        public char Type { get; set; }
    }
}
