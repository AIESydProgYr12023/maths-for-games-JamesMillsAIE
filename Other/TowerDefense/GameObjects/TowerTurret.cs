using Azimuth;
using Azimuth.GameObjects;

using MathLib;

using Raylib_cs;

using TowerDefense.Tiles;

namespace TowerDefense.GameObjects
{
	public class TowerTurret : GameObject
	{
		private Vec2 target;

		private readonly Texture2D texture;

		public TowerTurret(GameObject _base)
		{
			texture = Assets.Find<Texture2D>("Textures/tower_turret");
			transform.SetParent(_base.transform);
		}

		public override void Update(float _deltaTime)
		{
			if(target == Vec2.zero)
			{
				transform.Rotation = 0;
				return;
			}

			Vec2 dir = transform.Position - target;
			dir.Normalize();
			dir.Rotate(-(MathF.PI / 2));
			transform.Rotation = MathF.Atan2(dir.y, dir.x);
		}

		public override void Draw()
		{
			Vec2 position = transform.Position;

			Rectangle src = new(0, 0, TileMapping.TILE_SIZE, TileMapping.TILE_SIZE);
			Rectangle dst = new(position.x, position.y, transform.Scale.x, transform.Scale.y);
			
			Raylib.DrawTexturePro(texture, src, dst, TileMapping.origin, transform.Rotation, Color.WHITE);
		}
	}
}