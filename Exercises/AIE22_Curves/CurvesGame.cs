using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE22_Curves
{
	public class CurvesGame : Game
	{
		private int xDivisions;
		private int yDivisions;

		private float curveDensity;

		private Rectangle rect;

		private float t;

		private Curve.Type current = Curve.Type.LinearInterpolation;

		public override void Load()
		{
			xDivisions = Config.Get<int>("Grid", "xDiv");
			yDivisions = Config.Get<int>("Grid", "yDiv");

			curveDensity = Config.Get<float>("Curves", "density");

			int gridSize = Config.Get<int>("Grid", "size");
			rect = new Rectangle(Application.Instance!.Window.Width / 2, Config.Get<int>("Grid", "yPos"), gridSize, gridSize);
			
			Curve.GenerateCurves();
		}

		public override void Draw()
		{
			DrawName();
			DrawGrid();
			DrawCurve();
			DrawPoint();
		}

		public override void Update(float _deltaTime)
		{
			t += _deltaTime;
			if(t > 1)
				t = 0;

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
			{
				if(current != 0)
					current--;
			}

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
			{
				current++;
				if(current == Curve.Type.CurveTypeCount)
					current = 0;
			}
		}

		public override void Unload() { }

	#region Drawing Functions

		private void DrawName()
		{
			Raylib.DrawText(Curve.NameOf(current), 10, 10, 20, Color.BLACK);
		}
		
		private void DrawGrid()
		{
			for(int i = 0; i < xDivisions; i++)
			{
				float xPos = rect.x + i * xDivisions;
				
				Raylib.DrawLine((int)xPos, (int)rect.y, (int)xPos, (int)(rect.y + rect.height), Color.GRAY);
			}
			
			for(int i = 0; i < yDivisions; i++)
			{
				float yPos = rect.y + i * yDivisions;
				
				Raylib.DrawLine((int)rect.x, (int)yPos, (int)(rect.x + rect.width), (int)yPos, Color.GRAY);
			}
		}

		private void DrawCurve()
		{
			int segments = (int) curveDensity;

			for(int i = 0; i < segments; i++)
			{
				float x1 = i / curveDensity;
				float x2 = (i + 1) / curveDensity;

				float y1 = Curve.Evaluate(x1, 0, 1, current);
				float y2 = Curve.Evaluate(x2, 0, 1, current);
				
				Raylib.DrawLineEx(new Vec2(rect.x + x1 * rect.width, rect.y + rect.height - y1 * rect.height),
				                  new Vec2(rect.x + x2 * rect.width, rect.y + rect.height - y2 * rect.height),
				                  2, Color.BLACK);
			}
		}

		private void DrawPoint()
		{
			Raylib.DrawCircleV(new Vec2(rect.x + t * rect.width, 
			                            rect.y + rect.height - Curve.Evaluate(t, 0, 1, current) * rect.height),
			                   8, Color.RED);
		}

	#endregion
	}
}