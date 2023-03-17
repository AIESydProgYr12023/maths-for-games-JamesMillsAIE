using Azimuth;

using MathLib;
using MathLib.Geometry;

using Raylib_cs;

namespace AIE16_BoxToBox
{
	public class BoxToBoxGame : Game
	{
		private Rect aabb1 = new(400, 250, 50, 50);
		private Rect aabb2 = new(500, 250, 25, 25);

		private bool resolveCollision;

		public override void Draw()
		{
			DrawAabb(aabb1, Color.BLACK);
			DrawAabbClosestPoint(aabb1, Raylib.GetMousePosition());
			
			DrawAabb(aabb2, Color.BLACK);
			DrawAabbAabbIntersection(aabb1, aabb2);
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
				resolveCollision = !resolveCollision;

			if(Raylib.IsKeyDown(KeyboardKey.KEY_A)) aabb2.center.x -= 300 * _deltaTime;
			if(Raylib.IsKeyDown(KeyboardKey.KEY_D)) aabb2.center.x += 300 * _deltaTime;
			if(Raylib.IsKeyDown(KeyboardKey.KEY_W)) aabb2.center.y -= 300 * _deltaTime;
			if(Raylib.IsKeyDown(KeyboardKey.KEY_S)) aabb2.center.y += 300 * _deltaTime;

			if(resolveCollision)
			{
				Hit? hit = aabb1.Intersects(aabb2);
				if(hit != null)
				{
					aabb2.center += hit.Value.delta;
				}	
			}
		}

		public override void Load() { }

		public override void Unload() { }

		private void DrawAabb(Rect _box, Color _color)
		{
			if(_box.Contains(Raylib.GetMousePosition()))
				_color = Color.RED;
			
			Raylib.DrawRectangleLinesEx(_box, 1, _color);
		}

		private void DrawAabbClosestPoint(Rect _box, Vec2 _point)
		{
			Hit? hit = _box.Intersects(_point);
			if(hit != null)
			{
				Raylib.DrawLineEx(hit.Value.point, hit.Value.point + hit.Value.normal * 15, 2, Color.RED);
				Raylib.DrawCircleV(hit.Value.point, 3, Color.RED);
			}
		}

		private void DrawAabbAabbIntersection(Rect _boxA, Rect _boxB)
		{
			Hit? hit = _boxA.Intersects(_boxB);
			if(hit != null)
			{
				Hit h = hit.Value;

				Rect boxC = new(_boxB.center, _boxB.extents);
				boxC.center += h.delta;
				DrawAabb(boxC, Color.RED);
				
				Raylib.DrawLineEx(h.point, h.point + h.normal * 15, 2f, Color.RED);
				Raylib.DrawCircleV(h.point, 3, Color.RED);
			}
		}
	}
}