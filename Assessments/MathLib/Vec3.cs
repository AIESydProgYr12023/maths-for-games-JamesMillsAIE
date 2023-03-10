using System.Numerics;

namespace MathLib
{
	public struct Vec3
	{
		public float x = 0;
		public float y = 0;
		public float z = 0;

		public Vec3() { }

		public Vec3(float _x, float _y, float _z)
		{
			x = _x;
			y = _y;
			z = _z;
		}

		/// <summary>
		/// This method allows an implicit conversion from System.Numerics.Vector2 to our Vec3 class.
		/// </summary>
		/// <param name="_vec"></param>
		public static implicit operator Vec3(Vector3 _vec)
		{
			return new Vec3(_vec.X, _vec.Y, _vec.Z);
		}

		/// <summary>
		/// This method allows an implicit conversion from our Vec3 class to System.Numerics.Vector2.
		/// </summary>
		/// <param name="_v"></param>
		public static implicit operator Vector3(Vec3 _v)
		{
			return new Vector3(_v.x, _v.y, _v.z);
		}

		//Addition
		public static Vec3 Addition(Vec3 _lhs, Vec3 _rhs)
		{
			return new Vec3(_lhs.x + _rhs.x, _lhs.y + _rhs.y, _lhs.z + _rhs.z);
		}

		//Addition Operator Overload +
		public static Vec3 operator +(Vec3 _lhs, Vec3 _rhs)
		{
			return new Vec3(_lhs.x + _rhs.x, _lhs.y + _rhs.y, _lhs.z + _rhs.z);
		}

		//Subtract
		public static Vec3 Subtraction(Vec3 _lhs, Vec3 _rhs)
		{
			return new Vec3(_lhs.x - _rhs.x, _lhs.y - _rhs.y, _lhs.z - _rhs.z);
		}

		//Subtraction Operator Overload - 
		public static Vec3 operator -(Vec3 _lhs, Vec3 _rhs)
		{
			return new Vec3(_lhs.x - _rhs.x, _lhs.y - _rhs.y, _lhs.z - _rhs.z);
		}

		//PreScale Function
		public static Vec3 PreScale(float _lhs, Vec3 _rhs)
		{
			return new Vec3(_lhs * _rhs.x, _lhs * _rhs.y, _lhs * _rhs.z);
		}

		//PreScale Operator Overload 
		public static Vec3 operator *(float _lhs, Vec3 _rhs)
		{
			return new Vec3(_lhs * _rhs.x, _lhs * _rhs.y, _lhs * _rhs.z);
		}

		//PostScale Function
		public static Vec3 PostScale(Vec3 _lhs, float _rhs)
		{
			return new Vec3(_lhs.x * _rhs, _lhs.y * _rhs, _lhs.z * _rhs);
		}

		//PostScale Operator Overload
		public static Vec3 operator *(Vec3 _lhs, float _rhs)
		{
			return new Vec3(_lhs.x * _rhs, _lhs.y * _rhs, _lhs.z * _rhs);
		}

		public Vec3 Cross(Vec3 _rhs)
		{
			return new Vec3(y * _rhs.z - z * _rhs.y,
				z * _rhs.x - x * _rhs.z,
				x * _rhs.y - y * _rhs.x);
		}

		public float Magnitude()
		{
			return MathF.Sqrt(x * x + y * y + z * z);
		}

		//Normalisation - Non Static
		public void Normalise()
		{
			float length = Magnitude();
			if(length == 0)
			{
				x = 0;
				y = 0;
				z = 0;
			}
			else
			{
				x /= length;
				y /= length;
				z /= length;
			}
		}

		//Normalisation - Static
		public static Vec3 Normalise(Vec3 _vec)
		{
			float len = _vec.Magnitude();
			return len == 0 ? new Vec3(0, 0, 0) : new Vec3(_vec.x / len, _vec.y / len, _vec.z / len);
		}

		//Dot
		public float Dot(Vec3 _rhs)
		{
			return (x * _rhs.x) + (y * _rhs.y) + (z * _rhs.z);
		}

		public static float Dot(Vec3 _lhs, Vec3 _rhs)
		{
			return (_lhs.x * _rhs.x) + (_lhs.y * _rhs.y) + (_lhs.z * _rhs.z);
		}
	}
}