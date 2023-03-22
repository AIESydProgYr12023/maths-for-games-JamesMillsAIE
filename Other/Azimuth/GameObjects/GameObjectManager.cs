namespace Azimuth.GameObjects
{
	public static class GameObjectManager
	{
		private static readonly List<GameObject> gameObjects = new();

		private static readonly List<Action> updateActions = new();

		public static void Spawn(GameObject _gameObject)
		{
			if(!gameObjects.Contains(_gameObject))
			{
				updateActions.Add(() =>
				{
					gameObjects.Add(_gameObject);
					_gameObject.Load();
				});
			}
		}

		public static void Destroy(GameObject _gameObject)
		{
			if(gameObjects.Contains(_gameObject))
			{
				updateActions.Add(() =>
				{
					_gameObject.Unload();
					gameObjects.Remove(_gameObject);
				});
			}
		}

		public static void Update(float _deltaTime)
		{
			for(int action = 0; action < updateActions.Count; action++)
				updateActions[action]();
			
			updateActions.Clear();

			foreach(GameObject gameObject in gameObjects)
			{
				gameObject.Update(_deltaTime);
				if(gameObject.transform.Parent == null)
				{
					gameObject.transform.Update();
				}
			}
		}

		public static void Draw()
		{
			foreach(GameObject gameObject in gameObjects)
				gameObject.Draw();
		}
	}
}