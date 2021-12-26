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
				var player = node.Players
					.FirstOrDefault(p => p.Key == _playerState.UserName);

				_playerState.PlayerScore = (float)player.Value.Radius;
				DrawPlayer(player.Value, player.Key);
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
