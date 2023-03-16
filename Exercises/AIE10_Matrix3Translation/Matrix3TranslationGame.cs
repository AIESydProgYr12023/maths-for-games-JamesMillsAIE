using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE10_Matrix3Translation
{
	public class Matrix3TranslationGame : Game
	{
		private Mat3 mat = new();
		private Matrix3Renderer renderer;

		public override void Load()
		{
			Vec2 screenSize = new Vec2(Application.Instance!.Window.Width, Application.Instance.Window.Height);
			Vec2 unitSize = new Vec2(24, 24);

			renderer = new Matrix3Renderer(screenSize, unitSize);
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsKeyPressed(KeyboardKey.KEY_W))
			{
				mat.Translation += new Vec2(0, 1);
			}
			
			if(Raylib.IsKeyPressed(KeyboardKey.KEY_S))
			{
				mat.Translation -= new Vec2(0, 1);
			}
			
			if(Raylib.IsKeyPressed(KeyboardKey.KEY_A))
			{
				mat.Translation -= new Vec2(1, 0);
			}
			
			if(Raylib.IsKeyPressed(KeyboardKey.KEY_D))
			{
				mat.Translation += new Vec2(1, 0);
			}
		}

		public override void Draw()
		{
			renderer.BeginRender();
			renderer.DrawMatrix("Matrix", mat, Color.RED, new Vec2(10, 10));
			renderer.EndRender();
		}

		public override void Unload() { }
	}
}