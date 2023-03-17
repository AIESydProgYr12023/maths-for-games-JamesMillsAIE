using Azimuth;
using Azimuth.GameObjects;

using MathLib;

using Raylib_cs;

namespace AIE15_SceneGraphPlanetsSprite
{
	public class PlanetGameObject : GameObject
	{
		public float Radius { get; set; }
		public Texture2D Texture { get; set; }

		public PlanetGameObject(Vec2 _position, float _radius, string _name) : base(_position)
		{
			Radius = _radius;
			Texture = Assets.Find<Texture2D>($"Textures/{_name}");
		}

		public override void Draw()
		{
			Vec2 pos = transform.GlobalTransform.Translation;
			Vec2 size = transform.GlobalTransform.Scale * Radius;
			float rot = transform.GlobalTransform.RotationX * Azimath.RAD_2_DEG;

			Rectangle src = new(0, 0, Texture.width, Texture.height);
			Rectangle dst = new(pos.x, pos.y, size.x, size.y);
			
			Raylib.DrawTexturePro(Texture, src, dst, size * 0.5f, rot, Color.WHITE);
		}
	}
}