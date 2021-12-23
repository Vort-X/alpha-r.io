using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace r.io.server
{
    //ya ne znayu zachem ya napisal etot class
    class RequestQueue : IEnumerable<UdpReceiveResult>
    {
        private readonly Queue<UdpReceiveResult> q;

        public RequestQueue()
        {
            q = new();
        }

        public int Count => q.Count;

        public UdpReceiveResult Dequeue() => q.Dequeue();

        public void Enqueue(UdpReceiveResult request) => q.Enqueue(request);

        public IEnumerator<UdpReceiveResult> GetEnumerator() => q.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => q.GetEnumerator();
    }
}
