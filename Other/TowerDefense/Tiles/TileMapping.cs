using Azimuth;

using MathLib;

using Newtonsoft.Json;

using Raylib_cs;

namespace TowerDefense.Tiles
{
	[JsonObject(MemberSerialization.OptOut)]
	public class TileMapping
	{
		[JsonIgnore] public const int TILE_SIZE = 64;
		[JsonIgnore] public static readonly Vec2 origin = new(TILE_SIZE / 2, TILE_SIZE / 2);

		public string textureId;
		public Color key;
		public TileType type;
		public float rotation;
		public Vec2[] uvs;

		[JsonIgnore] private Texture2D? texture;

		public TileMapping()
		{
			textureId = "";
			key = Color.BLANK;
			type = TileType.Grass;
			rotation = 0;
			uvs = new Vec2[1];
		}

		public void Draw(Random _rand, int _x, int _y)
		{
			texture ??= Assets.Find<Texture2D>($"Textures/{textureId}");

			if(texture.HasValue)
			{
				Mat3 t = Mat3.CreateTransform(new Vec2(_x, _y) + Vec2.half, 0, Vec2.one * TILE_SIZE);
				t.RotationZ = rotation * Azimath.DEG_2_RAD;
				float r = t.RotationX * Azimath.RAD_2_DEG;

				Vec2 uv = uvs[_rand.Next(0, uvs.Length)];

				Rectangle src = new(uv.x, uv.y, TILE_SIZE, TILE_SIZE);
				Rectangle dst = new(t.Translation.x, t.Translation.y, t.Scale.x, t.Scale.y);
				
				Raylib.DrawTexturePro(texture.Value, src, dst, origin, r, Color.WHITE);
			}
		}
	}
}