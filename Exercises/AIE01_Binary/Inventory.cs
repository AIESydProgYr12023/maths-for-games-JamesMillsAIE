namespace AIE01_Binary
{
	public class Inventory
	{
		private byte contents;

		public Inventory()
		{
			contents = 0x43;
		}

		public Inventory(byte _contents)
		{
			contents = _contents;
		}

		public void AddWeapon(byte _weapon)
		{
			contents |= _weapon;
		}

		public void ToggleWeapon(byte _weapon)
		{
			// 0100
			// 0110
			// 0010
			contents ^= _weapon;
		}
		
		public void PrintContents()
		{
			Console.Write("Fists | ");
			
			// 0000 0001
			if((contents & Weapons.CHAINSAW) == Weapons.CHAINSAW)
				Console.Write("Chainsaw | ");
			
			// 0000 0010
			if((contents & Weapons.PISTOL) == Weapons.PISTOL)
				Console.Write("Pistol | ");
			
			// 0000 0100
			if((contents & Weapons.SHOTGUN) == Weapons.SHOTGUN)
				Console.Write("Shotgun | ");
			
			// 0000 1000
			if((contents & Weapons.SUPER_SHOTGUN) == Weapons.SUPER_SHOTGUN)
				Console.Write("Super Shotgun | ");
			
			// 0001 0000
			if((contents & Weapons.CHAINGUN) == Weapons.CHAINGUN)
				Console.Write("Chain Gun | ");
			
			// 0010 0000
			if((contents & Weapons.ROCKET_LAUNCHER) == Weapons.ROCKET_LAUNCHER)
				Console.Write("Rocket Launcher | ");
			
			// 0100 0000
			if((contents & Weapons.PLASMA_GUN) == Weapons.PLASMA_GUN)
				Console.Write("Plasma Gun | ");
			
			// 1000 0000
			if((contents & Weapons.BFG9000) == Weapons.BFG9000)
				Console.Write("BFG 9000 | ");
		}

		public void SmartPrint()
		{
			Console.Write($"{Weapons.weapons[0]} | ");

			for(int i = 1; i < Weapons.weapons.Length; i++)
			{
				int mask = 0x01 << i - 1;
				if((contents & mask) == mask)
					Console.Write($"{Weapons.weapons[i]} | ");
			}

			Console.WriteLine();
		}
	}
}