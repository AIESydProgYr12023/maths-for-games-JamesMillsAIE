using MathLib;

namespace Azimuth.GameObjects
{
	public class GameObject
	{
		public readonly Transform transform;

		public GameObject()
		{
			transform = new Transform();
		}

		public GameObject(Vec2 _position)
		{
			transform = new(_position);
		}

		public GameObject(Vec2 _position, float _rotation)
		{
			transform = new(_position, _rotation);
		}

		public GameObject(Vec2 _position, float _rotation, Vec2 _scale)
		{
			transform = new(_position, _rotation, _scale);
		}
		
		public virtual void Load() { }

		public virtual void Draw() { }

		public virtual void Update(float _deltaTime) { }

		public virtual void Unload() { }
	}
}