using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE07_OverlappingCircles
{
	public class OverlappingCirclesGame : Game
	{
		public Random rand = new();
		public List<Circle> circles = new();

		public override void Load() { }

		public override void Unload() { }

		public override void Draw()
		{
			foreach(Circle circle in circles)
				Raylib.DrawCircleV(circle.position, circle.radius, circle.color);
		}

		public override void Update(float _deltaTime)
		{
			TryCreateNewCircle(Raylib.GetMousePosition());
			UpdateCirclePositions(_deltaTime);
			CheckCirclesOverlapping();
		}

		private void TryCreateNewCircle(Vec2 _mousePos)
		{
			if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
			{
				circles.Add(new Circle()
				{
					position = _mousePos,
					radius = rand.Next(20, 50),
					direction = new Vec2((float)rand.NextDouble() * 2f - 1f,
					                     (float)rand.NextDouble() * 2f - 1f).Normalized
				});
			}
		}

		private void UpdateCirclePositions(float _deltaTime)
		{
			foreach(Circle circle in circles)
				circle.position += circle.direction * _deltaTime * 250;

			int windowWidth = Application.Instance!.Window.Width;
			int windowHeight = Application.Instance.Window.Height;
			foreach(Circle circle in circles)
			{
				if(circle.position.x - circle.radius < 0)
					circle.direction.x = -circle.direction.x;

				if(circle.position.x + circle.radius > windowWidth)
					circle.direction.x = -circle.direction.x;
				
				if(circle.position.y - circle.radius < 0)
					circle.direction.y = -circle.direction.y;

				if(circle.position.y + circle.radius > windowHeight)
					circle.direction.y = -circle.direction.y;
			}
		}

		private void CheckCirclesOverlapping()
		{
			foreach(Circle circle in circles)
				circle.color = new Color(0, 0, 0, 128);

			for(int i = 0; i < circles.Count; i++)
			{
				for(int j = i + 1; j < circles.Count; j++)
				{
					float distance = Vec2.Distance(circles[i].position, circles[j].position);

					if(distance < circles[i].radius + circles[j].radius)
					{
						circles[i].color = new Color(255, 0, 0, 128);
						circles[j].color = new Color(255, 0, 0, 128);
					}
				}
			}
		}
	}
}