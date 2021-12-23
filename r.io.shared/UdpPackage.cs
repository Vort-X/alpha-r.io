using r.io.shared.UdpGraph;
using System;
using System.Collections.Generic;

namespace r.io.shared
{
    [Serializable]
    public class UdpPackage
    {
        private readonly Dictionary<string, Node> values;

        public UdpPackage()
        {
            values = new();
        }

        public Node this[string index]
        {
            get 
            {
                values.TryGetValue(index, out Node val);
                return val; 
            }
            set { values[index] = value; }
        }

        [System.Diagnostics.CodeAnalysis.NotNull]
        public char Type { get; set; }
    }
}
