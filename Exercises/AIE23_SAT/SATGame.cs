using Azimuth;

using MathLib;
using MathLib.Geometry;

using Raylib_cs;

namespace AIE23_SAT
{
	public class SATGame : Game
	{
		private OrientedRect a;
		private OrientedRect b;

		private Color aColor;
		private Color bColor;

		public override void Load()
		{
			a = new OrientedRect(new Vec2(400, 300), new Vec2(50, 50), Raylib.GetRandomValue(0, 360));
			aColor = Color.GRAY;

			b = new OrientedRect(new Vec2(600, 300), new Vec2(25, 25), 0);
			bColor = Color.RED;
		}

		public override void Draw()
		{
			Raylib.DrawRectanglePro(new Rectangle(a.center.x, a.center.y, a.extents.x * 2, a.extents.y * 2),
			                        a.extents, a.rotation, aColor);

			Hit? hit = a.Intersects(b);
			bColor = hit != null ? Color.BLUE : Color.RED;
			
			Raylib.DrawRectanglePro(new Rectangle(b.center.x, b.center.y, b.extents.x * 2, b.extents.y * 2),
			                        b.extents, b.rotation, bColor);
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsKeyDown(KeyboardKey.KEY_W))
				b.center.y -= 100 * _deltaTime;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_S))
				b.center.y += 100 * _deltaTime;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_A))
				b.center.x -= 100 * _deltaTime;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_D))
				b.center.x += 100 * _deltaTime;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_Q))
				b.rotation -= 100 * _deltaTime;
			
			if(Raylib.IsKeyDown(KeyboardKey.KEY_E))
				b.rotation += 100 * _deltaTime;
		}

		public override void Unload()
		{
			
		}
	}
}