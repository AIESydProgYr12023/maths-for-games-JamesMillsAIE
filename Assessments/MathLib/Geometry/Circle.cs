namespace MathLib.Geometry
{
	public struct Circle
	{
		public Vec2 center;
		public float radius;

		public Circle()
		{
			center = Vec2.zero;
			radius = 1f;
		}

		public Circle(Vec2 _center, float _radius)
		{
			center = _center;
			radius = _radius;
		}

		public Hit? Intersects(Vec2 _point)
		{
			float distance = Vec2.Distance(center, _point);

			if(distance > radius)
				return null;

			Vec2 closestPoint = center + (_point - center).Normalized * radius;
			Vec2 delta = closestPoint - _point;

			Hit hit = new()
			{
				point = closestPoint,
				delta = delta,
				normal = delta.Normalized
			};

			return hit;
		}

		public Hit? Intersects(Circle _circle)
		{
			float distance = Vec2.Distance(center, _circle.center);
			if(distance >= radius + _circle.radius)
				return null;

			Vec2 vec = (_circle.center - center).Normalized;
			Vec2 closestPoint = center + vec * radius;
			float overlapLength = radius + _circle.radius - distance;
			Vec2 delta = vec * overlapLength;

			Hit hit = new()
			{
				point = closestPoint,
				delta = delta,
				normal = vec
			};

			return hit;
		}
	}
}