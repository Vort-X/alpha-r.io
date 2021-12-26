using Godot;
using System;

public class ExitButton : Button
{
	
	public override void _Ready()
	{
		
	}
 
	private void _on_ButtonExit_pressed()
	{
		GetTree().Quit();
	}
}



