namespace MathLib.Geometry
{
	public interface IShape
	{
		public Hit? Intersects<SHAPE>(SHAPE _other) where SHAPE : IShape;
	}
}