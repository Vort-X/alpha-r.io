using r.io.model.Services.Abstract;
using r.io.server.Constants;
using r.io.shared;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace r.io.server
{
    public class Listener
    {
        internal GameLoopManager gameLoopManager;
        internal RequestProcessor requestProcessor;
        private UdpClient client;

        internal Listener()
        {
        }

        public void Start()
        {
            try
            {
                client = new UdpClient(Server.Port);
                requestProcessor.Send += OnSend;
                gameLoopManager.Start();
                requestProcessor.Start();
                while (true)
                {
                    var request = client.ReceiveAsync();
                    var result = request.Result; //awaiting result, current thread blocks until package received
                    Console.WriteLine($"Receiving data at {DateTime.Now}");
                    requestProcessor.AddToQueue(result);
                }
            }
            finally
            {
                client.Dispose();
            }
        }

        private void OnSend(IPEndPoint endpoint, byte[] package)
        {
            client.SendAsync(package, package.Length, endpoint);
        }
    }
}
