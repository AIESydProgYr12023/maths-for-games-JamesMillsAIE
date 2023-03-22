using Azimuth;
using Azimuth.GameObjects;

using MathLib;

using Newtonsoft.Json;

using Raylib_cs;

using TowerDefense.Tiles;

namespace TowerDefense.GameObjects
{
	public class TileMapGameObject : GameObject
	{
	#region Static Helper Functions

		private static Color[,] GetMapColorArray(Image _map)
		{
			Color[,] map = new Color[_map.width, _map.height];

			for(int x = 0; x < _map.width; x++)
			{
				for(int y = 0; y < _map.height; y++)
				{
					map[x, y] = Raylib.GetImageColor(_map, x, y);
				}
			}

			return map;
		}

		private static bool CompareColor(Color _a, Color _b)
		{
			return _a.r == _b.r && _a.g == _b.g && _a.b == _b.b && _a.a == _b.a;
		}

	#endregion

		private readonly Dictionary<TileType, TileMapping> mappingDict = new();
		private TileMapping[] mappings = new TileMapping[1];
		private uint[,] tiles = new uint[1, 1];

		private Action<Vec2> onTowerSlotFound;

		public TileMapGameObject(Action<Vec2> _onTowerSlotFound) : base(Vec2.half, 0, Vec2.one * TileMapping.TILE_SIZE)
		{
			onTowerSlotFound = _onTowerSlotFound;
		}

		public override void Load()
		{
			LoadMappings();
			LoadMap();
		}

		public override void Draw()
		{
			Random random = new(0);
			TileType[] types = Enum.GetValues<TileType>();

			for(int x = 0; x < tiles.GetLength(0); x++)
			{
				for(int y = 0; y < tiles.GetLength(1); y++)
				{
					uint tile = tiles[x, y];

					for(int i = 0; i < types.Length; i++)
					{
						int mask = 0x00000001 << i;
						if((tile & mask) == mask && mappingDict.TryGetValue(types[i], out TileMapping? mapping))
						{
							mapping.Draw(random, x, y);
						}
					}
				}
			}
		}

		private void LoadMappings()
		{
			TileMapping[]? mappingArray = JsonConvert.DeserializeObject<TileMapping[]>(File.ReadAllText("Assets/tile_mappings.json"));

			if(mappingArray != null)
			{
				mappings = new TileMapping[mappingArray.Length];
				Array.Copy(mappingArray, mappings, mappings.Length);

				foreach(TileMapping mapping in mappings)
				{
					mappingDict.Add(mapping.type, mapping);
				}
			}
		}

		private void LoadMap()
		{
			Image levelMap = Assets.Find<Image>("Images/level_map");
			tiles = new uint[levelMap.width, levelMap.height];

			Image environmentMap = Assets.Find<Image>("Images/environment");
			Image towerMap = Assets.Find<Image>("Images/tower_placement");
			
			PopulateWith(GetMapColorArray(levelMap), levelMap.width, levelMap.height);
			PopulateWith(GetMapColorArray(environmentMap), environmentMap.width, environmentMap.height);
			PopulateWith(GetMapColorArray(towerMap), towerMap.width, towerMap.height);
		}

		private uint GetIdFor(Color _color, int _x, int _y)
		{
			foreach(TileMapping mapping in mappings)
			{
				if(CompareColor(mapping.key, _color))
				{
					if(mapping.type == TileType.Tower)
					{
						onTowerSlotFound(new Vec2(_x, _y));

						return 0;
					}

					return (uint) mapping.type;
				}
			}

			return 0;
		}

		private void PopulateWith(Color[,] _map, int _width, int _height)
		{
			for(int x = 0; x < _width; x++)
			{
				for(int y = 0; y < _height; y++)
				{
					tiles[x, y] ^= GetIdFor(_map[x, y], x, y);
				}
			}
		}
	}
}