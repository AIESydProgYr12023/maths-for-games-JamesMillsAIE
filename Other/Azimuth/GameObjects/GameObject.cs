using MathLib;

namespace Azimuth.GameObjects
{
	public class GameObject
	{
		public Vec2 position;
		
		public virtual void Load() { }

		public virtual void Draw() { }

		public virtual void Update(float _deltaTime) { }

		public virtual void Unload() { }
	}
}