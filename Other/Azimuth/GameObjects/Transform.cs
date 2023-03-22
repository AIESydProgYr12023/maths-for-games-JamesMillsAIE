using MathLib;

namespace Azimuth.GameObjects
{
	public class Transform
	{
		public Transform? Parent { get; private set; }

		public Vec2 LocalPosition => transform.Translation;

		public Vec2 Position
		{
			get => GlobalTransform.Translation;
			set => transform.Translation = value;
		}

		public float Rotation
		{
			get => GlobalTransform.GetRotationX() * Azimath.RAD_2_DEG;
			set => SetRotation(value * Azimath.DEG_2_RAD);
		}

		public Vec2 Scale
		{
			get => GlobalTransform.Scale;
			set => transform.Scale = value;
		}

		public Mat3 GlobalTransform => Parent == null ? transform : transform * Parent.GlobalTransform;

		public Mat3 transform;
		
		private readonly List<Transform> children = new();

		private readonly List<Action> updateChildrenActions = new();

		public Transform(Vec2? _position = null, float _rotation = 0f, Vec2? _scale = null)
		{
			// ReSharper disable once MergeConditionalExpression
			transform = Mat3.CreateTransform(_position.HasValue ? _position.Value : Vec2.zero, _rotation, _scale);
		}

		public void Update()
		{
			foreach(Action action in updateChildrenActions)
				action();
			
			updateChildrenActions.Clear();

			foreach(Transform child in children)
				child.Update();
		}

		public void AddChild(Transform _child)
		{
			updateChildrenActions.Add(() =>
			{
				_child.Parent?.RemoveChild(_child);

				_child.Parent = this;
				children.Add(_child);
			});
		}

		public void RemoveChild(Transform _child)
		{
			updateChildrenActions.Add(() =>
			{
				if(_child.Parent == this)
				{
					_child.Parent = null;
					children.Remove(_child);
				}
			});
		}

		public void SetParent(Transform? _parent)
		{
			if(_parent == null)
			{
				Parent?.RemoveChild(this);
			}
			else
			{
				_parent.AddChild(this);
			}
		}

		private void SetRotation(float _radians) => transform.SetZRotation(_radians);
	}
}