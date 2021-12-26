using Godot;
using System;
using R.io.client;

public class TopPlayersLabel : RichTextLabel
{
	
	public override void _Ready()
	{
		Text = $"{GetNode<PlayerState>("/root/MenuState").TopPlayers}";
	}

}
