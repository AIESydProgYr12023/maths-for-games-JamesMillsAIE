namespace AIE01_Binary
{
	public static class Weapons
	{
		public const byte CHAINSAW = 0x01;				// 0000 0001 | 0x01 | 1
		public const byte PISTOL = 0x01 << 1;			// 0000 0010 | 0x02 | 2
		public const byte SHOTGUN = 0x01 << 2;			// 0000 0100 | 0x04 | 4
		public const byte SUPER_SHOTGUN = 0x01 << 3;	// 0000 1000 | 0x08 | 8
		public const byte CHAINGUN = 0x01 << 4;			// 0001 0000 | 0x10 | 16
		public const byte ROCKET_LAUNCHER = 0x01 << 5;	// 0010 0000 | 0x20 | 32
		public const byte PLASMA_GUN = 0x01 << 6;		// 0100 0000 | 0x40 | 64
		public const byte BFG9000 = 0x01 << 7;			// 1000 0000 | 0x80 | 128
		// 1111 1111 <- max possible byte value aka 255 | 

		public static readonly string[] weapons =
		{
			"Fists",
			"Chainsaw",
			"Pistol",
			"Shotgun",
			"Super Shotgun",
			"Chaingun",
			"Rocket Launcher",
			"Plasma Gun",
			"BFG 9000"
		};
	}
}