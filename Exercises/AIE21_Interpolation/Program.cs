using MathLib;

namespace AIE21_Interpolation
{
	public static class Program
	{
		private static float Clamp01(float _val)
		{
			return Clamp(_val, 0, 1);
		}
		
		private static float Clamp(float _val, float _min, float _max)
		{
			if(_val < _min)
				_val = _min;

			if(_val > _max)
				_val = _max;

			return _val;
		}

		private static float Lerp(float _a, float _b, float _t)
		{
			_t = Clamp01(_t);

			return _a * (1 - _t) + _b * _t;
		}

		private static Vec2 Lerp(Vec2 _a, Vec2 _b, float _t)
		{
			_t = Clamp01(_t);
			
			return _a * (1 - _t) + _b * _t;
		}
		
		public static void Main()
		{
			string? input = Console.ReadLine();
			
			Print(10, 100, float.Parse(input));
			
			//50,1,1,1 -> a = 50,1, b = 1,1
			input = Console.ReadLine();
			string[] split = input.Split(',');

			float[] vals = new float[split.Length];
			for(int i = 0; i < split.Length; i++)
			{
				vals[i] = float.Parse(split[i]);
			}

			input = Console.ReadLine();
			Print(new Vec2(vals[0], vals[1]), new Vec2(vals[2], vals[3]), float.Parse(input));
		}

		private static void Print(float _a, float _b, float _t)
		{
			Console.WriteLine($"{(int)(_t * 100)}% interpolation between {_a} and {_b} is: {Lerp(_a, _b, _t)}");
		}

		private static void Print(Vec2 _a, Vec2 _b, float _t)
		{
			Console.WriteLine($"{(int)(_t * 100)}% interpolation between {_a} and {_b} is: {Lerp(_a, _b, _t)}");
		}
	}
}