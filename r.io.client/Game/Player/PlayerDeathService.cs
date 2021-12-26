using Godot;
using R.io.client.Network;

namespace R.io.client.Game.Player
{
    public class PlayerDeathService : Node
    {
        private UdpClientNode _udpClient;

        public override void _Ready()
        {
            _udpClient = GetNode<UdpClientNode>("/root/UdpClient");

            _udpClient.OnPlayerDeath += () =>
            {
                GetNode<SceneManager.SceneManager>("/root/SceneManager").SwitchToResults();
            };
        }
    }
}