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
            _label = GetNode<RichTextLabel>("Font/RichTextLabel");
            
            GetNode<UdpClientNode>("/root/UdpClient").OnTopPlayers += node =>
            {
                _label.Text = $"Top Players: \n {node.Players.Select(n => $"{n.Key}: {n.Value.Radius}\n)")}";
                GetNode<PlayerState>("/root/MenuState").TopPlayers = _label.Text;
            };
        }
        
        
    }
}