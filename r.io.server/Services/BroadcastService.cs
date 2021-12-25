using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace r.io.server.Services
{
    class BroadcastService
    {
        public UdpClient Server { get; set; }

        public async Task Send(IPEndPoint endpoint, byte[] data)
        {
            await Server.SendAsync(data, data.Length, endpoint);
        }
    }
}
