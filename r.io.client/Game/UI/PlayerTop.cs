using System;
using System.Linq;
using Godot;
using R.io.client.Network;

namespace R.io.client.Game.UI
{
	public class PlayerTop : Node
	{
		public RichTextLabel _label;

		public override void _Ready()
		{
			_label = GetNode<RichTextLabel>("RichTextLabel");
			
			GetNode<UdpClientNode>("/root/UdpClient").OnTopPlayers += node =>
			{
				_label.Text = $" Top Players: {node.Players.Take(5).Select(n => $" {n.Key}: {(int)(100*n.Value.Radius)}").Aggregate("\n", (s1, s2) => s1+s2 + "\n")}";
				GetNode<PlayerState>("/root/MenuState").TopPlayers = _label.Text;
			};
		}
		
		
	}
}
