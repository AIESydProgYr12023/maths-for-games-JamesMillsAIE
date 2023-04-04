namespace MathLib.Geometry
{
	public struct OrientedRect
	{
		public Vec2 center;
		public Vec2 extents;
		public float rotation;

		public OrientedRect()
		{
			center = Vec2.zero;
			extents = Vec2.one;
			rotation = 0;
		}

		public OrientedRect(Vec2 _center, Vec2 _extents)
		{
			center = _center;
			extents = _extents;
			rotation = 0;
		}

		public OrientedRect(Vec2 _center, Vec2 _extents, float _rotation)
		{
			center = _center;
			extents = _extents;
			rotation = _rotation;
		}

		public bool Contains(Vec2 _point)
		{
			Vec2 rotVector = _point - center;
			float theta = -(rotation * Azimath.DEG_2_RAD);
			
			Mat3 zRot = Mat3.CreateXRotation(theta);

			rotVector = zRot.TransformVector(rotVector);

			Rect local = new Rect(Vec2.zero, extents * 2f);

			Vec2 localPoint = rotVector + local.extents;

			return local.Contains(localPoint);
		}

		public Hit? Intersects(Rect _rect)
		{
			bool intersects = Interval.RectOrientedRectSAT(_rect, this, out float[] _);

			return intersects ? new Hit() : null;
		}

		public Hit? Intersects(OrientedRect _other)
		{
			bool intersects = Interval.OrientedOrientedSAT(_other, this, out float[] _);

			return intersects ? new Hit() : null;
		}
	}
}