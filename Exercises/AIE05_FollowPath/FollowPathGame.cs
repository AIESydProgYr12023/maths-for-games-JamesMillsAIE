using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE05_FollowPath
{
	public class FollowPathGame : Game
	{
		private Vec2 playerPos = new Vec2(400, 255);
		private List<Vec2> path = new();

		public override void Load() { }

		public override void Draw()
		{
			DrawPath();
			DrawPlayer();
		}

		private void DrawPath()
		{
			for(int i = 1; i < path.Count; i++)
				Raylib.DrawLineV(path[i], path[i - 1], Color.BLACK);

			foreach(Vec2 point in path)
				Raylib.DrawCircleV(point, 3, Color.BLACK);
		}

		private void DrawPlayer()
		{
			float radius = 10;
			Raylib.DrawCircleV(playerPos, radius, Color.RED);

			if(path.Count > 0)
			{
				Vec2 endPointLine = playerPos + (path[0] - playerPos).Normalized * radius;
				Raylib.DrawLineEx(playerPos, endPointLine, 2, Color.BLACK);
			}
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
				path.Add(Raylib.GetMousePosition());

			if(path.Count > 0)
			{
				Vec2 target = path[0];
				Vec2 toTargetPos = (target - playerPos).Normalized;

				playerPos += toTargetPos * _deltaTime * 50;

				if(Vec2.Distance(target, playerPos) < 1f)
				{
					path.RemoveAt(0);
				}
			}
		}

		public override void Unload() { }
	}
}