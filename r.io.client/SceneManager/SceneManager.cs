using Godot;

namespace R.io.client.SceneManager
{
	public class SceneManager : Node
	{
		[Export] public PackedScene MenuScene { get; set; }
		[Export] public PackedScene GameScene { get; set; }
		[Export] public PackedScene ResultsScene { get; set; }

		public void SwitchToGame()
		{
			GetTree().ChangeScene(GameScene.ResourcePath);
		}
		
		public void SwitchToMenu()
		{
			GetTree().ChangeScene(MenuScene.ResourcePath);
		}

		public void SwitchToResults()
		{
			GetTree().ChangeScene(ResultsScene.ResourcePath);
		}
	}
}
