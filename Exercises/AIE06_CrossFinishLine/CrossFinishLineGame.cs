using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE06_CrossFinishLine
{
	public class CrossFinishLineGame : Game
	{
		private const float MOVE_SPEED = 150f;
		private const float PLAYER_RADIUS = 10f;

		private Vec2 playerPos = new(400, 225);
		private Vec2 playerMoveDir = Vec2.zero;

		private readonly Rectangle finishLine = new(300, 100, 200, 25);
		private readonly Vec2 finishLineDir = Vec2.up;

		private int lapCount;

		private bool isInside;

		public override void Load() { }

		public override void Unload() { }

		public override void Update(float _deltaTime)
		{
			playerMoveDir = Vec2.zero;

			if(Raylib.IsKeyDown(KeyboardKey.KEY_W))
				playerMoveDir.y -= 1;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_S))
				playerMoveDir.y += 1;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_A))
				playerMoveDir.x -= 1;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_D))
				playerMoveDir.x += 1;
			
			playerMoveDir.Normalize();
			playerPos += playerMoveDir * _deltaTime * MOVE_SPEED;
			
			bool wasInside = isInside;
			float exitDir = Vec2.Dot(playerMoveDir, finishLineDir);
			isInside = Raylib.CheckCollisionPointRec(playerPos, finishLine);

			if(wasInside && !isInside)
			{
				if(exitDir > 0f)
				{
					lapCount++;
				}
				else
				{
					lapCount--;
				}
			}
		}

		public override void Draw()
		{
			DrawFinishLine();
			DrawPlayer();
			DrawUI();
		}

		private void DrawPlayer()
		{
			Raylib.DrawCircleV(playerPos, PLAYER_RADIUS, Color.RED);

			if(playerMoveDir.Magnitude() > 0)
			{
				Vec2 endLinePoint = playerPos + playerMoveDir * PLAYER_RADIUS;
				Raylib.DrawLineEx(playerPos, endLinePoint, 2, Color.BLACK);
			}
		}

		private void DrawFinishLine()
		{
			Raylib.DrawRectangleRec(finishLine, Color.GRAY);
			
			Raylib.DrawLineEx(new Vec2(finishLine.x, finishLine.y),
			                  new Vec2(finishLine.x + finishLine.width, finishLine.y),
			                  2,
			                  Color.BLACK);
			
			Raylib.DrawLineEx(new Vec2(finishLine.x + finishLine.width / 2, finishLine.y),
			                  new Vec2(finishLine.x + finishLine.width / 2, finishLine.y - 10),
			                  2,
			                  Color.BLACK);
		}

		// ReSharper disable once InconsistentNaming
		private void DrawUI()
		{
			Raylib.DrawText($"Laps: {lapCount}", 10, 10, 10, Color.BLACK);
		}
	}
}