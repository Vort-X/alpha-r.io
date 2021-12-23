using r.io.model.Services.Abstract;
using r.io.server.Constants;
using System;
using System.Net;
using System.Net.Sockets;

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
                    //TODO: find a way to handle disconnection w/o try-catch
                    try
                    {
                        var request = client.ReceiveAsync();
                        var result = request.GetAwaiter().GetResult(); //awaiting result, current thread blocks until package received
                        Console.WriteLine($"Receiving data at {DateTime.Now}");
                        requestProcessor.AddToQueue(result);
                    }
                    catch (SocketException)
                    {
                    }
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
