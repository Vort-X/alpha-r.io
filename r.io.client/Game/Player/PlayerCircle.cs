using Godot;
using System;

public class PlayerCircle : Node2D
{
	public float Radius { get; set; }
	public Color Color { get; set; }
	public string Nickname {get; set; }

	public RichTextLabel _label;
	
	public override void _Ready()
	{
		_label = GetNode<RichTextLabel>("Font/RichTextLabel");
	}

	public override void _Process(float delta)
	{
		Update();
		_label.BbcodeText = $"[center]{Nickname}[/center]";
	}

	public override void _Draw()
	{
		DrawCircle(Vector2.Zero, Radius, Color);
	}
}
