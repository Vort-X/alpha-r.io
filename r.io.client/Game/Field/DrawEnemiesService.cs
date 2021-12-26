using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using R.io.client.Network;
using r.io.shared.UdpGraph;
using Node = Godot.Node;

namespace R.io.client.Game.Field
{
	public class DrawEnemiesService : Node
	{
		[Export] private int EnemiesBufferSize { get; set; } = 100;
		private List<PlayerCircle> _enemiesCircle = new List<PlayerCircle>();
		private PackedScene playerCircleScene = (PackedScene)ResourceLoader.Load("res://Game/Player/PlayerCircle.tscn");
		
		private PlayerState _playerState;

		public override void _Ready()
		{
			_playerState = GetNode<PlayerState>("/root/MenuState");
			
			for (var i = 0; i < EnemiesBufferSize; i++)
			{
				var enemy = playerCircleScene.Instance<PlayerCircle>();
				enemy.Color = Colors.Chocolate;
				enemy.Hide();
				
				_enemiesCircle.Add(enemy);
				
				AddChild(enemy);
			}
			
			GetNode<UdpClientNode>("/root/UdpClient").OnNearby += node =>
			{
			   DrawEnemies(node.Players
				   .Where(_ => _.Key != _playerState.UserName)
				   .ToList());
			};
		}

		public void DrawEnemies(List<KeyValuePair<string, CircleGameObjectNode>> enemies)
		{
			_enemiesCircle.ForEach(_ => _.Hide());

			var enemiesCount = Math.Min(EnemiesBufferSize, enemies.Count);

			for (var i = 0; i < enemiesCount; i++)
			{
				var c = _enemiesCircle[i];

				c.Radius = (float)enemies[i].Value.Radius;
				c.Position = new Vector2((float) enemies[i].Value.X, (float) enemies[i].Value.Y);
				c.Nickname = enemies[i].Key;
				c.Show();
				c.Update();
			}
		}
	}
}
