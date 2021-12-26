using Godot;
using System;
using R.io.client;

public class PlayerScore : RichTextLabel
{
  
	public override void _Ready()
	{
		Text = $"Your Score: {GetNode<PlayerState>("/root/MenuState").PlayerScore}";
	}
}
