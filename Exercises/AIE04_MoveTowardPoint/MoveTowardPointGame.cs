using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE04_MoveTowardPoint
{
	public class MoveTowardPointGame : Game
	{
		private Vec2 playerPos = new(400, 225);
		private Vec2 targetPos = new(500, 225);
		
		public override void Load() { }

		public override void Draw()
		{
			DrawTarget();
			DrawPlayer();
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
				targetPos = Raylib.GetMousePosition();

			if(playerPos == targetPos)
				return;

			Vec2 toTargetPos = (targetPos - playerPos).Normalized;
			playerPos += toTargetPos * _deltaTime * 50;
		}

		private void DrawPlayer()
		{
			float radius = 10;
			
			Raylib.DrawCircleV(playerPos, radius, Color.RED);

			Vec2 endPointLine = playerPos + (targetPos - playerPos).Normalized * radius;
			Raylib.DrawLineEx(playerPos, endPointLine, 2, Color.BLACK);
		}

		private void DrawTarget()
		{
			Raylib.DrawCircleV(targetPos, 3, Color.BLUE);
		}

		public override void Unload() { }
	}
}