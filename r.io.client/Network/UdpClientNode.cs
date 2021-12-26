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

		private Vector2 _inputMovement = Vector2.Zero;

		public event Action<NearbyAreasNode> OnNearby;
		public event Action<TopPlayersNode> OnTopPlayers;
		public event Action OnPlayerDeath;

		public override void _Ready()
		{
			Client = new UdpClient();
			_serializer = new Serializer<UdpPackage>();
		}

		public override void _Process(float delta)
		{
			if (Client.Available > 0)
			{
				for (int i = 0; i < Client.Available; i++)
				{
					var result = Client.ReceiveAsync().Result;

					var package = _serializer.Deserialize(result.Buffer);

					switch (package.Node)
					{
						case NearbyAreasNode nearby:
							OnNearby?.Invoke(nearby);
							break;
						case TopPlayersNode topPlayers:
							OnTopPlayers?.Invoke(topPlayers);
							break;
						case PlayerDeathNode _:
							OnPlayerDeath?.Invoke();
							OnNearby = null;
							OnTopPlayers = null;
							OnPlayerDeath = null;
							return;
					}

					GD.Print(package.Type);
				}

				var move = new UdpPackage()
				{
					Type = 'm',
					Node = new MoveNode()
					{
						X = _inputMovement.x,
						Y = _inputMovement.y,
					}
				};

				Send(move);
			}
		}

		public void Send(UdpPackage package)
		{
			try
			{
				var data = _serializer.Serialize(package);
				Client.Send(data, data.Length, Host, HostPort);
			}
			catch (SocketException e) { }
		}

		public void SetMovementDirection(Vector2 movement)
		{
			_inputMovement = movement;
		}
	}
}
