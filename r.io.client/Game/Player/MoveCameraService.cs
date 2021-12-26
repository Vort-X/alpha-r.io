using Godot;
using System;
using System.Linq;
using R.io.client;
using R.io.client.Network;

public class MoveCameraService : Node2D
{
	private PlayerState _playerState;
	
	public override void _Ready()
	{
		_playerState = GetNode<PlayerState>("/root/MenuState");
		GetNode<UdpClientNode>("/root/UdpClient").OnNearby += node =>
		{
			if (!node.Players.ContainsKey(_playerState.UserName)) return;
			var player = node.Players
				.FirstOrDefault(p => p.Key == _playerState.UserName)
				.Value;

			Position = new Vector2((float) player.X, (float) player.Y);
		};
	}
}
