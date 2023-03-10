using MathLib;

using Raylib_cs;

namespace AIE07_OverlappingCircles
{
	public class Circle
	{
		public Vec2 position = Vec2.zero;
		public Vec2 direction = Vec2.zero;
		public float radius = 50f;
		public Color color = new(0, 0, 0, 128);
	}
}