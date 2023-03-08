namespace AIE01_Binary
{
	public static class Program
	{
		public static void Main()
		{
			Inventory inventory = new Inventory();
			inventory.PrintContents();
			
			Console.WriteLine();

			inventory = new Inventory(0x00);
			inventory.PrintContents();
			
			Console.WriteLine();
			
			inventory.AddWeapon(Weapons.BFG9000);
			inventory.PrintContents();

			Console.WriteLine();
			
			// 1000 0000
			// 0000 0100
			// 1000 0100
			inventory.ToggleWeapon(Weapons.SHOTGUN);
			inventory.PrintContents();
			
			Console.WriteLine();

			// 1000 0100
			// 0000 0100
			// 1000 0000
			inventory.ToggleWeapon(Weapons.SHOTGUN);
			inventory.SmartPrint();
		}
	}
}