using Azimuth;
using Azimuth.GameObjects;

using MathLib;

using Raylib_cs;

using TowerDefense.Tiles;

namespace TowerDefense.GameObjects
{
	public class TowerBaseGameObject : GameObject
	{
		private readonly Texture2D texture;

		private TowerTurret? turret;

		public TowerBaseGameObject(Vec2 _position) : base(_position)
		{
			texture = Assets.Find<Texture2D>("Textures/tower_base");
		}

		public override void Load()
		{
			turret = new TowerTurret(this);
			GameObjectManager.Spawn(turret);
		}

		public override void Unload()
		{
			if(turret != null)
			{
				GameObjectManager.Destroy(turret);
				turret = null;
			}
		}

		public override void Draw()
		{
			Vec2 position = transform.Position;

			Rectangle src = new(0, 0, TileMapping.TILE_SIZE, TileMapping.TILE_SIZE);
			Rectangle dst = new(position.x, position.y, transform.Scale.x, transform.Scale.y);
			
			Raylib.DrawTexturePro(texture, src, dst, TileMapping.origin, 0, Color.WHITE);
		}
	}
}