using System;
using System.Collections.Generic;

namespace r.io.shared
{
    [Serializable]
    public class UdpPackage
    {
        private readonly Dictionary<string, object> values;

        public UdpPackage()
        {
            values = new();
        }

        public object this[string index]
        {
            get 
            {
                values.TryGetValue(index, out object val);
                return val; 
            }
            set { values[index] = value; }
        }

        [System.Diagnostics.CodeAnalysis.NotNull]
        public string Type { get; set; }
    }
}
