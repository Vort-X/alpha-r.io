using Godot;
using System;
using R.io.client;
using R.io.client.Network;
using R.io.client.SceneManager;

public class ConnectionButton : Button
{
	private MenuState _menu;
	
	public override void _Ready()
	{
		_menu = GetNode<MenuState>("/root/MenuState");
	}
	
	private void _on_Button_pressed()
	{
		var connectionService = new ConnectionService(GetNode<UdpClientNode>("/root/UdpClient"));
		
		connectionService.Connect(
			string.IsNullOrEmpty(_menu.UserName) ? 
				new Guid().ToString()[..8] 
				: _menu.UserName);
		
		GetNode<SceneManager>("/root/SceneManager").SwitchToGame();
	}
}


