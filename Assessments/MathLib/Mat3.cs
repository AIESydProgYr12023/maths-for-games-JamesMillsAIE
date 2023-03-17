namespace MathLib
{
	public struct Mat3
	{
	#region Static Matrix Builders

		public static Mat3 CreateTransform(Vec2 _position, float _zRotation = 0f, Vec2? _scale = null, float _xRotation = 0f, float _yRotation = 0f)
		{
			// ReSharper disable once MergeConditionalExpression
			Vec2 scale = _scale.HasValue ? _scale.Value : Vec2.one;

			Mat3 transMat = CreateTranslation(_position);
			Mat3 scaleMat = CreateScale(scale);

			Mat3 xRotMat = CreateXRotation(_xRotation);
			Mat3 yRotMat = CreateYRotation(_yRotation);
			Mat3 zRotMat = CreateZRotation(_zRotation);
			Mat3 rotMat = xRotMat * yRotMat * zRotMat;

			return transMat * rotMat * scaleMat;
		}

		public static Mat3 CreateTranslation(Vec2 _position)
		{
			return CreateTranslation(_position.x, _position.y);
		}

		public static Mat3 CreateTranslation(float _x, float _y)
		{
			return new Mat3(1, 0, _x,
			                0, 1, _y,
			                0, 0, 1);
		}

		public static Mat3 CreateScale(Vec2 _scale)
		{
			return CreateScale(_scale.x, _scale.y);
		}

		public static Mat3 CreateScale(float _x, float _y)
		{
			return new Mat3(_x, 0, 0,
			                0, _y, 0,
			                0, 0, 1);
		}

		public static Mat3 CreateXRotation(float _rot)
		{
			float cos = MathF.Cos(_rot);
			float sin = MathF.Sin(_rot);

			return new Mat3(1, 0, 0,
			                0, cos, -sin,
			                0, sin, cos);
		}

		public static Mat3 CreateYRotation(float _rot)
		{
			float cos = MathF.Cos(_rot);
			float sin = MathF.Sin(_rot);

			return new Mat3(cos, 0, sin,
			                0, 1, 0,
			                -sin, 0, cos);
		}

		public static Mat3 CreateZRotation(float _rot)
		{
			float cos = MathF.Cos(_rot);
			float sin = MathF.Sin(_rot);

			return new Mat3(cos, -sin, 0,
			                sin, cos, 0,
			                0, 0, 1);
		}

	#endregion

	#region Accessor Properties

		public Vec2 Translation
		{
			get => GetTranslation();
			set => SetTranslation(value.x, value.y);
		}

		public Vec2 Scale
		{
			get => GetScale();
			set => SetScale(value.x, value.y);
		}

		public float RotationX
		{
			get => GetRotationX();
			set => SetXRotation(value);
		}

		public float RotationY
		{
			get => GetRotationY();
			set => SetYRotation(value);
		}

		public float RotationZ
		{
			get => GetRotationZ();
			set => SetZRotation(value);
		}

	#endregion

		public float m1, m2, m3, m4, m5, m6, m7, m8, m9;

		public Mat3()
		{
			// 1 0 0
			// 0 1 0
			// 0 0 1
			m1 = 1;
			m2 = 0;
			m3 = 0;
			m4 = 0;
			m5 = 1;
			m6 = 0;
			m7 = 0;
			m8 = 0;
			m9 = 1;
		}

		public Mat3(float _m1, float _m4, float _m7, float _m2, float _m5, float _m8, float _m3, float _m6, float _m9)
		{
			m1 = _m1;
			m2 = _m2;
			m3 = _m3;
			m4 = _m4;
			m5 = _m5;
			m6 = _m6;
			m7 = _m7;
			m8 = _m8;
			m9 = _m9;
		}

		public float this[int _row, int _col]
		{
			get
			{
				if(_row == 0 && _col == 0) return m1;
				if(_row == 1 && _col == 0) return m2;
				if(_row == 2 && _col == 0) return m3;
				if(_row == 0 && _col == 1) return m4;
				if(_row == 1 && _col == 1) return m5;
				if(_row == 2 && _col == 1) return m6;
				if(_row == 0 && _col == 2) return m7;
				if(_row == 1 && _col == 2) return m8;
				if(_row == 2 && _col == 2) return m9;

				throw new IndexOutOfRangeException("Mat3 only has 3 columns and 3 rows");
			}
		}

		public Vec2 TransformPoint(Vec2 _point)
		{
			return new Vec2(_point.x * m1 + _point.y * m4 + m7,
			                _point.x * m2 + _point.y * m5 + m8);
		}

		public Vec2 TransformVector(Vec2 _vec)
		{
			return new Vec2(_vec.x * m1 + _vec.y * m4,
			                _vec.x * m2 + _vec.y * m5);
		}

	#region Operators

		public static Mat3 operator *(Mat3 _lhs, Mat3 _rhs)
		{
			return new Mat3(
			                _lhs.m1 * _rhs.m1 + _lhs.m2 * _rhs.m4 + _lhs.m3 * _rhs.m7,
			                _lhs.m4 * _rhs.m1 + _lhs.m5 * _rhs.m4 + _lhs.m6 * _rhs.m7,
			                _lhs.m7 * _rhs.m1 + _lhs.m8 * _rhs.m4 + _lhs.m9 * _rhs.m7,
			                
			                _lhs.m1 * _rhs.m2 + _lhs.m2 * _rhs.m5 + _lhs.m3 * _rhs.m8,
			                _lhs.m4 * _rhs.m2 + _lhs.m5 * _rhs.m5 + _lhs.m6 * _rhs.m8,
			                _lhs.m7 * _rhs.m2 + _lhs.m8 * _rhs.m5 + _lhs.m9 * _rhs.m8,
			                
			                _lhs.m1 * _rhs.m3 + _lhs.m2 * _rhs.m6 + _lhs.m3 * _rhs.m9,
			                _lhs.m4 * _rhs.m3 + _lhs.m5 * _rhs.m6 + _lhs.m6 * _rhs.m9,
			                _lhs.m7 * _rhs.m3 + _lhs.m8 * _rhs.m6 + _lhs.m9 * _rhs.m9
			               );
		}

		public static Vec3 operator *(Vec3 _lhs, Mat3 _rhs)
		{
			return new Vec3(
			                _lhs.x * _rhs.m1 + _lhs.y * _rhs.m4 + _lhs.z * _rhs.m7,
			                _lhs.x * _rhs.m2 + _lhs.y * _rhs.m5 + _lhs.z * _rhs.m8,
			                _lhs.x * _rhs.m3 + _lhs.y * _rhs.m6 + _lhs.z * _rhs.m9
			               );
		}

		public static Vec3 operator *(Mat3 _lhs, Vec3 _rhs)
		{
			return new Vec3(
			                _lhs.m1 * _rhs.x + _lhs.m2 * _rhs.y + _lhs.m3 * _rhs.z,
			                _lhs.m4 * _rhs.x + _lhs.m5 * _rhs.y + _lhs.m6 * _rhs.z,
			                _lhs.m7 * _rhs.x + _lhs.m8 * _rhs.y + _lhs.m9 * _rhs.z
			               );
		}

	#endregion

	#region Value Getters

		public Vec2 GetTranslation()
		{
			return new Vec2(m7, m8);
		}

		public Vec2 GetScale()
		{
			float xALength = MathF.Sqrt(m1 * m1 + m2 * m2 + m3 * m3);
			float yALength = MathF.Sqrt(m4 * m4 + m5 * m5 + m6 * m6);

			return new Vec2(xALength, yALength);
		}

		public float GetRotationX()
		{
			return MathF.Atan2(m2, m1);
		}

		public float GetRotationY()
		{
			return MathF.Atan2(-m4, m5);
		}

		public float GetRotationZ()
		{
			return MathF.Atan2(m7, m9);
		}

	#endregion

	#region Value Setters

		public void SetTranslation(float _x, float _y)
		{
			m7 = _x;
			m8 = _y;
		}

		public void Translate(float _x, float _y)
		{
			m7 += _x;
			m8 += _y;
		}

		public void SetScale(float _x, float _y)
		{
			float xALength = MathF.Sqrt(m1 * m1 + m2 * m2 + m3 * m3);
			float yALength = MathF.Sqrt(m4 * m4 + m5 * m5 + m6 * m6);

			if(xALength > 0 && _x != 0)
			{
				m1 = m1 / xALength * _x;
				m2 = m2 / xALength * _x;
				m3 = m3 / xALength * _x;
			}

			if(yALength > 0 && _y != 0)
			{
				m4 = m4 / yALength * _y;
				m5 = m5 / yALength * _y;
				m6 = m6 / yALength * _y;
			}
		}

		public void SetXRotation(float _xRot)
		{
			float yALength = MathF.Sqrt(m4 * m4 + m5 * m5 + m6 * m6);
			float zALength = MathF.Sqrt(m7 * m7 + m8 * m8 + m9 * m9);

			float cos = MathF.Cos(_xRot);
			float sin = MathF.Sin(_xRot);

			m5 = cos * yALength;
			m8 = -sin * zALength;
			m6 = sin * yALength;
			m9 = cos * zALength;
		}

		public void SetYRotation(float _yRot)
		{
			float xALength = MathF.Sqrt(m1 * m1 + m2 * m2 + m3 * m3);
			float zALength = MathF.Sqrt(m7 * m7 + m8 * m8 + m9 * m9);

			float cos = MathF.Cos(_yRot);
			float sin = MathF.Sin(_yRot);

			m1 = cos * xALength;
			m7 = sin * zALength;
			m3 = -sin * xALength;
			m9 = cos * zALength;
		}

		public void SetZRotation(float _zRot)
		{
			float xALength = MathF.Sqrt(m1 * m1 + m2 * m2 + m3 * m3);
			float yALength = MathF.Sqrt(m4 * m4 + m5 * m5 + m6 * m6);

			float cos = MathF.Cos(_zRot);
			float sin = MathF.Sin(_zRot);

			m1 = cos * xALength;
			m4 = -sin * yALength;
			m2 = sin * xALength;
			m5 = cos * yALength;
		}

	#endregion
	}
}