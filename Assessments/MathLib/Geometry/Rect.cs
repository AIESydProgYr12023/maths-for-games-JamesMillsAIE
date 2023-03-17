using Raylib_cs;

namespace MathLib.Geometry
{
	public struct Rect
	{
		// The top left hand corner of the shape
		public Vec2 Min => center - extents;

		// The bottom right hand corner of the shape
		public Vec2 Max => center + extents;

		// The total size of the shape from edge to edge
		public Vec2 Size => extents * 2;
		
		// The position
		public Vec2 center;

		// The distance from the center to any edge (absolute)
		public Vec2 extents;

		public Rect()
		{
			center = Vec2.zero;
			extents = Vec2.half;
		}

		public Rect(Vec2 _center, Vec2 _extents)
		{
			center = _center;
			extents = _extents;
		}

		public Rect(float _x, float _y, float _hWidth, float _hHeight)
		{
			center = new Vec2(_x, _y);
			extents = new Vec2(_hWidth, _hHeight);
		}

		public bool Contains(Vec2 _point)
		{
			Vec2 min = Min;
			Vec2 max = Max;

			return _point.x > min.x && _point.x < max.x &&
			       _point.y > min.y && _point.y < max.y;
		}

		public Hit? Intersects(Vec2 _point)
		{
			if(!Contains(_point))
				return null;

			Hit hit = new Hit();

			Vec2 vec = _point - center;
			Vec2 overlap = extents - new Vec2(MathF.Abs(vec.x), MathF.Abs(vec.y));

			if(overlap.x < overlap.y)
			{
				float xDir = vec.x < 0 ? -1 : 1;

				hit.delta.x = overlap.x * xDir;
				hit.normal.x = xDir;
				hit.point.x = center.x + extents.x * xDir;
				hit.point.y = _point.y;
			}
			else
			{
				float yDir = vec.y < 0 ? -1 : 1;

				hit.delta.y = overlap.y * yDir;
				hit.normal.y = yDir;
				hit.point.y = center.y + extents.y * yDir;
				hit.point.x = _point.x;
			}

			return hit;
		}

		public Hit? Intersects(Rect _other)
		{
			Vec2 vec = _other.center - center;
			Vec2 overlap = (_other.extents + extents) - new Vec2(MathF.Abs(vec.x), MathF.Abs(vec.y));

			if(overlap.x <= 0 || overlap.y <= 0)
				return null;

			Hit hit = new Hit();
			
			if(overlap.x < overlap.y)
			{
				float xDir = vec.x < 0 ? -1 : 1;
				hit.delta.x = overlap.x * xDir;
				hit.normal.x = xDir;
				hit.point.x = center.x + extents.x * xDir;
				hit.point.y = _other.center.y;
			}
			else
			{
				float yDir = vec.y < 0 ? -1 : 1;
				hit.delta.y = overlap.y * yDir;
				hit.normal.y = yDir;
				hit.point.y = center.y + extents.y * yDir;
				hit.point.x = _other.center.x;
			}

			return hit;
		}

		public static implicit operator Rectangle(Rect _rect) =>
			new(_rect.center.x - _rect.extents.x, _rect.center.y - _rect.extents.y, _rect.Size.x, _rect.Size.y);
	}
}