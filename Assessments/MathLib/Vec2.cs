using System.Numerics;

namespace MathLib
{
	public struct Vec2
	{
	#region Global Constant Vectors

		public static Vec2 zero = new();
		public static Vec2 one = new(1, 1);
		public static Vec2 half = new(0.5f, 0.5f);
		public static Vec2 up = new(0, 1);
		public static Vec2 down = new(0, -1);
		public static Vec2 right = new(1, 0);
		public static Vec2 left = new(-1, 0);

	#endregion

		public Vec2 Normalized => this / Magnitude();

		public float x;
		public float y;

		public Vec2(float _x, float _y)
		{
			x = _x;
			y = _y;
		}

		public void Normalize()
		{
			this /= Magnitude();
		}

		public float Magnitude()
		{
			return MathF.Sqrt(SqrMagnitude());
		}

		public float SqrMagnitude()
		{
			return x * x + y * y;
		}

	#region Operators

		public static float Distance(Vec2 _lhs, Vec2 _rhs)
		{
			return (_lhs - _rhs).Magnitude();
		}

		public static Vec2 operator +(Vec2 _lhs, Vec2 _rhs)
		{
			return new Vec2(_lhs.x + _rhs.x, _lhs.y + _rhs.y);
		}

		public static Vec2 operator -(Vec2 _lhs, Vec2 _rhs)
		{
			return new Vec2(_lhs.x - _rhs.x, _lhs.y - _rhs.y);
		}

		public static Vec2 operator *(Vec2 _lhs, float _rhs)
		{
			return new Vec2(_lhs.x * _rhs, _lhs.y * _rhs);
		}

		public static Vec2 operator *(float _lhs, Vec2 _rhs)
		{
			return new Vec2(_lhs * _rhs.x, _lhs * _rhs.y);
		}

		public static Vec2 operator /(Vec2 _lhs, float _rhs)
		{
			return new Vec2(_lhs.x / _rhs, _lhs.y / _rhs);
		}

		public static bool operator ==(Vec2 _lhs, Vec2 _rhs)
		{
			return Azimath.Approximately(_lhs.x, _rhs.x) && Azimath.Approximately(_lhs.y, _rhs.y);
		}

		public static bool operator !=(Vec2 _lhs, Vec2 _rhs)
		{
			return !(_lhs == _rhs);
		}

		public static implicit operator Vec2(Vector2 _vector)
		{
			return new Vec2(_vector.X, _vector.Y);
		}

		public static implicit operator Vector2(Vec2 _vec)
		{
			return new Vector2(_vec.x, _vec.y);
		}

	#endregion
	}
}