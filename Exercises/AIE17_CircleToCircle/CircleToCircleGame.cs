using Azimuth;

using MathLib;
using MathLib.Geometry;

using Raylib_cs;

namespace AIE17_CircleToCircle
{
	public class CircleToCircleGame : Game
	{
		private Circle a = new Circle(new Vec2(400, 225), 50);
		private Circle b = new Circle(new Vec2(500, 225), 20);

		private bool resolveCollisions;
		
		public override void Load() { }

		public override void Unload() { }

		public override void Draw()
		{
			DrawCircle(a, Color.BLACK);
			DrawClosestPoint(a, Raylib.GetMousePosition());
			
			DrawCircle(b, Color.BLACK);
			DrawCircleCircleIntersection(a, b);
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsKeyDown(KeyboardKey.KEY_A)) b.center.x -= 300 * _deltaTime;
			if(Raylib.IsKeyDown(KeyboardKey.KEY_D)) b.center.x += 300 * _deltaTime;
			if(Raylib.IsKeyDown(KeyboardKey.KEY_W)) b.center.y -= 300 * _deltaTime;
			if(Raylib.IsKeyDown(KeyboardKey.KEY_S)) b.center.y += 300 * _deltaTime;

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
				resolveCollisions = !resolveCollisions;

			if(resolveCollisions)
			{
				Hit? hit = a.Intersects(b);
				if(hit != null)
				{
					b.center += hit.Value.delta;
				}
			}
		}

		private void DrawCircle(Circle _c, Color _color)
		{
			Raylib.DrawCircleLines((int)_c.center.x, (int)_c.center.y, _c.radius, _color);
		}

		private void DrawClosestPoint(Circle _c, Vec2 _point)
		{
			Hit? hit = _c.Intersects(_point);
			if(hit != null)
			{
				Hit h = hit.Value;
				
				Raylib.DrawLineEx(h.point, h.point + h.normal * 15, 2, Color.RED);
				Raylib.DrawCircleV(h.point, 3, Color.RED);
			}
		}

		private void DrawCircleCircleIntersection(Circle _a, Circle _b)
		{
			Hit? hit = _a.Intersects(_b);
			if(hit != null)
			{
				Hit h = hit.Value;

				Circle c = new Circle(_b.center + h.delta, _b.radius);
				DrawCircle(c, Color.RED);
				Raylib.DrawLineEx(h.point, h.point + h.normal * 15, 2, Color.RED);
			}
		}
	}
}