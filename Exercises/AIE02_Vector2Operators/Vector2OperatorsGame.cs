using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE02_Vector2Operators
{
	public class Vector2OperatorsGame : Game
	{
		private Vec2 playerPos = Vec2.zero;

		public override void Load()
		{
			Raylib.SetTargetFPS(60);
		}

		public override void Draw()
		{
			Raylib.DrawCircleV(playerPos, 3, Color.BLACK);
			
			Raylib.DrawText("Press Arrow Keys to move", 10, 10, 10, Color.BLACK);
			Raylib.DrawText("Press Left Mouse Button to set player pos to mouse pos.", 10, 30, 10, Color.BLACK);
		}

		public override void Update(float _deltaTime)
		{
			float speed = 10;
			Vec2 horizontal = Vec2.right * speed;
			Vec2 vertical = speed * Vec2.up;

			if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
			{
				playerPos = Raylib.GetMousePosition();
			}

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
				playerPos += horizontal;

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
				playerPos -= horizontal;

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
				playerPos += vertical;

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
				playerPos -= vertical;
		}

		public override void Unload() { }
	}
}