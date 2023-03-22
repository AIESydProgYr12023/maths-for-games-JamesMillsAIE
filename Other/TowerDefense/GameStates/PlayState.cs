using Azimuth.GameObjects;
using Azimuth.GameStates;

using MathLib;

using TowerDefense.GameObjects;

namespace TowerDefense.GameStates
{
	public class PlayState : IGameState
	{
		public string ID => TowerDefenseGame.PLAY_ID;

		private TileMapGameObject? tileMap;
		private List<TowerSlotGameObject> slots = new();
		private List<TowerBaseGameObject> bases = new();

		public void Load()
		{
			tileMap = new TileMapGameObject(OnTowerSlotFound);

			GameObjectManager.Spawn(tileMap);
		}

		public void Update(float _deltaTime) { }

		public void Draw() { }

		public void Unload()
		{
			foreach(TowerSlotGameObject slot in slots)
				GameObjectManager.Destroy(slot);
			
			slots.Clear();

			foreach(TowerBaseGameObject tb in bases)
				GameObjectManager.Destroy(tb);

			bases.Clear();
			
			if(tileMap != null)
				GameObjectManager.Destroy(tileMap);
		}

		private void OnTowerSlotFound(Vec2 _pos)
		{
			if(tileMap == null)
				return;

			TowerSlotGameObject go = new(_pos, OnTowerSlotClicked);
			go.transform.SetParent(tileMap.transform);
			
			slots.Add(go);
			GameObjectManager.Spawn(go);
		}

		private void OnTowerSlotClicked(TowerSlotGameObject _slot)
		{
			if(tileMap == null)
				return;

			if(slots.Contains(_slot))
				slots.Remove(_slot);
			
			// Spawn the tower base
			TowerBaseGameObject towerBase = new(_slot.transform.LocalPosition);
			towerBase.transform.SetParent(tileMap.transform);
			
			bases.Add(towerBase);
			GameObjectManager.Spawn(towerBase);
		}
	}
}