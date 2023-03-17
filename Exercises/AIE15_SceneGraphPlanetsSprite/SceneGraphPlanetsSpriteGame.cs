using Azimuth;
using Azimuth.GameObjects;

using MathLib;

namespace AIE15_SceneGraphPlanetsSprite
{
	public class SceneGraphPlanetsSpriteGame : Game
	{
		private GameObject world;
		private PlanetGameObject sun;
		private PlanetGameObject earth;

		public override void Load()
		{
			Vec2 centerScreen = new Vec2(Application.Instance!.Window.Width / 2, Application.Instance.Window.Height / 2);

			world = new GameObject();

			sun = new PlanetGameObject(centerScreen, 75, "planet1");
			sun.transform.SetParent(world.transform);

			earth = new PlanetGameObject(new Vec2(200, 0), 40, "planet2");
			earth.transform.SetParent(sun.transform);

			PlanetGameObject moon = new PlanetGameObject(new Vec2(100, 0), 10, "planet3");
			moon.transform.SetParent(earth.transform);
			
			GameObjectManager.Spawn(world);
			GameObjectManager.Spawn(sun);
			GameObjectManager.Spawn(earth);
			GameObjectManager.Spawn(moon);
		}

		public override void Update(float _deltaTime)
		{
			sun.transform.transform = Mat3.CreateZRotation(1 * _deltaTime) * sun.transform.transform;
			earth.transform.transform = Mat3.CreateZRotation(10 * _deltaTime) * earth.transform.transform;
		}

		public override void Unload() { }
	}
}