using Azimuth.GameObjects;

using MathLib;

using Raylib_cs;

namespace AIE14_SceneGraphPlanets
{
	public class PlanetGameObject : GameObject
	{
		public float Radius { get; set; }
		public Color Color { get; set; }

		public PlanetGameObject(Vec2 _position, float _radius = 20, Color? _color = null) : base(_position)
		{
			Radius = _radius;
			// ReSharper disable once MergeConditionalExpression
			Color = _color.HasValue ? _color.Value : Color.BLACK;
		}
		
		public override void Draw()
		{
			Vec2 position = transform.GlobalTransform.Translation;
			Vec2 size = transform.GlobalTransform.Scale * Radius;
			
			Raylib.DrawEllipse((int)position.x, (int)position.y, (int)size.x, (int) size.y, Color);
		}
	}
}