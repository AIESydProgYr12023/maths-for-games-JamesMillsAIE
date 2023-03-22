using Azimuth;
using Azimuth.GameStates;

using TowerDefense.GameStates;

namespace TowerDefense
{
	public class TowerDefenseGame : Game
	{
		public const string PLAY_ID = "Play";

		public override void Load()
		{
			GameStateManager.AddState(new PlayState());
			
			GameStateManager.ActivateState(PLAY_ID);
		}

		public override void Unload() { }
	}
}