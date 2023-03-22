using Azimuth.GameObjects;
using Azimuth.GameStates;
using Azimuth.UI;

using Raylib_cs;

namespace Azimuth
{
	public sealed class Application
	{
		public static Application? Instance { get; private set; }

		public static void Run<GAME>() where GAME : Game, new()
		{
			if(Instance != null)
			{
				Console.WriteLine("[Error] Attempted to run application more than once!");

				return;
			}
			
			Instance = new Application(new GAME());
			Instance.Run();
		}

		public static void Quit() => Instance!.running = false;
		
		public Window Window { get; }

		private readonly Game game;
		private bool running = true;

		private Application(Game _game)
		{
			Config.Create();
			Window = new Window();
			game = _game;
		}

		private void Run()
		{
			Window.Open();
			Assets.Load();
			
			game.Load();

			while(running)
			{
				float deltaTime = Raylib.GetFrameTime();
				game.Update(deltaTime);
				
				GameObjectManager.Update(deltaTime);
				GameStateManager.Update(deltaTime);
				
				UIManager.Update();
				
				Raylib.BeginDrawing();
				Window.Clear();
				
				game.Draw();
				
				// Global drawing here
				GameObjectManager.Draw();
				GameStateManager.Draw();
				
				UIManager.Draw();
				
				Raylib.EndDrawing();

				if(Raylib.WindowShouldClose())
					running = false;
			}
			
			game.Unload();
			Assets.Unload();
			
			Window.Close();
		}
	}
}