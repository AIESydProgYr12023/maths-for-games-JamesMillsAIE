namespace TowerDefense.Tiles
{
	public enum TileType
	{
		Grass = 0x00000001 << 0,			// 1
		PathHorizontal = 0x00000001 << 1,	// 2
		PathVertical = 0x00000001 << 2,		// 4
		PathUpRight = 0x00000001 << 3,		// 8
		PathUpLeft = 0x00000001 << 4,		// 16
		PathDownRight = 0x00000001 << 5,	// 32
		PathDownLeft = 0x00000001 << 6,		// 64
		Tower = 0x00000001 << 7,			// 128
		Rock = 0x00000001 << 8,				// 256
		Tree = 0x00000001 << 9				// 512
	}
}