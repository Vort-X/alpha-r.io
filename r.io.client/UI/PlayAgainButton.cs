using Godot;
using System;
using R.io.client.SceneManager;

public class PlayAgainButton : Button
{
	public override void _Ready()
	{
		
	}
	
	private void _on_ButtonMenu_pressed()
	{
		GetNode<SceneManager>("/root/SceneManager").SwitchToMenu();
	}
}



