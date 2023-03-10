using MathLib;

using Raylib_cs;

namespace AIE08_RotatingTetrisPiece
{
	public class Block
	{
		public const float BLOCK_SIZE = 32;
		public const float HBS = BLOCK_SIZE * 0.5f;

		public Vec2 position;

		public Block(Vec2 _position)
		{
			position = _position;
		}

		public void Draw()
		{
			Rectangle rect = new(position.x - HBS + 1, position.y - HBS + 1, BLOCK_SIZE - 2, BLOCK_SIZE - 2);
			Raylib.DrawRectangleRounded(rect, 0.25f, 8, Color.BLUE);
		}
	}
}