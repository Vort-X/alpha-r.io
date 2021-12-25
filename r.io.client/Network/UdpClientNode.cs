using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Godot;
using r.io.shared;
using r.io.shared.UdpGraph;
using Node = Godot.Node;

namespace R.io.client.Network
{
	public class UdpClientNode : Node
	{
		[Export] private int LocalPort { get; set; } = 5555;
		[Export] private string Host { get; set; } = "127.0.0.1";
		[Export] private int HostPort { get; set; } = 4096;
		private UdpClient Client { get; set; }
		private Serializer<UdpPackage> _serializer;

		private event Action<NearbyAreasNode> OnNearby;

		public override void _Ready()
		{
			Client = new UdpClient(LocalPort);
			_serializer = new Serializer<UdpPackage>();
		}

		public override void _Process(float delta)
		{
			if (Client.Available > 0)
			{
				var result = Client.ReceiveAsync().Result;

				var package = _serializer.Deserialize(result.Buffer);

				switch(package.Node)
				{
					case NearbyAreasNode nearby:
						OnNearby?.Invoke(nearby);
						break;
				}
				
				GD.Print(package.Type);
			}
		}

		public void Send(UdpPackage package)
		{
			var data = _serializer.Serialize(package);
			GD.Print($"{Host}/{HostPort} username: {(package.Node as UsernameNode).Username}");
			Client.Send(data, data.Length, Host, HostPort);
		}
		
		
	}
}
