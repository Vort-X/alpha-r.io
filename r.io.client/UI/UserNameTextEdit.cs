using Godot;
using R.io.client;

public class UserNameTextEdit : TextEdit
{
	private PlayerState _player;

	public override void _Ready()
	{
		_player = GetNode<PlayerState>("/root/MenuState");
	}
	
	private void _on_TextEdit_text_changed()
	{
		_player.UserName = Text;
	}
}
