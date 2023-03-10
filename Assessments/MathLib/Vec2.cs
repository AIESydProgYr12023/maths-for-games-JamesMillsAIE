using System.Numerics;

namespace MathLib
{
	public struct Vec2
	{
	#region Global Constant Vectors

		public static Vec2 zero = new();
		public static Vec2 one = new(1, 1);
		public static Vec2 half = new(0.5f, 0.5f);
		public static Vec2 up = new(0, -1);
		public static Vec2 down = new(0, 1);
		public static Vec2 right = new(1, 0);
		public static Vec2 left = new(-1, 0);

	#endregion

		public Vec2 Normalized => this / Magnitude();
		public float Rotation => Azimath.Atan2(y, x);

		public float x;
		public float y;

		public Vec2(float _x, float _y)
		{
			x = _x;
			y = _y;
		}

		public void Normalize()
		{
			float mag = Magnitude();
			if(mag == 0)
			{
				x = 0;
				y = 0;
			}
			else
			{
				x /= mag;
				y /= mag;
			}
		}

		public float Magnitude()
		{
			return MathF.Sqrt(SqrMagnitude());
		}

		public float SqrMagnitude()
		{
			return x * x + y * y;
		}

		public void Rotate(float _amount)
		{
			float xRotated = x * MathF.Cos(_amount) - y * MathF.Sin(_amount);
			float yRotated = x * MathF.Sin(_amount) + y * MathF.Cos(_amount);

			x = xRotated;
			y = yRotated;
		}

		public void RotateAround(Vec2 _pivotPoint, float _amount)
		{
			x -= _pivotPoint.x;
			y -= _pivotPoint.y;
			
			Rotate(_amount);

			x += _pivotPoint.x;
			y += _pivotPoint.y;
		}

		public override string ToString() => $"({x}, {y})";

		public override int GetHashCode() => HashCode.Combine(x.GetHashCode(), y.GetHashCode());

		public override bool Equals(object? _obj)
		{
			if(_obj == null)
				return false;

			return (Vec2) _obj == this;
		}

	#region Operators

		public static float Distance(Vec2 _lhs, Vec2 _rhs)
		{
			return (_lhs - _rhs).Magnitude();
		}

		public static float Dot(Vec2 _lhs, Vec2 _rhs)
		{
			return _lhs.x * _rhs.x + _lhs.y * _rhs.y;
		}

		public static Vec2 CreateRotationVector(float _radians)
		{
			return new Vec2(MathF.Cos(_radians), MathF.Sin(_radians));
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