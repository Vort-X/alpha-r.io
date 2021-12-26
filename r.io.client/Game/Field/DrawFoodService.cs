using System.Collections.Generic;
using System.Linq;
using Godot;
using R.io.client.Network;
using r.io.shared.UdpGraph;
using Node = Godot.Node;

namespace R.io.client.Game.Field
{
	public class DrawFoodService: Node2D
	{
		private IEnumerable<CircleGameObjectNode> _food = new List<CircleGameObjectNode>();

		public override void _Ready()
		{
			GetNode<UdpClientNode>("/root/UdpClient").OnNearby += node =>
			{
				_food = node.Food;
			};
		}

		public override void _Process(float delta)
		{
			Update();
		}

		public override void _Draw()
		{
			DrawFood();
		}

		private void DrawFood()
		{
			foreach (var f in _food)
			{
				var cen = new Vector2((float) f.X, (float) f.Y);
				var col = new Color(1, 0, 0);
				DrawCircle(cen, 0.5f*(float)f.Radius, col);
			}
		}
	}
}
