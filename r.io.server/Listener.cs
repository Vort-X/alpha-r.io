using r.io.model.Services.Abstract;
using r.io.server.Constants;
using System;
using System.Net.Sockets;

namespace r.io.server
{
    public class Listener
    {
        internal GameLoopManager gameLoopManager;
        internal RequestProcessor requestProcessor;
        private readonly UdpClient client;

        internal Listener(UdpClient client)
        {
            this.client = client;
        }

        public void Start()
        {
            try
            {
                gameLoopManager.Start();
                requestProcessor.Start();
                while (true)
                {
                    try
                    {
                        var request = client.ReceiveAsync();
                        //awaiting result, current thread blocks until package received
                        var result = request.GetAwaiter().GetResult(); 
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
    }
}
