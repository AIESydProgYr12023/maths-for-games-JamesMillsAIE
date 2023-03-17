using Azimuth;
using Azimuth.GameObjects;

using MathLib;

using Raylib_cs;

namespace AIE14_SceneGraphPlanets
{
	public class SceneGraphPlanetsGame : Game
	{
		private GameObject world;
		private PlanetGameObject sun;
		private PlanetGameObject earth;

		public override void Load()
		{
			Vec2 centerScreen = new Vec2(Application.Instance!.Window.Width / 2, Application.Instance.Window.Height / 2);

			world = new GameObject();

			sun = new PlanetGameObject(centerScreen, 75, Color.YELLOW);
			sun.transform.SetParent(world.transform);

			earth = new PlanetGameObject(new Vec2(200, 0), 40, Color.BLUE);
			earth.transform.SetParent(sun.transform);

			PlanetGameObject moon = new(new Vec2(100, 0), 10, Color.GRAY);
			moon.transform.SetParent(earth.transform);
							
			GameObjectManager.Spawn(world);
			GameObjectManager.Spawn(sun);
			GameObjectManager.Spawn(earth);
			GameObjectManager.Spawn(moon);
			
			Raylib.SetTargetFPS(60);
		}

		public override void Update(float _deltaTime)
		{
			sun.transform.transform = Mat3.CreateZRotation(0.01f) * sun.transform.transform;
			earth.transform.transform = Mat3.CreateZRotation(0.1f) * earth.transform.transform;
			
			world.transform.Update();
		}

		public override void Unload() { }
	}
}