using Godot;
using R.io.client;

public class UserNameTextEdit : TextEdit
{
	private MenuState _menu;

	public override void _Ready()
	{
		_menu = GetNode<MenuState>("/root/MenuState");
	}
	
	private void _on_TextEdit_text_changed()
	{
		_menu.UserName = Text;
	}
}
