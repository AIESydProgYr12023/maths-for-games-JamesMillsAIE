using Raylib_cs;

using System.Diagnostics.CodeAnalysis;

namespace MathLib.Geometry
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public struct Interval
	{
		public static bool OrientedOrientedSAT(OrientedRect _a, OrientedRect _b, out float[] _overlaps)
		{
			Rect local1 = new Rect(_a.extents / 2, _a.extents);

			Vec2 r = _b.center - _a.center;

			OrientedRect local2 = new OrientedRect(_b.center, _b.extents, _b.rotation);
			local2.rotation = _b.rotation - _a.rotation;

			float theta = -(_a.rotation * Azimath.DEG_2_RAD);
			Mat3 zRot = Mat3.CreateXRotation(theta);

			local2.center = zRot.TransformVector(r + _a.extents);

			return RectOrientedRectSAT(local1, local2, out _overlaps);
		}
		
		public static bool RectOrientedRectSAT(Rect _a, OrientedRect _b, out float[] _overlaps)
		{
			_overlaps = new float[4];

			Vec2[] axes = { new(1, 0), new(0, 1), new(), new() };

			float theta = _b.rotation * Azimath.DEG_2_RAD;
			Mat3 zRot = Mat3.CreateZRotation(theta);
			
			Vec2 axisToRotate = new Vec2(_b.extents.x, 0).Normalized;
			axes[2] = zRot.TransformVector(axisToRotate);

			axisToRotate = new Vec2(0, _b.extents.y).Normalized;
			axes[3] = zRot.TransformVector(axisToRotate);
			
			for(int axis = 0; axis < axes.Length; axis++)
			{
				Vec2 start = axes[axis] * 25;
				Vec2 end = axes[axis] * -25;
				
				Raylib.DrawLineV(start + new Vec2(100, 100), end + new Vec2(100, 100), Color.BLUE);
				
				if(!OverlapOnAxis(_a, _b, axes[axis], out _overlaps[axis]))
					return false;
			}

			return true;
		}
		
		public static bool RectRectSAT(Rect _a, Rect _b, out float[] _overlaps)
		{
			_overlaps = new float[2];

			Vec2[] axes = { new(1, 0), new(0, 1) };
			for(int axis = 0; axis < axes.Length; axis++)
			{
				if(!OverlapOnAxis(_a, _b, axes[axis], out _overlaps[axis]))
					return false;
			}

			return true;
		}

		public static bool OverlapOnAxis(Rect _a, OrientedRect _b, Vec2 _axis, out float _overlap)
		{
			Interval a = GetInterval(_a, _axis);
			Interval b = GetInterval(_b, _axis);

			bool didOverlap = b.min <= a.max && a.min <= b.max;

			if(didOverlap)
				_overlap = MathF.Abs((a.max - a.min) - (b.max - b.min));
			else
				_overlap = -1;
			
			return didOverlap;
		}
		
		public static bool OverlapOnAxis(Rect _a, Rect _b, Vec2 _axis, out float _overlap)
		{
			Interval a = GetInterval(_a, _axis);
			Interval b = GetInterval(_b, _axis);

			bool didOverlap = b.min <= a.max && a.min <= b.max;

			if(didOverlap)
				_overlap = MathF.Abs((a.max - a.min) - (b.max - b.min));
			else
				_overlap = -1;
			
			return didOverlap;
		}

		public static Interval GetInterval(OrientedRect _rect, Vec2 _axis)
		{
			Rect r = new(_rect.center - _rect.extents, _rect.extents);

			Vec2 min = r.Min;
			Vec2 max = r.Max;

			Vec2[] verts =
			{
				min, max,
				new(min.x, max.y), new(max.x, min.y)
			};

			float theta = _rect.rotation * Azimath.DEG_2_RAD;
			Mat3 zRot = Mat3.CreateZRotation(theta);

			for(int vert = 0; vert < verts.Length; vert++)
			{
				Vec2 rotVec = verts[vert] - _rect.center;
				rotVec = zRot.TransformVector(rotVec);
				verts[vert] = rotVec + _rect.center;
				Raylib.DrawCircleV(verts[vert], 10, Color.BLUE);
			}

			Interval result = new();
			result.min = result.max = Vec2.Dot(_axis, verts[0]);
			for(int vert = 1; vert < verts.Length; vert++)
			{
				float projection = Vec2.Dot(_axis, verts[vert]);
				if(projection < result.min)
					result.min = projection;
				if(projection > result.max)
					result.max = projection;
			}

			return result;
		}
		
		public static Interval GetInterval(Rect _rect, Vec2 _axis)
		{
			Interval result = new();

			Vec2 min = _rect.Min;
			Vec2 max = _rect.Max;

			Vec2[] verts =
			{
				new(min.x, min.y), new(min.x, max.y),
				new(max.x, max.y), new(max.x, min.y)
			};

			result.min = result.max = Vec2.Dot(_axis, verts[0]);

			for(int vert = 1; vert < verts.Length; vert++)
			{
				float projection = Vec2.Dot(_axis, verts[vert]);
				
				if(projection < result.min)
					result.min = projection;
				if(projection > result.max)
					result.max = projection;
			}

			return result;
		}
		
		public float min;
		public float max;
	}
}