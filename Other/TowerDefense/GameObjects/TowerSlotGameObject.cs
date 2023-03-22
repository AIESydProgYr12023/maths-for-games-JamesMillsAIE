using Azimuth;
using Azimuth.GameObjects;

using MathLib;
using MathLib.Geometry;

using Raylib_cs;

using TowerDefense.Tiles;

namespace TowerDefense.GameObjects
{
	public class TowerSlotGameObject : GameObject
	{
		public Action<TowerSlotGameObject> onClicked;

		private readonly Texture2D texture;
		private readonly Rect rect;

		private Color tint;

		public TowerSlotGameObject(Vec2 _tilePos, Action<TowerSlotGameObject> _onClicked) : base(_tilePos)
		{
			onClicked = _onClicked;
			texture = Assets.Find<Texture2D>("Textures/towerslot");
			rect = new Rect((_tilePos + Vec2.half) * TileMapping.TILE_SIZE, Vec2.half * TileMapping.TILE_SIZE);

			tint = Color.WHITE;
		}

		public override void Update(float _deltaTime)
		{
			Hit? hit = rect.Intersects(Raylib.GetMousePosition());
			if(hit != null)
			{
				tint = Color.RED;
				if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
				{
					GameObjectManager.Destroy(this);
					onClicked(this);
				}
			}
			else
			{
				tint = Color.WHITE;
			}
		}

		public override void Draw()
		{
			Vec2 position = transform.Position;

			Rectangle src = new(0, 0, TileMapping.TILE_SIZE, TileMapping.TILE_SIZE);
			Rectangle dst = new(position.x, position.y, transform.Scale.x, transform.Scale.y);
			
			Raylib.DrawTexturePro(texture, src, dst, TileMapping.origin, 0, tint);
		}
	}
}