using Godot;
using System;
using R.io.client;
using R.io.client.Network;
using R.io.client.SceneManager;

public class ConnectionButton : Button
{
	private PlayerState _player;
	
	public override void _Ready()
	{
		_player = GetNode<PlayerState>("/root/MenuState");
	}
	
	private void _on_Button_pressed()
	{
		var connectionService = new ConnectionService(GetNode<UdpClientNode>("/root/UdpClient"));
		
		connectionService.Connect(_player.UserName);
		GetNode<SceneManager>("/root/SceneManager").SwitchToGame();
	}
}


