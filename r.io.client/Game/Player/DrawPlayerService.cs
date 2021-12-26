using System.Linq;
using Godot;
using R.io.client.Network;
using r.io.shared.UdpGraph;

namespace R.io.client.Game.Player
{
	public class DrawPlayerService: Node2D
	{
		private PlayerCircle _player;
		private PlayerState _playerState;
		private PackedScene playerCircleScene = (PackedScene)ResourceLoader.Load("res://Game/Player/PlayerCircle.tscn");
		
		public override void _Ready()
		{
			_playerState = GetNode<PlayerState>("/root/MenuState");
			_player = playerCircleScene.Instance<PlayerCircle>();
			_player.Color = Colors.Fuchsia;
			AddChild(_player);
			
			GetNode<UdpClientNode>("/root/UdpClient").OnNearby += node =>
			{
				if (!node.Players.ContainsKey(_playerState.UserName)) return;
				var (username, circle) = node.Players
					.First(p => p.Key == _playerState.UserName);

				_playerState.PlayerScore = (float)circle.Radius;
				DrawPlayer(circle, username);
			};
		}

		public void DrawPlayer(CircleGameObjectNode p, string nickname)
		{
			GD.Print($"DrawPlayer radius:{p.Radius}");
			_player.Nickname = nickname;
			_player.Radius = (float) p.Radius;
			_player.Position = new Vector2((float) p.X, (float) p.Y);
			_player.Update();
		}
	}
}
