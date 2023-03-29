using MathLib;

namespace AIE22_Curves
{
	public delegate float CurveFunction(float _t, float _start, float _end);
	
	public class Curve
	{
		public enum Type
		{
			LinearInterpolation,
			
			QuadraticEaseOut,
			QuadraticEaseIn,
			QuadraticEaseInOut,
			
			CubicEaseOut,
			CubicEaseIn,
			CubicEaseInOut,
			
			CurveTypeCount
		}

		private static readonly Dictionary<Type, Curve> curves = new();

		public static void GenerateCurves()
		{
			curves.Add(Type.LinearInterpolation,
			           new Curve(Type.LinearInterpolation, (_t, _start, _end) => Azimath.Lerp(_start, _end, _t)));
			
			// ------------------------- //
			//     Quadratic Curves      //
			curves.Add(Type.QuadraticEaseOut, 
			           new Curve(Type.QuadraticEaseOut, (_t, _start, _end) => -_end * _t * (_t - 2) + _start));
			curves.Add(Type.QuadraticEaseIn, 
			           new Curve(Type.QuadraticEaseIn, (_t, _start, _end) => _end * _t * _t + _start));
			curves.Add(Type.QuadraticEaseInOut,
			           new Curve(Type.QuadraticEaseInOut, (_t, _start, _end) =>
			           {
				           _t *= 2.0f;

				           if(_t < 1.0f)
					           return _end / 2.0f * _t * _t + _start;

				           _t--;

				           return -_end / 2.0f * (_t * (_t - 2) - 1) + _start;
			           }));
			
			// ------------------------- //
			//       Cubic Curves        //
			curves.Add(Type.CubicEaseOut, 
			           new Curve(Type.CubicEaseOut, (_t, _start, _end) =>
			           {
				           _t -= 1;

				           return _end * (_t * _t * _t + 1.0f) + _start;
			           }));
			curves.Add(Type.CubicEaseIn, 
			           new Curve(Type.CubicEaseIn, (_t, _start, _end) => _end * _t * _t * _t + _start));
			curves.Add(Type.CubicEaseInOut,
			           new Curve(Type.CubicEaseInOut, (_t, _start, _end) =>
			           {
				           _t *= 2.0f;

				           if(_t < 1.0f)
					           return _end / 2 * _t * _t * _t + _start;

				           _t -= 2f;

				           return _end / 2 * (_t * _t * _t + 2) + _start;
			           }));
		}

		public static float Evaluate(float _t, float _start, float _end, Type _type)
		{
			if(curves.TryGetValue(_type, out Curve curve))
			{
				return curve.Evaluate(_t, _start, _end);
			}

			return 0;
		}

		public static string NameOf(Type _type)
		{
			if(curves.TryGetValue(_type, out Curve curve))
			{
				return curve.ToString();
			}

			return "INVALID";
		}

		private readonly CurveFunction function;
		private readonly string name;

		public Curve(Type _type, CurveFunction _function)
		{
			name = _type.ToString();
			function = _function;
		}

		private float Evaluate(float _t, float _start, float _end)
		{
			if(_t <= 0f)
				return _start;

			if(_t >= _end)
				return _end;

			return function(_t, _start, _end);
		}

		public override string ToString() => name;
	}
}