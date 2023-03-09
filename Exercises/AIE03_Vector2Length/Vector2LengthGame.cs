using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE03_Vector2Length
{
	public class Vector2LengthGame : Game
	{
		private float totalLength;
		private List<Vec2> points = new();

		public override void Load() { }

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
			{
				points.Add(Raylib.GetMousePosition());

				totalLength = 0;
				for(int i = 1; i < points.Count; i++)
					totalLength += (points[i] - points[i - 1]).Magnitude();
			}
		}

		public override void Draw()
		{
			for(int i = 1; i < points.Count; i++)
				Raylib.DrawLineV(points[i], points[i - 1], Color.BLACK);

			foreach(Vec2 point in points)
				Raylib.DrawCircleV(point, 3, Color.BLACK);
			
			Raylib.DrawText($"Number of Points: {points.Count}", 10, 10, 10, Color.BLACK);
			Raylib.DrawText($"Length: {totalLength}", 10, 30, 10, Color.BLACK);
		}

		public override void Unload() { }
	}
}