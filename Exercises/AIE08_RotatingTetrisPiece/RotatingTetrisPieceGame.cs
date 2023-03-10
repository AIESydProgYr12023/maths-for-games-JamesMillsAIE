using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE08_RotatingTetrisPiece
{
	public class RotatingTetrisPieceGame : Game
	{
		private Shape shape;

		public override void Load()
		{
			shape = Shape.CreateTShape(new Vec2(200, 200));
		}

		public override void Unload() { }

		public override void Draw()
		{
			shape.Draw();
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
				shape.RotateShape(MathF.PI * _deltaTime);
			
			if(Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
				shape.RotateShape(MathF.PI / 2);
		}
	}
}