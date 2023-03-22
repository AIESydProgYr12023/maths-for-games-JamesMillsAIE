namespace MathLib
{
	/// <summary>A collection of useful constants and mathematical functions.</summary>
	public static class Azimath
	{
		/// <summary>Degrees-to-radians conversion constant. Multiply a degree angle by this to get the radians equivalent.</summary>
		public const float DEG_2_RAD = MathF.PI * 2f / 360f;

		/// <summary>Radians-to-degrees conversion constant. Multiply a radian angle by this to get the degrees equivalent.</summary>
		public const float RAD_2_DEG = 1f / DEG_2_RAD;

		/// <summary>The standard mathematical constant, PI (3.14).</summary>
		public const float PI = MathF.PI;

		/// <summary>A number as close to 0 as possible without actually being 0.</summary>
		public const float EPSILON = float.Epsilon;

		/// <summary>The minimum possible value a float can be.</summary>
		public const float NEGATIVE_INFINITY = float.NegativeInfinity;
		
		/// <summary>The maximum possible value a float can be.</summary>
		public const float POSITIVE_INFINITY = float.PositiveInfinity;

		/// <summary>Returns the smaller of the two passed values.</summary>
		public static float Min(float _a, float _b) => _a < _b ? _a : _b;

		/// <summary>Returns the larger of the two passed values.</summary>
		public static float Max(float _a, float _b) => _a > _b ? _a : _b;

		/// <summary>Calculates the Sine value for the passed Theta Angle.</summary>
		public static float Sin(float _theta) => MathF.Sin(_theta);
		
		/// <summary>Calculates the Cosine value for the passed Theta Angle.</summary>
		public static float Cos(float _theta) => MathF.Cos(_theta);
		
		/// <summary>Calculates the Arc Tangent value for the passed x and y.</summary>
		public static float Atan2(float _x, float _y) => MathF.Atan2(_y, _x);

		/// <summary>Calculates the Logarithmic value for the passed value.</summary>
		public static float Log(float _val) => MathF.Log(_val);

		/// <summary>Raises the passed value to the passed power.</summary>
		/// <param name="_val">The value being raised.</param>
		/// <param name="_power">The power the value is being raised to.</param>
		public static float Pow(float _val, float _power) => MathF.Pow(_val, _power);

		/// <summary>Calculates the Exponential value for the passed value.</summary>
		public static float Exp(float _val) => MathF.Exp(_val);

		/// <summary>Gets the sign of the passed value.</summary>
		/// <returns>-1 if the value is a negative number, +1 if the number is a positive number.</returns>
		public static float Sign(float _val) => _val < 0 ? -1 : 1;

		/// <summary>Gets the Absolute value for the passed value.</summary>
		public static float Abs(float _val) => MathF.Abs(_val);

		/// <summary>Rounds a value to the nearest whole number, if it is at exactly 0.5, it will round up.</summary>
		/// <param name="_value">The value we want to accurately round.</param>
		public static float Round(float _value) => MathF.Round(_value, MidpointRounding.AwayFromZero);

		/// <summary>Ensures a value stays within the specified range.</summary>
		/// <example>5 within 0 - 10 returns 5. 19 within 0 - 10 returns 10.</example>
		/// <param name="_value">The number we are clamping.</param>
		/// <param name="_min">The minimum possible value for <paramref name="_value"/>.</param>
		/// <param name="_max">The maximum possible value for <paramref name="_value"/>.</param>
		public static float Clamp(float _value, float _min, float _max)
		{
			if(_value < _min)
				_value = _min;

			if(_value > _max)
				_value = _max;

			return _value;
		}

		/// <summary>Ensures the passed value remains within the range 0 - 1.</summary>
		/// <param name="_value">The number we are clamping.</param>
		public static float Clamp01(float _value)
		{
			return Clamp(_value, 0f, 1f);
		}

		public static float Lerp(float _a, float _b, float _t)
		{
			_t = Clamp01(_t);

			return _a * (1 - _t) + _b * _t;
		}

		public static int Lerp(int _a, int _b, float _t)
		{
			_t = Clamp01(_t);

			return (int) (_a * (1 - _t) + _b * _t);
		}

		/// <summary>Checks if two floats are almost or exactly the same value.</summary>
		public static bool Approximately(float _a, float _b) => Abs(_b - _a) < Max(0.000001f * Max(Abs(_a), Abs(_b)), EPSILON * 8f);

		/// <summary>Takes a value from one range and changes it to fit another.</summary>
		/// <example>0.5 starts in 0 - 1 | Remaps to 0 in the range -1 - 1.</example>
		/// <param name="_value">The number we are remapping.</param>
		/// <param name="_oldMin">The minimum value of the old range.</param>
		/// <param name="_oldMax">The maximum value of the old range.</param>
		/// <param name="_newMin">The minimum value of the new range.</param>
		/// <param name="_newMax">The maximum value of the new range.</param>
		public static float Remap(float _value, float _oldMin, float _oldMax, float _newMin, float _newMax)
		{
			return (_value - _oldMin) / (_oldMax - _oldMin) * (_newMax - _newMin) + _newMin;
		}
	}
}