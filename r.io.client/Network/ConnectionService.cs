using System.Net;
using System.Net.Sockets;
using Godot;
using r.io.shared;
using r.io.shared.UdpGraph;
using Node = Godot.Node;

namespace R.io.client.Network
{
    public class ConnectionService
    {
        private UdpClientNode Client { get; }
        
        public ConnectionService(UdpClientNode client)
        {
            Client = client;
        }
        
        public void Connect(string userName)
        {
            var package = new UdpPackage
            {
                Type = 'c',
                Node = new UsernameNode()
                {
                    Username = userName
                }
            };

            Client.Send(package);
        }
    }
}