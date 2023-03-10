using MathLib;

using Raylib_cs;

namespace AIE08_RotatingTetrisPiece
{
	public class Shape
	{
		public static Shape CreateTShape(Vec2 _position)
		{
			Shape shape = new(_position);

			shape.blocks = new Block[4];
			shape.blocks[0] = new Block(_position);
			shape.blocks[1] = new Block(_position + new Vec2(-1 * Block.BLOCK_SIZE, 0));
			shape.blocks[2] = new Block(_position + new Vec2(1 * Block.BLOCK_SIZE, 0));
			shape.blocks[3] = new Block(_position + new Vec2(0, 1 * Block.BLOCK_SIZE));

			return shape;
		}
		
		public Vec2 position;
		public Block[] blocks;

		public Shape(Vec2 _position)
		{
			position = _position;
			blocks = new Block[1];
		}

		public void Draw()
		{
			foreach(Block block in blocks)
				block.Draw();

			Raylib.DrawCircleV(position, 2, Color.BLACK);
		}

		public void RotateShape(float _amount)
		{
			foreach(Block block in blocks)
				block.position.RotateAround(position, _amount);
		}
	}
}