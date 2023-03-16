using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE09_Matrix3DebugRendering
{
	public class Matrix3DebugRenderingGame : Game
	{
		private Mat3 matrixA = new();
		private Matrix3Renderer matrixDebugRenderer;
		
		public override void Load()
		{
			Vec2 screenSize = new Vec2(Application.Instance!.Window.Width, Application.Instance.Window.Height);
			Vec2 unitSize = new Vec2(24, 24);

			matrixDebugRenderer = new Matrix3Renderer(screenSize, unitSize);
		}

		public override void Draw()
		{
			matrixDebugRenderer.BeginRender();
			matrixDebugRenderer.DrawMatrix("MatA", matrixA, Color.RED, new Vec2(10, 10));
			matrixDebugRenderer.EndRender();

			matrixA = matrixDebugRenderer.GetMatrix("MatA");
		}

		public override void Unload()
		{
			
		}
	}
}