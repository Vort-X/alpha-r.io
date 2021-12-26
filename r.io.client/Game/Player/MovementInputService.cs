using Godot;
using R.io.client.Network;
using r.io.shared;

namespace R.io.client.Game.Player
{
	public class MovementInputService : Node
	{
		private UdpClientNode _udpClient;

		public override void _Ready()
		{
			_udpClient = GetNode<UdpClientNode>("/root/UdpClient");
		}

		public override void _Process(float delta)
		{
			var horizontal = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
			var vertical = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

			var direction = new Vector2(horizontal, vertical).Normalized();
			
			
			_udpClient.SetMovementDirection(direction);
		}
	}
}
